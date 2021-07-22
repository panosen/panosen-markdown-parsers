using System;
using System.Collections.Generic;
using System.Text;

using Panosen.Collections.Generic;
using static Panosen.Markdown.Parser2.LineParser;

namespace Panosen.Markdown.Parser2
{
    public class StatusMachine
    {
        public Matrix<Status, Status, string> xx = new Matrix<Status, Status, string>();

        public string GetAction(Status currentStatus, Status nextStatus)
        {
            return null;
        }
    }
}
