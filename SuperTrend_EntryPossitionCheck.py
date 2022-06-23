from SuperTrends import supertrendrequests
from Coin_Course import getcoincourse


def entryposscheck(value1):
    supertrendinfo = []
    supertrendinfo += supertrendrequests(2, value1)

    courseinfo = []
    courseinfo += getcoincourse(value1, 2)

    if supertrendinfo[1] < courseinfo[1] and supertrendinfo[0] > courseinfo[0]:
        print('Short entry possition found')
        return 'Short'

    elif supertrendinfo[1] > courseinfo[1] and supertrendinfo[0] < courseinfo[0]:
        print('Long entry possition found')
        return 'Long'

    else:
        print('No entry possition found')
        return


entryposscheck('BTC/USDT')
