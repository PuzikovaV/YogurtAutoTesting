using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CommentsMapper
    {
        public CommentsByClientResponseModel MappCommentsRequestModelToCommentsResponseModelForClient(CommentsRequestModel model, int commentId, int clientId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentsRequestModel, CommentsByClientResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CommentsByClientResponseModel>(model);
            responseModel.Id = commentId;
            responseModel.CleanerId = null;
            responseModel.ClientId = clientId;
            return responseModel;
        }
        public CommentsByCleanerResponseModel MappCommentsRequestModelToCommentsResponseModelForCleaner(CommentsRequestModel model, int commentId, int cleanerId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CommentsRequestModel, CommentsByCleanerResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CommentsByCleanerResponseModel>(model);
            responseModel.Id = commentId;
            responseModel.CleanerId = cleanerId;
            responseModel.ClientId = null;
            return responseModel;
        }
    }
}
