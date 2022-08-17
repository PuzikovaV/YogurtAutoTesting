using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CleanerMapper
    {
        public CleanerResponseModel MappCleanerRequestModelToCleanerResponseModel(CleanerRequestModel model, int id, DateTime date)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CleanerRequestModel, CleanerResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleanerResponseModel>(model);
            responseModel.Id = id;
            responseModel.RegistrationDate = date;
            return responseModel;
        }

        public CleanerResponseModel MappUpdateCleanerRequestModelToCleanerResponseModel(UpdateCleanerRequestModel model, DateTime date)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCleanerRequestModel, CleanerResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleanerResponseModel>(model);
            responseModel.RegistrationDate = date;
            return responseModel;
        }
    }
}
