using Celula.Extensions.ActionResults;
using Celula.ViewModel.Shared;
using System;
using System.Text;
using System.Web.Mvc;

namespace Celula.Extensions.Controllers
{
    public class ApiBaseController : Controller
    {

        protected string GetApiKey()
        {
            string reply = "";

            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader == null || authHeader.Length == 0 || !authHeader.StartsWith("Basic")) return "";

            string base64Credentials = authHeader.Substring(6);
            string[] apiKey = Encoding.ASCII.GetString(Convert.FromBase64String(base64Credentials)).Split(new char[] { ':' });

            if (apiKey != null && apiKey.Length > 1)
                reply = apiKey[0];

            return reply;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (!(data is JsonResponse))
                throw new ArgumentException("Must return a JsonResponse object");

            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        protected JsonResult JsonRaw(object data)
        {
            return new JsonResult() { Data = data };
        }


        protected JsonResult JsonSuccess()
        {
            return this.JsonSuccess(null);
        }

        protected JsonResult JsonSuccess(object data)
        {
            var result = JsonResponse.Ok(data);
            return Json(result);
        }
        protected JsonResult JsonError(string message)
        {
            var result = JsonResponse.Error(message);
            return Json(result);
        }
    }
}