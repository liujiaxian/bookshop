﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   邮件发送助手类  0.10
//   Verion:0.10
//   Description:通过SMTP发送邮件
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace BookShop.Common
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// 邮件发送助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    /// </remarks>
    public static class MailHelper
    {
        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="copyTos">抄送地址列表</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string [] smtp,string to, string subject, string mailBody, params string[] copyTos)
        {
            return Send(smtp,new[] { to }, subject, mailBody, copyTos, new string[] { }, MailPriority.Normal);
        }

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="tos">收件人地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="ccs">抄送地址列表</param>
        /// <param name="bccs">密件抄送地址列表</param>
        /// <param name="priority">此邮件的优先级</param>
        /// <param name="attachments">附件列表</param>
        /// <returns>是否发送成功</returns>
        /// <exception cref="System.ArgumentNullException">attachments</exception>
        public static bool Send(string[] smtp,string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments)
        {
            if (attachments == null) throw new ArgumentNullException("attachments");
            if (tos.Length == 0) return false;
            //创建Email实体
            var message = new MailMessage();
            message.From = new MailAddress(smtp[3], smtp[4]);
            message.Subject = subject;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = priority;
            //插入附件
            foreach (var attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }
            //插入收件人地址,抄送地址和密件抄送地址
            foreach (var to in tos.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.To.Add(new MailAddress(to));
            }
            foreach (var cc in ccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(cc));
            }
            foreach (var bcc in bccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(bcc));
            }
            //创建SMTP客户端
            var client = new SmtpClient
            {
                Host = smtp[0],
                Credentials = new System.Net.NetworkCredential(smtp[3], smtp[5]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = smtp[2].ToBoolean(),
                Port = smtp[1].ToInt32()
            };
            //client.SendCompleted += Client_SendCompleted;
            //try
            //{
            //发送邮件
            client.Send(message);
            //client.SendAsync(message,DateTime.Now.ToString());

            //client.Dispose();
            //message.Dispose();
            return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
