using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class BundleMapper
    {
        public BundlesResponseModel MappBundleRequestModelToBundleResponseModel(BundlesRequestModel model, List<ServicesResponseModel> services, int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BundlesRequestModel, BundlesResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<BundlesResponseModel>(model);
            responseModel.Id = id;
            responseModel.Services = services;
            return responseModel;
        }
    }
}
