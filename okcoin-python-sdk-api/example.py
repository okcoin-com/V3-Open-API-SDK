import okcoin.account_api as account
import okcoin.lever_api as lever
import okcoin.spot_api as spot
import json
import logging
import datetime

log_format = '%(asctime)s - %(levelname)s - %(message)s'
logging.basicConfig(filename='mylog-rest.json', filemode='a', format=log_format, level=logging.INFO)


def get_timestamp():
    now = datetime.datetime.now()
    t = now.isoformat("T", "milliseconds")
    return t + "Z"


time = get_timestamp()

if __name__ == '__main__':

    api_key = ""
    secret_key = ""
    passphrase = ""

# param use_server_time's value is False if is True will use server timestamp

# account api test
# 钱包API
    accountAPI = account.AccountAPI(api_key, secret_key, passphrase, False)
    # 获取币种列表
    # result = accountAPI.get_currencies()
    # 钱包账户信息
    # result = accountAPI.get_wallet()
    # 单一币种账户信息
    # result = accountAPI.get_currency('btc')
    # 资金划转
    # result = accountAPI.coin_transfer('ltc', '0.1', '3', '9')
    # 提币
    # result = accountAPI.coin_withdraw('XRP', 1, 4, '', "", 0.0005)
    # 提币手续费
    # result = accountAPI.get_coin_fee('btc')
    # 获取账户资产估值
    # result = accountAPI.get_asset_valuation()
    # 查询最近所有币种的提币记录
    # result = accountAPI.get_coins_withdraw_record()
    # 查询单个币种提币记录
    # result = accountAPI.get_coin_withdraw_record('btc')
    # 账单流水查询
    # result = accountAPI.get_ledger_record()
    # 获取充值地址
    # result = accountAPI.get_top_up_address('btc')
    # 获取所有币种充值记录
    # result = accountAPI.get_top_up_records()
    # 获取单个币种充值记录
    # result = accountAPI.get_top_up_record('btc')

    # print(time + json.dumps(result))
    # logging.info("result:" + json.dumps(result))

# spot api test
# 币币API
    spotAPI = spot.SpotAPI(api_key, secret_key, passphrase, False)
    # 币币账户信息 （20次/2s）
    # result = spotAPI.get_account_info()
    # 单一币种账户信息 （20次/2s）
    # result = spotAPI.get_coin_account_info('btc')
    # 账单流水查询 （最近3个月的数据）（20次/2s）
    # result = spotAPI.get_ledger_record('btc')
    # 下单 （100次/2s）
    # result = spotAPI.take_order('', 'buy', client_oid='', type='market', price='0.2717', order_type='0', notional='1', size='')

    # take orders
    # 批量下单 （每次只能下最多4个币对且每个币对可批量下10个单）（50次/2s）
    # params = [
    #   {"instrument_id": "", "side": "buy", "type": "limit", "price": "0.2717", "size": "1"},
    #   {"instrument_id": "", "side": "buy", "type": "market", "price": "2.5451", "notional": "1"}
    # ]
    # result = spotAPI.take_orders(params)
    # 撤消指定订单 （100次/2s）
    # result = spotAPI.revoke_order('', order_id='4351604261981184')

    # revoke orders
    # 批量撤消订单 （每次只能下最多4个币对且每个币对可批量下10个单）（50次/2s）
    # params = [
    #     {'instrument_id': '', 'order_ids': ['4351631930908672','4351631930908673']}
    # ]
    # result = spotAPI.revoke_orders(params)
    # 获取订单列表 （最近3个月的订单信息）（20次/2s）
    # result = spotAPI.get_orders_list('', '')
    # 获取所有未成交订单 （20次/2s）
    # result = spotAPI.get_orders_pending('')
    # 获取订单信息 （已撤销的未成交单只保留2个小时）（20次/2s）
    # result = spotAPI.get_order_info('')
    # 获取成交明细 （最近3个月的数据）（20次/2s）
    # result = spotAPI.get_fills('', '')
    # 获取币对信息 （20次/2s）
    # result = spotAPI.get_coin_info()
    # 获取深度数据 （20次/2s）
    # result = spotAPI.get_depth('')
    # 获取全部ticker信息 （50次/2s）
    # result = spotAPI.get_ticker()
    # 获取某个ticker信息 （20次/2s）
    # result = spotAPI.get_specific_ticker('')
    # 获取成交数据 （最近60条数据）（20次/2s）
    # result = spotAPI.get_deal('', '')
    # 获取K线数据（最多可获取最近2000条）（20次/2s）
    # result = spotAPI.get_kline('', 60)

    # print(time + json.dumps(result))
    # logging.info("result:" + json.dumps(result))

# level api test
# 币币杠杆API
    levelAPI = lever.LeverAPI(api_key, secret_key, passphrase, False)
    # 币币杠杆账户信息 （20次/2s）
    # result = levelAPI.get_account_info()
    # 单一币对账户信息 （20次/2s）
    # result = levelAPI.get_specific_account('')
    # 账单流水查询 （最近3个月的数据）（20次/2s）
    # result = levelAPI.get_ledger_record('')
    # 杠杆配置信息 （20次/2s）
    # result = levelAPI.get_config_info()
    # 某个杠杆配置信息 （20次/2s）
    # result = levelAPI.get_specific_config_info('')
    # 获取借币记录 （20次/2s）
    # result = levelAPI.get_borrow_coin()
    # 某币对借币记录 （20次/2s）
    # result = levelAPI.get_specific_borrow_coin('')
    # 借币 （100次/2s）
    # result = levelAPI.borrow_coin('', '', '', '')
    # 还币 （100次/2s）
    # result = levelAPI.repayment_coin('', '', '')
    # 下单 （100次/2s）
    # result = levelAPI.take_order('', '', '', price='', size='')

    # take orders
    # params = [
    #   {"instrument_id": "", "side": "buy", "type": "market", "notional": "2", "margin_trading": "2"},
    #   {"instrument_id": "", "side": "sell", 'price': '0.2806', "size": "5", "margin_trading": "2"}
    # ]
    # 批量下单 （每次只能下最多4个币对且每个币对可批量下10个单）（50次/2s）
    # result = levelAPI.take_orders(params)
    # 撤销指定订单 （100次/2s）
    # result = levelAPI.revoke_order('', order_id='')

    # revoke orders
    # params = [
    #   {"instrument_id": "", "order_ids": ['23464', '23465']},
    #   {"instrument_id": "", "client_oids": ['243464', '234465']}
    # ]
    # 批量撤销订单 （每个币对可批量撤10个单）（50次/2s）
    # result = levelAPI.revoke_orders(params)

    # 获取订单列表 （最近100条订单信息）（20次/2s）
    # result = levelAPI.get_order_list('', state='0')
    # 获取杠杆倍数 （5次/2s）
    # result = levelAPI.get_leverage('')
    # 设置杠杆倍数 （5次/2s）
    # result = levelAPI.set_leverage('', '')
    # 获取订单信息 （已撤销的未成交单只保留2个小时）（20次/2s）
    # result = levelAPI.get_order_info('', '')
    # 获取所有未成交订单 （20次/2s）
    # result = levelAPI.get_order_pending('')
    # 获取成交明细 （最近3个月的数据）（20次/2s）
    # result = levelAPI.get_fills('', '')
    # 公共-获取标记价格 （20次/2s）
    # result = levelAPI.get_mark_price('')


    print(time + json.dumps(result))
    logging.info("result:" + json.dumps(result))
