using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;
using graffino_online_store_api.Users.Repository.Interfaces;

namespace graffino_online_store_api.Orders.Services;

public class OrdersCommandService(
    IUsersRepository usersRepository,
    IOrdersRepository ordersRepository,
    IProductsRepository productsRepository
    
    ) : IOrdersCommandService
{
    public async Task<Order> CreateOrder(CreateOrderRequest request)
    {
        if (await usersRepository.GetByIdAsync(request.CustomerId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST);
        }

        foreach (CreateOrderDetailRequest od in request.OrderDetails)
        {
            if (await productsRepository.GetProductByIdAsync(od.ProductId) == null)
            {
                throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
            }
        }

        Order order = await ordersRepository.CreateAsync(request);
        return order;
    }

    public async Task<Order> UpdateOrder(UpdateOrderRequest request)
    {
        if (request.Status == OrderStatus.None)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_ORDER_STATUS);
        }
        
        if (await ordersRepository.GetByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DOES_NOT_EXIST);
        }

        Order order = await ordersRepository.UpdateAsync(request);
        return order;
    }

    public async Task<Order> DeleteOrderById(int id)
    {
        if (await ordersRepository.GetByIdAsync(id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DOES_NOT_EXIST);
        }

        Order order = await ordersRepository.DeleteByIdAsync(id);
        return order;
    }
}