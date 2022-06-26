using System;
using System.Net;
using System.Web;
using System.Threading;
using System.IO;
using System.Timers;

namespace CryptoTerminal
{
    public class ThreadView
    {
        public static void ThreadOnAir(object obj)
        {

            Thread Pair = new Thread(TradingFunctions.Request);
            Pair.Start(obj);

            Thread.Sleep(1800000); //1800000 30000

            Pair.Interrupt();

            Pair.Abort();

            
            ThreadOnAir(obj);
          
        }
    }
}
