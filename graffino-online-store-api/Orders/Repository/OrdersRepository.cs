using AutoMapper;
using graffino_online_store_api.Data;
using graffino_online_store_api.OrderDetails.Models;
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
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(string customerId)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Where(o => o.CustomerId.Equals(customerId))
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateAsync(CreateOrderRequest request)
    {
        Order order = new Order
        {
            CustomerId = request.CustomerId,
            Status = OrderStatus.Processing,
            LastDateUpdated = DateTime.Now
        };
        context.Orders.Add(order);

        order.OrderDetails = new List<OrderDetail>();
        request.OrderDetails.ForEach(od =>
        {
            OrderDetail orderDetail = mapper.Map<OrderDetail>(od);
            order.OrderDetails.Add(orderDetail);
        });
        
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