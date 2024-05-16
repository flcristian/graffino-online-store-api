using AutoMapper;
using graffino_online_store_api.Data;
using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace graffino_online_store_api.OrderDetails.Repository;

public class OrderDetailsRepository(AppDbContext context, IMapper mapper) : IOrderDetailsRepository
{
    public async Task<IEnumerable<OrderDetail>> GetAllAsync()
    {
        return await context.OrderDetails
            .Include(od => od.Product)
            .ToListAsync();
    }

    public async Task<OrderDetail?> GetByIdAsync(int id)
    {
        return await context.OrderDetails
            .Include(od => od.Product)
            .FirstOrDefaultAsync(od => od.Id == id);
    }

    public async Task<OrderDetail> CreateAsync(CreateOrderDetailRequest request)
    {
        OrderDetail orderDetail = mapper.Map<OrderDetail>(request);
        context.OrderDetails.Add(orderDetail);
        await context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<OrderDetail> UpdateAsync(UpdateOrderDetailRequest request)
    {
        OrderDetail orderDetail = (await context.OrderDetails .FirstOrDefaultAsync(od => od.Id == request.Id))!;
        orderDetail.Count = request.Count;
        context.OrderDetails.Update(orderDetail);
        await context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<OrderDetail> DeleteByIdAsync(int id)
    {
        OrderDetail orderDetail = (await context.OrderDetails .FirstOrDefaultAsync(od => od.Id == id))!;
        context.OrderDetails.Remove(orderDetail);
        await context.SaveChangesAsync();
        return orderDetail;
    }
}