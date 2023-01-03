using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KENGINE
{
    public class Debug
    {
        public static Action<string>? OnLog;
        public static void Log(string message)
        {
            if (OnLog != null)
            {
                OnLog.Invoke(message);
            }
        }
    }
}
