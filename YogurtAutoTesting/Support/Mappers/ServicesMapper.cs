using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class ServicesMapper
    {
        public ServicesResponseModel MappServiceRequestModelToServiceResponseModel(ServicesRequestModel model, int id)
        {
            var config = new MapperConfiguration(cfg=>cfg.CreateMap<ServicesRequestModel, ServicesResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<ServicesResponseModel>(model);
            responseModel.Id = id;
            return responseModel;
        }

        /*public List<ServicesResponseModel> MappListServicesRequestModelToListServicesResponseModel(List<ServicesRequestModel> model, List<int> servicesIds)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ServicesRequestModel, ServicesResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<List<ServicesResponseModel>>(model);
            responseModel.Id
            return responseModel;
        }*/
    }
}
