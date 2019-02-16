using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace $ext_safeprojectname$.API.Middleware
{
    public class HttpContextMiddleware
    {
        const string MessageTemplate =
            "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms [User:{User}][Protocol:{RequestProtocol}][Host:{RequestHost}][Referer:{Referer}][User-Agent:{UserAgent}]";

        const string ErrorMessageTemplate =
           "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms [User:{User}][Protocol:{RequestProtocol}][Host:{RequestHost}][Headers:{RequestHeaders}]";


        readonly RequestDelegate _next;

        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var userName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "unknown";

            LogContext.PushProperty("User", !string.IsNullOrWhiteSpace(userName) ? userName : null);

            var sw = Stopwatch.StartNew();
            try
            {
                await _next(httpContext);
                sw.Stop();
                PushProperties(sw.Elapsed.TotalMilliseconds, httpContext);
                Log.Information(MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, httpContext.Response.StatusCode, sw.Elapsed.TotalMilliseconds);
            }            
            catch (Exception ex)
            {
                sw.Stop();
                var errorId = Guid.NewGuid();
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                PushProperties(sw.Elapsed.TotalMilliseconds, httpContext, errorId);
                Log.Error(ex, ErrorMessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { errorId }));
            }
        }

        private static void PushProperties(double elapsed, HttpContext httpContext, Guid? errorId = null)
        {
            var requestHeader = httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString());
            LogContext.PushProperty("Elapsed", elapsed);
            LogContext.PushProperty("RequestHeaders", requestHeader, destructureObjects: true);
            LogContext.PushProperty("Referer", requestHeader.ContainsKey("Referer") ? requestHeader["Referer"] : "");
            LogContext.PushProperty("UserAgent", requestHeader.ContainsKey("User-Agent") ? requestHeader["User-Agent"] : "");
              
            LogContext.PushProperty("RequestHost", httpContext.Request.Host);
            LogContext.PushProperty("RequestProtocol", httpContext.Request.Protocol);
            if (errorId != null)
            {
                LogContext.PushProperty("ErrorId", errorId);
            }
        }
    }
}


