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

Страгия для нашего бота будет основываться на индикаторе технического анализа - SuperTrend.

<br/> -Что он делает?
<br/> -Показывает тенденцию рынка, и позволяет отследить точки смены тренда, которые являются хорошими точками для открытия позиции.

<br/> -Как это работает?
<br/> -Для реализации нашей задумки нам нужно будет исполько API двух ресурсов - 
<br/> Binance, для получения актуальной цены актива, и TapApi, для получения значений индикатора SuperTrend.

<br/>  Для анализа рынка мы будем сравнивать 3 значения - Значение текущего SuperTrend (делее cST), Значение предыдущего SuperTrend (делее pST) и Значение текущей стоимость актива (делее cP).

<br/> Для определения точка входа условия должны быть такими: 
<br/> Если pST < cP < cST, то это сигнал для входа в шорт - ставка на понижение цены.
<br/> Если sST < cP < pST, то это сигнал для входа в лонг - ставка на повышение цены.

<br/> Для опредение точки выхода из рынка условия должны быть такими:
<br/> Если p4ST = p3ST = p2ST = p1ST = p0ST то рынок вошёл в точку разворота тренда и ест вероятность потерять деньги. 


