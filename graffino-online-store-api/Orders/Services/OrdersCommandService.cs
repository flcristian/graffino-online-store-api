using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;
using graffino_online_store_api.Users.Repository.Interfaces;

namespace graffino_online_store_api.Orders.Services;

public class OrdersCommandService(
    IUsersRepository usersRepository,
    IOrdersRepository ordersRepository
    ) : IOrdersCommandService
{
    public async Task<Order> CreateOrder(CreateOrderRequest request)
    {
        if (await usersRepository.GetByIdAsync(request.CustomerId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST);
        }

        if (await ordersRepository.GetCartByCustomerIdAsync(request.CustomerId) != null)
        {
            throw new ItemAlreadyExistsException(ExceptionMessages.CART_ALREADY_EXISTS);
        }

        Order order = await ordersRepository.CreateAsync(request);
        return order;
    }

    public async Task<Order> UpdateOrder(UpdateOrderRequest request)
    {
        if (request.Status == OrderStatus.None || request.Status == OrderStatus.Cart)
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

    public async Task<Order> PlaceOrder(PlaceOrderRequest request)
    {
        Order? order = await ordersRepository.GetByIdAsync(request.Id);
        
        if (order == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DOES_NOT_EXIST);
        }

        if (order.Status != OrderStatus.Cart)
        {
            throw new InvalidValueException(ExceptionMessages.ORDER_NOT_CART);
        }

        order = await ordersRepository.UpdateAsync(new UpdateOrderRequest
        {
            Id = request.Id,
            Status = OrderStatus.Processing
        });

        return order;
    }
}