<%@ Application Language="C#" %>
<%@ Import Namespace="CustomException" %>
<%@ Import Namespace="Quartz" %>

<script RunAt="server">
    IScheduler sched;
    private static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("UnKnown_Logger");

    void Application_Start(object sender, EventArgs e)
    {
        //log4net配置
        log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/config/log4net.config")));
        // 在应用程序启动时运行的代码
        PersistenceLayer.Setting.Instance().DatabaseMapFile = Server.MapPath("~") + @"\Config\DatabaseMap.config";
        ErrorCodeViewTipConfig.Inital(Server.MapPath("~/config/SysError.config"), Server.MapPath("~/ErrLog/"));

        ISchedulerFactory sf = new Quartz.Impl.StdSchedulerFactory();
        sched = sf.GetScheduler();
        JobDetail job = new JobDetail("job1", "group1", typeof(XiHuanJob));

        string cronExpr = ConfigurationManager.AppSettings["cronExpr"];
        CronTrigger trigger = new CronTrigger("trigger1", "group1", "job1", "group1", cronExpr);
        sched.AddJob(job, true);
        DateTime ft = sched.ScheduleJob(trigger);
        sched.Start();
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //string oldUrl = HttpContext.Current.Request.RawUrl;
        //string pattern = @"^(.+)xh/(\d+)\.html(\?.*)*$";
        //string replace = "$1xh.aspx?id=$2";
        //if (Regex.IsMatch(oldUrl, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled))
        //{
        //    string newUrl = Regex.Replace(oldUrl, pattern, replace, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //    this.Context.RewritePath(newUrl);
        //}
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码
        if (sched != null)
        {
            sched.Shutdown(true);
        }

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

        //log4net错误日志记录

        Exception exception = this.Server.GetLastError().InnerException;

        if (exception != null)
        {
            LogError.Error(exception.Message);
            CustomException customException = null;
            if (exception is CustomException)
            {
                customException = (CustomException)exception;
            }
            else
            {
                customException = new CustomException(ErrorCode.Unknown, "未知异常：" + exception.Message, exception);
            }

            ErrorCodeViewTip tipInfo = ErrorCodeViewTipConfig.GetViewTip(customException.CurrentErrorCode);
            CustomExceptionLogHelper.WriteLogToXML(Guid.NewGuid().ToString(), customException, this.Request.Url.ToString());
        }

    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。
    }
   
</script>

