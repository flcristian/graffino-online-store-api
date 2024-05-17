using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Orders.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;
using graffino_online_store_api.Users.Repository.Interfaces;

namespace graffino_online_store_api.Orders.Services;

public class OrdersQueryService(
    IUsersRepository usersRepository,
    IOrdersRepository ordersRepository
    ) : IOrdersQueryService
{
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        List<Order> orders = (await ordersRepository.GetAllAsync()).ToList();

        if (orders.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.ORDERS_DO_NOT_EXIST);
        }

        return orders;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersByCustomerId(string customerId)
    {
        if (await usersRepository.GetByIdAsync(customerId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST);
        }
        
        List<Order> orders = (await ordersRepository.GetAllByCustomerIdAsync(customerId)).ToList();

        if (orders.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.CUSTOMER_HAS_NO_ORDERS);
        }

        return orders;
    }

    public async Task<Order> GetCartByCustomerId(string customerId)
    {
        if (await usersRepository.GetByIdAsync(customerId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.USER_DOES_NOT_EXIST);
        }

        Order? order = await ordersRepository.GetCartByCustomerIdAsync(customerId);

        if (order == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.CART_DOES_NOT_EXIST);
        }

        return order;
    }
    
    public async Task<Order> GetOrderById(int id)
    {
        Order? order = await ordersRepository.GetByIdAsync(id);

        if (order == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DOES_NOT_EXIST);
        }

        return order;
    }
}