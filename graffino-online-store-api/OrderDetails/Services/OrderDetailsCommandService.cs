using graffino_online_store_api.OrderDetails.DTOs;
using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Repository.Interfaces;
using graffino_online_store_api.OrderDetails.Services.Interfaces;
using graffino_online_store_api.Orders.Repository.Interfaces;
using graffino_online_store_api.Products.Repository.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.OrderDetails.Services;

public class OrderDetailsCommandService(
    IOrderDetailsRepository orderDetailsRepository,
    IOrdersRepository ordersRepository,
    IProductsRepository productsRepository
    ) : IOrderDetailsCommandService
{
    public async Task<OrderDetail> CreateOrderDetail(CreateOrderDetailRequest request)
    {
        if (request.Count <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_ORDER_DETAIL_PRODUCT_COUNT);
        }
        
        if (await ordersRepository.GetByIdAsync(request.OrderId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DOES_NOT_EXIST);
        }

        if (await productsRepository.GetProductByIdAsync(request.ProductId) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.PRODUCT_DOES_NOT_EXIST);
        }

        if (await orderDetailsRepository.GetByOrderAndProductIdsAsync(request.OrderId, request.ProductId) == null)
        {
            throw new ItemAlreadyExistsException(ExceptionMessages.ORDER_DETAIL_ALREADY_EXISTS);
        }

        OrderDetail orderDetail = await orderDetailsRepository.CreateAsync(request);
        return orderDetail;
    }

    public async Task<OrderDetail> UpdateOrderDetail(UpdateOrderDetailRequest request)
    {
        if (request.Count <= 0)
        {
            throw new InvalidValueException(ExceptionMessages.INVALID_ORDER_DETAIL_PRODUCT_COUNT);
        }

        if (await orderDetailsRepository.GetByIdAsync(request.Id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DETAIL_DOES_NOT_EXIST);
        }

        OrderDetail orderDetail = await orderDetailsRepository.UpdateAsync(request);
        return orderDetail;
    }

    public async Task<OrderDetail> DeleteOrderDetailById(int id)
    {
        if (await orderDetailsRepository.GetByIdAsync(id) == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DETAIL_DOES_NOT_EXIST);
        }
        
        OrderDetail orderDetail = await orderDetailsRepository.DeleteByIdAsync(id);
        return orderDetail;
    }
}