import requests

def supertrendrequests(value1, value2):
    endpoint = f"https://api.taapi.io/supertrend"
    data_base = []

    parameters = {
        'secret': 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbHVlIjoiNjJiMTlhY2Y2YzI4YjU1Y2Q1ODQ4OWM4IiwiaWF0IjoxNjU1OTY3ODI0LCJleHAiOjMzMTYwNDMxODI0fQ.y-SLYp6uSnX25ws7cLNWG-idLjvbj0qW8M-qvMGs5Go',
        'exchange': 'binance',
        'symbol': value2,
        'interval': '1h',
        'backtracks': value1
    }

    response = requests.get(url=endpoint, params=parameters)
    result = response.json()

    for i in range(len(result)):
        data_base.append(result[i]['value'])

    #print (data_base)

    return data_base

supertrendrequests(5, 'BTC/USDT')