/*
Project: Mentors Academy
Copyright © 2018 Solution Zone, LLC.. All rights reserved.
Author: John K Johansen

Description: Mentors Academy is a self-publishing site where students can share academic papers and discoveries.

Details: The implementation of Mentors Academy


*/



using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Builder;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Academy.Mentors.Api.Logging
{
    /// <summary>
    /// LoggingMiddleware
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly LoggingMiddlewareOptions _options;

        /// <summary>
        /// LoggingMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="options"></param>
        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, LoggingMiddlewareOptions options)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
            _options = options;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // Ignore requests from swagger page
            if (context.Request.Path.Value.Contains("swagger"))
            {
                await _next.Invoke(context);
                return;
            }

            LogMessage logMessage = new LogMessage
            {
                ApplicationName = "Academy.Mentors",
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                RequestUri = context.Request.Path,
                RequestParams = context.Request.QueryString.Value,
                RequestHttpMethod = context.Request.Method,
                LoginToken = context.Request.Query["loginToken"],

                RequestBody = new StreamReader(context.Request.Body).ReadToEnd()
            };
            byte[] requestData = Encoding.UTF8.GetBytes(logMessage.RequestBody);
            context.Request.Body = new MemoryStream(requestData);

            // reading response body to a temp var
            var bodyStream = context.Response.Body;
            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                await _next.Invoke(context);

                logMessage.LogMessageTypeId = 6; // INFO
                logMessage.StatusCode = context.Response.StatusCode;

                // reading response body from temp var
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                // saving into logMessage
                logMessage.ResponseContent = responseBody;

                // copying response body back to response body
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(bodyStream);

                SaveLogMessageAsync(_options._connectionString, logMessage);
            }
            catch (Exception ex)
            {

                try
                {
                    logMessage.ShortMessage = "Exception";
                    logMessage.Exception = ex.ToString();
                    logMessage.Trace = ex.StackTrace;
                    logMessage.LogMessageTypeId = 3; // EXCEPTION

                    // reading response body from temp var
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                    // saving into logMessage
                    logMessage.ResponseContent = responseBody;

                    // copying response body back to response body
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(bodyStream);

                    // HANDLE EXCEPTION
                    logMessage.FullMessage = JsonConvert.SerializeObject(new { error = ex.Message, stackTrace = ex.StackTrace });

                    SaveLogMessageAsync(_options._connectionString, logMessage);

                    // if you don't want to rethrow the original exception then call return:
                    // return;
                    // await HandleExceptionAsync(context, ex);
                }
                catch (Exception ex2)
                {
                    _logger.LogError(0, ex2, "An exception was thrown attempting to execute the error handler.");
                }

                // Otherwise this handler will re-throw the original exception
                throw;
            }

        }


        /// <summary>
        /// SaveLogMessageAsync
        /// </summary>
        /// <param name="_connectionString"></param>
        /// <param name="obj"></param>
        private static async void SaveLogMessageAsync(string _connectionString, LogMessage obj)
        {
            await Task.Run(() =>
            {
                string query = "INSERT INTO [LogMessages]" +
                    "([LogMessageTypeId],[ApplicationName],[ApplicationMethod],[IpAddress],[LoginToken],[ShortMessage],[RequestHttpMethod],[RequestUri],[RequestParams]," +
                    "[RequestBody],[StatusCode],[ResponseContent],[FullMessage],[Exception],[Trace],[Logged])" +
                    "VALUES(" +
                    $"{GetValue(obj.LogMessageTypeId)}, {GetValue(obj.ApplicationName)}, {GetValue(obj.ApplicationMethod)}, {GetValue(obj.IpAddress)}, {GetValue(obj.LoginToken)}," +
                    $"{GetValue(obj.ShortMessage)}, {GetValue(obj.RequestHttpMethod)}, {GetValue(obj.RequestUri)}, {GetValue(obj.RequestParams)}, {GetValue(obj.RequestBody)}," +
                    $"{GetValue(obj.StatusCode)}, {GetValue(obj.ResponseContent)}, {GetValue(obj.FullMessage)}, {GetValue(obj.Exception)}, {GetValue(obj.Trace)}, CURRENT_TIMESTAMP);";

                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    dbConnection.Open();
                    using(var insertCommand = dbConnection.CreateCommand())
                    {
                        insertCommand.CommandText = query;
                        insertCommand.ExecuteNonQuery();
                    }
                }
            });
        }

        /// <summary>
        /// Check if the object is null and then replace special characters and add quotes otherwise return null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetValue(Object obj)
        {
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return "'" + Regex.Replace(obj.ToString(), @"(@|&|'|\(|\)|<|>|#|;|:|,|{|}|\[|\]|\^|%|\$|\!|\?|/)", "\\$1") + "'";
            }
            else
            {
                return "NULL";
            }
        }

        /// <summary>
        /// Handle the exceptions here rather on GlobalExceptionFilter
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            //if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (exception is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

    }

    /// <summary>
    /// LoggingMiddlewareExtensions
    /// </summary>
    public static class LoggingMiddlewareExtensions
    {
        /// <summary>
        /// UseLoggingMiddleware
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLoggingMiddleware(
            this IApplicationBuilder builder, LoggingMiddlewareOptions options)
        {
            return builder.UseMiddleware<LoggingMiddleware>(options);
        }
    }

    /// <summary>
    /// LoggingMiddlewareOptions
    /// </summary>
    public class LoggingMiddlewareOptions
    {
        /// <summary>
        /// _connectionString
        /// </summary>
        public string _connectionString { get; set; }
    }
}
