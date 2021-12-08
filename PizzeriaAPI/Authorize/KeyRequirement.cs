using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace PizzeriaAPI.Authorize
{
    [AttributeUsage(AttributeTargets.Class)]
    public class KeyRequirement : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("APIKey", out authTokens);

                var _token = authTokens.FirstOrDefault();

                if (_token != null)
                {
                    if (_token != null)
                    {
                        if (!IsValidToken(_token)) { 
                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid key"
                                },
                            };
                        }
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide key";
                    filterContext.Result = new JsonResult("Please Provide key")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide API Key"
                        },
                    };
                }
            }
        }

        public bool IsValidToken(string authToken)
        {
            return authToken == "b+zcArCc+BPkJVljCq5PNg==";
        }
    }
}
