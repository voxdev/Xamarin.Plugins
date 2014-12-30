using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Phone.Tasks;
using StoreRating.Forms.Plugin.Abstractions;

[assembly: Dependency(typeof(StoreRating.Forms.Plugin.WindowsPhone.SendMailService))]

namespace StoreRating.Forms.Plugin.WindowsPhone
{
    /// <summary>
    /// Windows native email control
    /// </summary>
    public class SendMailService : ISendMailService
    {
        /// <summary>
        /// Windows Open the native email control
        /// </summary>
        public void ShowDraft(string subject, string body, string[] to, string[] cc, string[] bcc)
        {
            var task = new EmailComposeTask()
            {
                Subject = subject,
                Body = body,

            };

            var stringBuilder = new StringBuilder();

            if (to != null && to.Any())
            {
                foreach (var t in to)
                {
                    stringBuilder.Append(t);
                    stringBuilder.Append(";");
                }

                task.To = stringBuilder.ToString();
                stringBuilder.Clear();
            }

            if (cc != null && cc.Any())
            {
                foreach (var c in cc)
                {
                    stringBuilder.Append(c);
                    stringBuilder.Append(";");
                }

                task.Cc = stringBuilder.ToString();
                stringBuilder.Clear();
            }

            if (bcc != null && bcc.Any())
            {
                foreach (var b in bcc)
                {
                    stringBuilder.Append(b);
                    stringBuilder.Append(";");
                }

                task.Bcc = stringBuilder.ToString();
                stringBuilder.Clear();
            }

            task.Show();

        }
        /// <summary>
        /// Windows dummy initialisation
        /// </summary>
        public static void Init() { }
    }
}
