using graffino_online_store_api.Orders.Controllers.Interfaces;
using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Intercom.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace graffino_online_store_api.System.Intercom.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CheckoutController(
    OrdersApiController ordersController,
    ILogger<CheckoutController> logger
    ) : ControllerBase
{
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutRequest request)
    {
        string orderRequestJson = JsonConvert.SerializeObject(request.OrderRequest);
            
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new()
                    {
                        Currency = "usd",
                        UnitAmount = 1000,
                        ProductData = new()
                        {
                            Name = "Shipping"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = "http://localhost:4200/payment-successful",
            CancelUrl = "http://localhost:4200/payment-canceled",
            Metadata = new Dictionary<string, string>{ {"order_request", orderRequestJson} }
        };

        foreach (CheckoutProductDetailDTO dto in request.ProductDetails)
        {
            options.LineItems.Add(new()
            {
                PriceData = new()
                {
                    Currency = "usd",
                    UnitAmount = (long)(dto.Price * 100),
                    ProductData = new()
                    {
                        Name = dto.Name
                    }
                },
                Quantity = dto.Count
            });
        }

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return Ok(new { id = session.Id });
    }
    
    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var signature = Request.Headers["Stripe-Signature"];

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json, signature, SystemConstants.WEBHOOK_ENDPOINT_SECRET);

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                
                if (session.Metadata.TryGetValue("order_request", out var orderRequestJson))
                {
                    CreateOrderRequest orderRequest = JsonConvert.DeserializeObject<CreateOrderRequest>(orderRequestJson)!;
                    
                    var result = await ordersController.CreateOrder(orderRequest);

                    if (result.Result is CreatedResult createdResult)
                    {
                        logger.LogInformation("Order created successfully.");
                        return createdResult;
                    }

                    logger.LogWarning("Order creation failed.");
                    return BadRequest("Order creation failed.");
                }
                
                return Ok(session);
            }

            logger.LogInformation("Unhandled event type: {type}", stripeEvent.Type);
            return Ok();
        }
        catch (StripeException e)
        {
            logger.LogError("Stripe webhook error: {error}", e.Message);
            return BadRequest();
        }
    }
}