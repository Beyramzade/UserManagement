﻿using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace Usermanagement.Domain.User
{
    public class ActivationCode
    {
        public ObjectId _id { get; set; }
        public string Email { get; private set; }
        public string Code { get; private set; }
        public long TimeStamp { get; private set; }
        public bool IsActive { get; set; }

        public ActivationCode()
        {

        }

        [JsonConstructor]
        public ActivationCode(string email, string code)
        {
            Email = email;
            Code = code;

        }

        public ActivationCode(string email)
        {
            Email = email;
            IsActive = true;
        }

        public void GenerateCode(bool isTest)
        {
            if (isTest)
                Code = "123456";
            else
                Code = GenerateOtpCode();
        }
        public void SetTime(DateTime datetime)
        {
            TimeStamp = ToTimeStamp(datetime);
        }

        public string GenerateOtpCode()
        {
            return $"{new Random().Next(100000, 999999)}";
        }

        public long ToTimeStamp(DateTime datetime)
        {
            return (long)datetime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
