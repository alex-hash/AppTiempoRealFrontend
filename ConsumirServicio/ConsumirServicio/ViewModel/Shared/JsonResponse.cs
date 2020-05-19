using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Celula.ViewModel.Shared
{
    public class JsonResponse
    {
        public bool success { get; set; }

        [JsonProperty("data")]
        public object data { get; set; }

        private JsonResponse()
        {

        }

        public static JsonResponse Ok(object result)
        {
            return new JsonResponse() { data = result, success = true };
        }
        public static JsonResponse Error(string message)
        {
            return new JsonResponse() { success = false, data = new { message = message } };
        }
    }
}