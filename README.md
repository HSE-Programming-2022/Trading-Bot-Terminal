# Терминал для трайдинг бота #

## Основаные части проекта: ##
1) Создание трейдинг бота для торговли 5 основными криптовалютами по определённой стратегии.
2) Разработка терминала и интеграция в него данного бота.

## Основные части были разделёны на несколько подзадач: ##
<br/> 1.1. Разработка стратегии торговли (Шевелёв Алексей)
<br/> 1.2. Поиск ресурсов для получения данных о торговых индикаторах и биржах (Шевелёв Алексей)
<br/> 1.3. Разработка парсинга данных с помощью API (Сафаров Тимур)
<br/> 1.3. Написание функций (Шевелёв Алексей, Сафаров Тимур): 
     <br/> а) по отбору сигналов на вход в рынок
     <br/> б) по отбору сигналов для выхода из рынка
     <br/> в) выставление ордеров 
     <br/> г) закрытие ордеров
<br/> 1.4. Интеграция бота в WPF терминал (Шевелёв Алексей, Никитина Александра, Амедян Эдвард)

<br/> 2.1. Разработка дизайна терминала (Никитина Александра)
<br/> 2.2. Разработка и реализация функционала (Никитина Александра, Амедян Эдвард)

## Описание стратегии торговли #

Стратегия для нашего бота будет основываться на индикаторе технического анализа - [SuperTrend](https://drive.google.com/file/d/1evQzWP0jHzyLnZlvdGdUGoz0zeJ3dKJg/view?usp=sharing).

### <br/> -Что он делает? ###
<br/> -Показывает тенденцию рынка, и позволяет отследить точки смены тренда, которые являются хорошими точками для открытия позиции.

### <br/> -Как это работает? ###
<br/> -Для реализации нашей задумки нам нужно будет исполько API двух ресурсов - 
<br/> Binance, для получения актуальной цены актива, и TapApi, для получения значений индикатора SuperTrend.

<br/>  Для анализа рынка мы будем сравнивать 3 значения - Значение текущего SuperTrend (делее cST), Значение предыдущего SuperTrend (делее pST) и Значение текущей стоимость актива (делее cP).

<br/> Для определения точка входа условия должны быть такими: 
<br/> Если [pST < cP < cST](https://drive.google.com/file/d/16iElJTz3wrKR_T_H2_6crNC2jN7kPOiW/view?usp=sharing), то это сигнал для входа в шорт - ставка на понижение цены.
<br/> Если [sST < cP < pST](https://drive.google.com/file/d/181-e9bC__bsX-WHuLqQsk_1_-mjlfU0n/view?usp=sharing), то это сигнал для входа в лонг - ставка на повышение цены.

<br/> Для опредение точки выхода из рынка условия должны быть такими:
<br/> Если [p4ST = p3ST = p2ST = p1ST = p0ST](https://drive.google.com/file/d/1ASCBzkNFEzv-SWlUrZyHR0-yAF_KqIL1/view?usp=sharing) то рынок вошёл в точку разворота тренда и ест вероятность потерять деньги. 

## Основные методы бота ##

### 1) Получение данных индикатора -SuperTrendAPICall(int n, string m) ###



```C#
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

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
```
### 2) Получение данных о курсе монеты - GetCoinCourse(int n, string m) ###

Метод GetCoinCourse(int n, string m) получает данные через API о n последних значениях закрытия свечей (ежечасный итог цены монеты) по m валютной паре, и возвращает массив с числовыми значения курса монеты, которые понадабятся в следующих методах.

```C#
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;

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
```


