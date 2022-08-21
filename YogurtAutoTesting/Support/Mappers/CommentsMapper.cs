using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CommentsMapper
    {
        public CommentsResponseModel MappCommentsRequestModelToCommentsResponseModel(CommentsRequestModel model, int commentId, int cleanerId, int clientId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentsRequestModel, CommentsResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CommentsResponseModel>(model);
            responseModel.Id = commentId;
            responseModel.CleanerId = cleanerId;
            responseModel.ClientId = clientId;
            return responseModel;
        }
    }
}
