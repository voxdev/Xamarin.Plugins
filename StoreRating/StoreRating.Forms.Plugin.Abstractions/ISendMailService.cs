using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRating.Forms.Plugin.Abstractions
{
    public interface ISendMailService
    {
        void ShowDraft(string subject, string body, string[] to, string[] cc, string[] bcc);
    }
}
