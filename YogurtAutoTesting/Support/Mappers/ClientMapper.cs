using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class ClientMapper
    {
        public ClientResponseModel MappClientRequestModelToClientResponseModel(ClientRequestModel model, int id, DateTime date)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientRequestModel, ClientResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<ClientResponseModel>(model);
            responseModel.Id = id;
            responseModel.RegistrationDate = date;
            return responseModel;
        }

        public ClientResponseModel MappUpdateClientRequestModelToClientResponseModel(UpdateClientRequestModel model, int id, DateTime date, string email)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateClientRequestModel, ClientResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<ClientResponseModel>(model);
            responseModel.Id = id;
            responseModel.RegistrationDate = date;
            responseModel.Email = email;
            return responseModel;
        }
    }
}
