using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CleaningObjectMapper
    {
        public CleaningObjectResponseModel MappCleaningObjectRequestModelToCleaningObjectResponseModel(CleaningObjectRequestModel model, int id, int objectId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CleaningObjectRequestModel, CleaningObjectResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleaningObjectResponseModel>(model);
            responseModel.Id = objectId;
            responseModel.ClientId = id;
            return responseModel;
        }
        
    }
}
