using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreRating.Forms.Plugin.Abstractions
{
    /// <summary>
    /// Interface for sending native email
    /// </summary>
    public interface ISendMailService
    {
        /// <summary>
        /// Open the native email control
        /// </summary>
        void ShowDraft(string subject, string body, string[] to, string[] cc, string[] bcc);
    }
}
