using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.WebApp.Models
{
    public sealed class QuartzApp
    {
        private static readonly QuartzApp quartzApp = new QuartzApp();
        private QuartzApp()
        {

        }
        public static QuartzApp GetInstance()
        {
            return quartzApp;
        }
        public void StartQuartz()
        {
            IScheduler sched;
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();
            JobDetail job = new JobDetail("job1", "group1", typeof(IndexJob));//IndexJob为实现了IJob接口的类
            DateTime ts = TriggerUtils.GetNextGivenSecondDate(null, 5);//5秒后开始第一次运行
            TimeSpan interval = TimeSpan.FromSeconds(5);//每隔1小时执行一次
            Trigger trigger = new SimpleTrigger("trigger1", "group1", "job1", "group1", ts, null,
                                                    SimpleTrigger.RepeatIndefinitely, interval);//每若干小时运行一次，小时间隔由appsettings中的IndexIntervalHour参数指定

            sched.AddJob(job, true);
            sched.ScheduleJob(trigger);
            sched.Start();
        }
        public void StartQuartzRatings()
        {
            IScheduler sched;
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();
            JobDetail job = new JobDetail("job2", "group2", typeof(IndexJobRatings));//IndexJob为实现了IJob接口的类
            DateTime ts = TriggerUtils.GetNextGivenSecondDate(null, 5);//5秒后开始第一次运行
            TimeSpan interval = TimeSpan.FromSeconds(20);//每隔1小时执行一次
            Trigger trigger = new SimpleTrigger("trigger2", "group2", "job2", "group2", ts, null,
                                                    SimpleTrigger.RepeatIndefinitely, interval);//每若干小时运行一次，小时间隔由appsettings中的IndexIntervalHour参数指定

            sched.AddJob(job, true);
            sched.ScheduleJob(trigger);
            sched.Start();
        }
    }
}