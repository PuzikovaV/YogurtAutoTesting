using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CleaningObjectMapper
    {
        public CleaningObjectResponseModel MappCleaningObjectRequestModelToCleaningObjectResponseModel(CleaningObjectRequestModel model)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CleaningObjectRequestModel, CleaningObjectResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleaningObjectResponseModel>(model);
            return responseModel;
        }
    }
}
