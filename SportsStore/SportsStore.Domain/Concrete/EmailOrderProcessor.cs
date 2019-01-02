using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsstore@examle.com";
        public bool UseSsl = true;
        public string Username = "MyStmpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\sports_store_emails";
    }

    public class EmailOrderProcessor:IOrderProcessor
    {
        private EmailSettings _emailSettings;

        public EmailOrderProcessor(EmailSettings P_Settings)
        {
            _emailSettings = P_Settings;
        }

        public void ProcessOrder(Cart P_Cart, ShippingDetails P_ShippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = _emailSettings.UseSsl;
                smtpClient.Host = _emailSettings.ServerName;
                smtpClient.Port = _emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                if (_emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = _emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder builder = new StringBuilder().AppendLine("A new order has been submitted")
                    .AppendLine("---").AppendLine("Items:");

                foreach (var line in P_Cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    builder.AppendFormat("{0}*{1}(subtotal:{2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                builder.AppendFormat("Total order value:{0:c}", P_Cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(P_ShippingInfo.Name)
                    .AppendLine(P_ShippingInfo.Line1)
                    .AppendLine(P_ShippingInfo.Line2 ?? "")
                    .AppendLine(P_ShippingInfo.Line3 ?? "")
                    .AppendLine(P_ShippingInfo.City)
                    .AppendLine(P_ShippingInfo.State ?? "")
                    .AppendLine(P_ShippingInfo.Country)
                    .AppendLine(P_ShippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap:{0}", P_ShippingInfo.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(_emailSettings.MailFromAddress, _emailSettings.MailToAddress
                    , "New order submitted", builder.ToString());

                if (_emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }
                smtpClient.Send(mailMessage);
            }
        }

    }


}
