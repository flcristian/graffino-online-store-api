using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace graffino_online_store_api.Products.Repository.Intercom.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CheckoutController : ControllerBase
{
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession()
    {
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
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = 5000,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Sample Product"
                        },
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = "https://localhost:4200/success",
            CancelUrl = "https://localhost:4200/cancel"
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return Ok(new { id = session.Id });
    }
}