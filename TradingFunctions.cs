using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

namespace TradingTerminal
{
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

            string[] firstfilter = client.DownloadString(URL.ToString()).Split(new char[] { ',' } );

            int infotypecount = 3 * n;

            string[] secondfilter = new string[n];

            int count = 0;
            for (int i = 0; i < infotypecount; i+=3)
            {
                string[] filter = new string[n];
                
                if (i == 0)
                {
                    filter[count] += firstfilter[i].Substring(10);
                    secondfilter[count] += filter[count].Substring(0, 5);
                    count++;
                }
                else if (i != 0)
                {
                    filter[count] += firstfilter[i].Substring(9);
                    secondfilter[count] += filter[count].Substring(0, 5);
                    count++;
                }
            }

            int[] thirdfilter = new int[n];
            for (int i = 0; i < n; i++)
            {
                thirdfilter[i] = Convert.ToInt32(secondfilter[i]);
            }
            /*
            foreach (var item in thirdfilter)
            {
                Console.WriteLine(item);       
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

            string[] firstfilter = client.DownloadString(URL.ToString()).Split(new char[] { ',' });
            
            int infotypecount = 8 * n;
            
            string[] secondfilter = new string[n];

            int count = 0;
            for (int i = 5; i < infotypecount ; i += 8)
            {
                string[] filter = new string[n];
                filter[count] += firstfilter[i].Substring(8);
                secondfilter[count] += filter[count].Substring(0, 5);
                count++;
            }
            
            int[] thirdfilter = new int[n];
            for (int i = 0; i < n; i++)
            {
                thirdfilter[i] = Convert.ToInt32(secondfilter[i]);
            }
            /*
            foreach (var item in thirdfilter)
            {
                Console.WriteLine(item);
            }
            */
            return thirdfilter;
        }

        public static bool SuperTrendFlatCheck(string m)
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
                Console.WriteLine("Close possition found");
                return true;
            }
            else
            {
                Console.WriteLine("No Close possition found");
                return false;
            }
        }

        public static string EntryPossitionCheck(string m)
        {
            int[] supertrandvalues = SuperTrendAPICall(2, m);

            int[] coincoursevalues = GetCoinCourse(2, m);

            if (supertrandvalues[1] < coincoursevalues[1] && supertrandvalues[0] > coincoursevalues[0])
            {
                Console.WriteLine("Short entry possition found");
                return "Short";
            }
            else if (supertrandvalues[1] > coincoursevalues[1] && supertrandvalues[0] < coincoursevalues[0])
            {
                Console.WriteLine("Long entry possition found");
                return "Long";
            }
            else
            {
                Console.WriteLine("No entry possition found");
                return "Nothing";
            }

        }
    }
}
