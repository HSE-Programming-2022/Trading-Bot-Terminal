from SuperTrends import supertrendrequests

def supertrend_flat_check(value1):

    totalinfo = []
    totalinfo += supertrendrequests(5, value1)

    count = 0

    for i in range(len(totalinfo)):

        if 0.995 < totalinfo[i]//totalinfo[0] < 1.005:
            count += 1
        else:
            return "Market is not in a flat possition"

            break


    if count == len(totalinfo):
        print('Close possition found')
        return True
    else:
        print('No Close possition found')
        return False







supertrend_flat_check('BTC/USDT')