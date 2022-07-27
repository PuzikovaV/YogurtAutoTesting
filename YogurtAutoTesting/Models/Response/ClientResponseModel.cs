﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YogurtAutoTesting.Models.Response
{
    public class ClientResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("registrationDate")]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }
        public override bool Equals(object? obj)
        {
            return obj is ClientResponseModel model &&
                Id == model.Id &&
                FirstName == model.FirstName &&
                LastName == model.LastName &&
                RegistrationDate == model.RegistrationDate &&
                Email == model.Email &&
                Phone == model.Phone &&
                BirthDate == model.BirthDate;
        } 
    }
}
