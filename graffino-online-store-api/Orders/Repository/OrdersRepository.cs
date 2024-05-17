using AutoMapper;
using graffino_online_store_api.Data;
using graffino_online_store_api.Orders.DTOs;
using graffino_online_store_api.Orders.Models;
using graffino_online_store_api.Orders.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.Orders.Repository;

public class OrdersRepository(AppDbContext context, IMapper mapper) : IOrdersRepository
{
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .Where(o => o.Status != OrderStatus.Cart)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(string customerId)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .Where(o => o.CustomerId.Equals(customerId) && o.Status != OrderStatus.Cart)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order?> GetCartByCustomerIdAsync(string customerId)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.CustomerId.Equals(customerId) && o.Status == OrderStatus.Cart);
    }

    public async Task<Order> CreateAsync(CreateOrderRequest request)
    {
        Order order = mapper.Map<Order>(request);
        order.Status = OrderStatus.Cart;
        order.LastDateUpdated = DateTime.Now;
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateAsync(UpdateOrderRequest request)
    {
        Order order = (await context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id))!;
        order.Status = request.Status;
        order.LastDateUpdated = DateTime.Now;
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> DeleteByIdAsync(int id)
    {
        Order order = (await context.Orders.FirstOrDefaultAsync(o => o.Id == id))!;
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        return order;
    }
}