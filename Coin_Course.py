import requests

def getcoincourse(value1, value2):
    endpoint = f"https://api.taapi.io/candle"
    coin_course = []

    parameters = {
        'secret': 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbHVlIjoiNjJiMTlhY2Y2YzI4YjU1Y2Q1ODQ4OWM4IiwiaWF0IjoxNjU1OTY3ODI0LCJleHAiOjMzMTYwNDMxODI0fQ.y-SLYp6uSnX25ws7cLNWG-idLjvbj0qW8M-qvMGs5Go',
        'exchange': 'binance',
        'symbol': value1,
        'interval': '1h',
        'backtracks': value2
    }

    response = requests.get(url=endpoint, params=parameters)
    result = response.json()

    for i in range(len(result)):
        coin_course.append(result[i]['close'])

    return coin_course

# getcoincourse('BTC/USDT', 2)
