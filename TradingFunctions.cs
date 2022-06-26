using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using CryptoTerminal.Core;

namespace CryptoTerminal
{
    //Done
    public class TradingFunctions
    {

        public static int[] SuperTrendAPICall(int n, string m)
        {
            var URL = new UriBuilder("https://api.taapi.io/supertrend");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["secret"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbHVlIjoiNjJiMTlhY2Y2YzI4YjU1Y2Q1ODQ4OWM4IiwiaWF0IjoxNjU1OTY3ODI0LCJleHAiOjMzMTYwNDMxODI0fQ.y-SLYp6uSnX25ws7cLNWG-idLjvbj0qW8M-qvMGs5Go";
            queryString["exchange"] = "binance";
            queryString["symbol"] = m;
            queryString["interval"] = "1h";
            queryString["backtracks"] = Convert.ToString(n);

            URL.Query = queryString.ToString();

            var client = new WebClient();

            string[] firstfilter = client.DownloadString(URL.ToString()).Split(new char[] { ',', '.', ':' });

            /*
            foreach (var item in firstfilter)
            {
                Console.WriteLine(item);
            }
            */

            int infotypecount = 7 * n;

            string[] secondfilter = new string[n];
                       
            
            int count = 0;
            for (int i = 1; i < infotypecount; i+=7)
            {
                string[] filter = new string[n];
                secondfilter[count] += firstfilter[i];
                count++;                
            }

            /*
            foreach (var item in secondfilter)
            {
                Console.WriteLine(item);
            }
            */

            int[] thirdfilter = new int[n];
            for (int i = 0; i < n; i++)
            {
                thirdfilter[i] = Convert.ToInt32(secondfilter[i]);
            }

            /*
            foreach (var item in thirdfilter)
            {
                Console.WriteLine(Convert.ToString(item));
            }
            */

            return thirdfilter;
            
            
            
        }

        public static int[] GetCoinCourse(int n, string m)
        {
            var URL = new UriBuilder("https://api.taapi.io/candle");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["secret"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbHVlIjoiNjJiMTlhY2Y2YzI4YjU1Y2Q1ODQ4OWM4IiwiaWF0IjoxNjU1OTY3ODI0LCJleHAiOjMzMTYwNDMxODI0fQ.y-SLYp6uSnX25ws7cLNWG-idLjvbj0qW8M-qvMGs5Go";
            queryString["exchange"] = "binance";
            queryString["symbol"] = m;
            queryString["interval"] = "1h";
            queryString["backtracks"] = Convert.ToString(n);

            URL.Query = queryString.ToString();

            var client = new WebClient();

            string[] firstfilter = client.DownloadString(URL.ToString()).Split(new char[] { ',', '.', ':' });

            /*
            foreach (var item in firstfilter)
            {
                Console.WriteLine(item);
            }
            */

            int infotypecount = 23 * n;

            string[] secondfilter = new string[n];

            int count = 0;

            for (int i = 16; i < infotypecount; i += 23)
            {
                secondfilter[count] += firstfilter[i];
                count++;
            }

            /*
            foreach (var item in secondfilter)
            {
                Console.WriteLine(item);
            }
            */

            int[] thirdfilter = new int[n];

            for (int i = 0; i < n; i++)
            {
                thirdfilter[i] = Convert.ToInt32(secondfilter[i]);
            }

            /*
            foreach (var item in thirdfilter)
            {
                Console.WriteLine(Convert.ToString(item));
            }
            */
            
            return thirdfilter;
        }


        public static string SuperTrendFlatCheck(string m)
        {
            int[] supertrandvalues = SuperTrendAPICall(5, m);

            int count = 0;

            for (int i = 1; i < supertrandvalues.Length; i++)
            {
                if (99 * supertrandvalues[i] < 100 * supertrandvalues[0] && 101 * supertrandvalues[i] > 100 * supertrandvalues[0])
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            if (count == supertrandvalues.Length)
            {
                //Console.WriteLine("Close possition found");
                return "True";
            }
            else
            {
                //Console.WriteLine("No Close possition found");
                return "False";
            }
        }

        public static string EntryPossitionCheck(string m)
        {
            int[] supertrandvalues = SuperTrendAPICall(2, m);

            int[] coincoursevalues = GetCoinCourse(2, m);

            if (supertrandvalues[1] < coincoursevalues[1] && supertrandvalues[0] > coincoursevalues[0])
            {
                //Console.WriteLine("Short entry possition found");
                return "Short";
            }
            else if (supertrandvalues[1] > coincoursevalues[1] && supertrandvalues[0] < coincoursevalues[0])
            {
                //Console.WriteLine("Long entry possition found");
                return "Long";
            }
            else
            {
                //Console.WriteLine("No entry possition found");
                return "Nothing";
            }


     
        }

        public static void Request(object obj)
        {

            string n = "";

            string sendentry = "";
            string sendexit = "";
            

            if (obj is string m)
            {
                n += obj;
            }

            string messege = n + ": ";

            string infoline = "";
            
            infoline += EntryPossitionCheck(n);
            infoline += SuperTrendFlatCheck(n);

            //Console.WriteLine(infoline);

            if (infoline.Contains("Short"))
            {
                Console.WriteLine("Found " + n +" short entry possition!");
                sendentry += "Found " + n + " short entry possition!";
                //Email.SendSignal(Email.Mail, sendentry);
            }
            else if (infoline.Contains("Long"))
            {
                Console.WriteLine("Found " + n + " long entry possition!");
                sendentry += "Found " + n + " long entry possition!";
                //Email.SendSignal(Email.Mail, sendentry);
            }
            else if (infoline.Contains("Nothing"))
            {
                Console.WriteLine("Found " + n + " no entry possition.");
                sendentry += "Found " + n + " no entry possition.";
                //Email.SendSignal(Email.Mail, sendentry);
            }


            if (infoline.Contains("True"))
            {
                Console.WriteLine("Found " + n + " market exit possition!");
                sendexit += " Found " + n + " market exit possition!";
                //Email.SendSignal(Email.Mail, sendexit);
            }
            else if (infoline.Contains("False"))
            {
                Console.WriteLine("Not found " + n + " no market exit possition.");
                sendexit += " Found " + n + " no market exit possition.";
                //Email.SendSignal(Email.Mail, sendexit);
            }

            messege += sendentry;
            messege += sendexit;

            Email.SendSignal(Email.Mail, messege);

            infoline = "";

        }
            

    }
}
