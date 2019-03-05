using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace PostsCommentsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region nlog
            //var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            //try
            //{
            //    CreateWebHostBuilder(args).Build().Run();
            //}
            //catch (Exception e)
            //{
            //    //NLog: catch setup errors
            //    logger.Error(e, "Stopped program because of exception");
            //    throw;
            //}
            //finally
            //{
            //    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            //    NLog.LogManager.Shutdown();
            //}

            #endregion

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        #region nlog
        //.ConfigureLogging(logging =>
        //{
        //    logging.ClearProviders();
        //    logging.SetMinimumLevel(LogLevel.Trace);
        //});

        // .UseNLog();
        #endregion

    }
}
