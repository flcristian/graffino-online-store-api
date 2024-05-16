using graffino_online_store_api.OrderDetails.Models;
using graffino_online_store_api.OrderDetails.Repository.Interfaces;
using graffino_online_store_api.OrderDetails.Services.Interfaces;
using graffino_online_store_api.System.Constants;
using graffino_online_store_api.System.Exceptions;

namespace graffino_online_store_api.OrderDetails.Services;

public class OrderDetailsQueryService(IOrderDetailsRepository repository) : IOrderDetailsQueryService
{
    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
    {
        List<OrderDetail> orderDetails = (await repository.GetAllAsync()).ToList();

        if (orderDetails.Count == 0)
        {
            throw new ItemsDoNotExistException(ExceptionMessages.ORDER_DETAILS_DO_NOT_EXIST);
        }

        return orderDetails;
    }

    public async Task<OrderDetail> GetOrderDetailById(int id)
    {
        OrderDetail? orderDetail = await repository.GetByIdAsync(id);

        if (orderDetail == null)
        {
            throw new ItemDoesNotExistException(ExceptionMessages.ORDER_DETAIL_DOES_NOT_EXIST);
        }

        return orderDetail;
    }
}