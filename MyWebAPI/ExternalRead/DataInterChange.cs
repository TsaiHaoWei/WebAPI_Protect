using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI.ExternalRead
{
    public class DataInterChange
    {
        public dynamic JsonFomat<T>(bool Serialize, List<T> listModel, string JsonString)
        {
            if (Serialize)
                return JsonConvert.SerializeObject(listModel);//回傳 string
            else
                return JsonConvert.DeserializeObject<List<T>>(JsonString);//反序列化;

        }


    }
}