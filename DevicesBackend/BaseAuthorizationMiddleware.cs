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
            try
            {
                StringValues base64AuthorizationHeader = context.Request.Headers["Authorization"];
                if (StringValues.IsNullOrEmpty(base64AuthorizationHeader))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Not authorized. Authorization header is null or zero");
                    return;
                }

                string encodedAuthorizationHeader = base64AuthorizationHeader.ToString();
                if (encodedAuthorizationHeader.StartsWith("Basic ") == false)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Not authorized. Authorization header must start with Basic");
                    return;
                }

                string encodedAuthorizationHeaderCredentials = encodedAuthorizationHeader.Substring("Basic ".Length).Trim();
                byte[] decodedAuthorizationHeaderCredentials = Convert.FromBase64String(encodedAuthorizationHeaderCredentials);
                string decodedString = System.Text.Encoding.UTF8.GetString(decodedAuthorizationHeaderCredentials);

                if (decodedString.Contains(':') == false)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Not authorized. Authorization header must contains a login password pair");
                    return;
                }

                string[] pair = decodedString.Split(':');
                string login = pair[0];
                string password = pair[1];

                if (await userService.CheckLoginAndPassword(login, password) == false)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Not authorized. Incorrect login or password");
                    return;
                }


                await _next(context);
            }
            catch (FormatException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Not authorized. Authorization header contains an incorrect base64");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Not authorized. An internal error occurred");
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
