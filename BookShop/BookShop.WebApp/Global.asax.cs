using BookShop.WebApp.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookShop.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息

            SearchIndexManager.GetInstance().StartThread();//开启线程
            QuartzApp.GetInstance().StartQuartz();//开启定时任务
            QuartzApp.GetInstance().StartQuartzRatings();//开启定时任务 执行生成离线评分文件

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //开启线程 将错误信息写到日志文件里
            //设置记录错误信息的文件路径
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    //判断队列中有没有异常信息
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {
                        //接收异常信息
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            //创建文件并记录文件的完整路径
                            //string fullPath = filePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            //将错误信息追加到.txt的文件里
                            // File.AppendAllText(fullPath,ex.ToString(),Encoding.Default);       
                            ILog logger = LogManager.GetLogger("errorMsg");//参数可以是类型也可以是个字符串，只是个标识
                            logger.Error(ex);
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//如果没有信息 就让线程休息3秒钟 防止CPU的空转 
                    }
                }
            },filePath);
        }
    }
}