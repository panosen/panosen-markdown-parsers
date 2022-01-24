using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Markdown.Helper
{
    public class Constant
    {
        /// <summary>
        /// Gets a list of URL schemes.
        /// </summary>
        public static List<string> KnownSchemes { get; private set; } = new List<string>()
        {
            "http",
            "https",
            "ftp",
            "steam",
            "irc",
            "news",
            "mumble",
            "ssh",
            "ms-windows-store",
            "sip"
        };
    }
}
