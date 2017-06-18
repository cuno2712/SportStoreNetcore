﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json; 


namespace WebApplication7.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetJson<T>(this ISession session ,string key)
        {
            var sessonData = session.GetString(key);
            return sessonData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessonData);
        }
    }
}