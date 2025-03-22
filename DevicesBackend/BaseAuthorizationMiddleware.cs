using DevicesBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace DevicesBackend
{
    public class BaseAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public BaseAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserService userService)
        {
            StringValues base64AuthorizationHeader = context.Request.Headers["Authorization"];
            if (StringValues.IsNullOrEmpty(base64AuthorizationHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Not authorized. Authorization header is null or zero");
                return;
            }
            else
            {
                string encodedAuthorizationHeader = base64AuthorizationHeader.ToString();

                if (encodedAuthorizationHeader.StartsWith("Basic "))
                {
                    string encodedAuthorizationHeaderCredentials = encodedAuthorizationHeader.Substring("Basic ".Length).Trim();

                    byte[] decodedAuthorizationHeaderCredentials = Convert.FromBase64String(encodedAuthorizationHeaderCredentials);

                    string decodedString = System.Text.Encoding.UTF8.GetString(decodedAuthorizationHeaderCredentials);
                    string[] pair = decodedString.Split(':');
                    string login = pair[0];
                    string password = pair[1];
                    if(await userService.CheckLoginAndPassword(login, password))
                        await _next(context);
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Not authorized. Incorrect login or password");
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                    return;
                }
            }
        }
    }

    public static class BaseAuthorizationMiddlewareExtension
    {
        public static IApplicationBuilder UseBaseAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BaseAuthorizationMiddleware>();
        }
    }



}
