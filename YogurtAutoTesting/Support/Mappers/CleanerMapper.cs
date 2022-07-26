﻿using AutoMapper;
using YogurtAutoTesting.Models.Request;
using YogurtAutoTesting.Models.Response;

namespace YogurtAutoTesting.Support.Mappers
{
    public class CleanerMapper
    {
        public CleanerResponseModel MappCleanerRequestModelToCleanerResponseModel(CleanerRequestModel model, int id, DateTime date, List<ServicesResponseModel> services)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CleanerRequestModel, CleanerResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleanerResponseModel>(model);
            responseModel.Id = id;
            responseModel.DateOfStartWork = date;
            responseModel.Services = services;

            return responseModel;
        }

        public CleanerResponseModel MappUpdateCleanerRequestModelToCleanerResponseModel(UpdateCleanerRequestModel model, DateTime date)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCleanerRequestModel, CleanerResponseModel>());
            Mapper mapper = new Mapper(config);
            var responseModel = mapper.Map<CleanerResponseModel>(model);
            responseModel.DateOfStartWork = date;
            return responseModel;
        }
    }
}
