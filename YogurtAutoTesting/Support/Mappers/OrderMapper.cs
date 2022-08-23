using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class OrderMapper
    {
        public OrdersResponseModel MappOrderRequestModelToOrderResponseModel(OrderRequestModel orderModel, int orderId, ClientResponseModel clientResponse, double price, DateTime endTime,
            DateTime updateTime, int status, CleaningObjectResponseModel cleaningObjectResponse, List<ServicesResponseModel> serviceResponse, List<BundlesResponseModel> bundleResponse,
            List<CleanerResponseModel> cleanerResponse)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderRequestModel, OrdersResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<OrdersResponseModel>(orderModel);
            responseModel.Id = orderId;
            responseModel.Price = price;
            responseModel.EndTime = endTime;
            responseModel.UpdateTime = updateTime;
            responseModel.Status = status;
            responseModel.Client = clientResponse;
            responseModel.CleaningObject = cleaningObjectResponse;
            responseModel.Bundles = bundleResponse;
            responseModel.Services = serviceResponse;
            responseModel.CleanersBand = cleanerResponse;
            return responseModel;
        }

        public OrdersResponseModel MappUpdateOrderRequestModelToOrderResponseModel(UpdateOrderRequestModel orderModel, int orderId, ClientResponseModel clientResponse, int price, DateTime endTime,
            DateTime updateTime, int status, CleaningObjectResponseModel cleaningObjectResponse, List<ServicesResponseModel> serviceResponse, List<BundlesResponseModel> bundleResponse,
            List<CleanerResponseModel> cleanerResponse, List<CommentsByClientResponseModel> commentsModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateOrderRequestModel, OrdersResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<OrdersResponseModel>(orderModel);
            responseModel.Id = orderId;
            responseModel.Price = price;
            responseModel.EndTime = endTime;
            responseModel.UpdateTime = updateTime;
            responseModel.Status = status;
            responseModel.Client = clientResponse;
            responseModel.CleaningObject = cleaningObjectResponse;
            responseModel.Bundles = bundleResponse;
            responseModel.Services = serviceResponse;
            responseModel.CleanersBand = cleanerResponse;
            return responseModel;
        }
    }
}
