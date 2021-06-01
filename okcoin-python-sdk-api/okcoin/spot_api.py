from .client import Client
from .consts import *


class SpotAPI(Client):

    def __init__(self, api_key, api_secret_key, passphrase, use_server_time=False, first=False):
        Client.__init__(self, api_key, api_secret_key, passphrase, use_server_time, first)

    # query spot account info
    def get_account_info(self):
        return self._request_without_params(GET, SPOT_ACCOUNT_INFO)

    # query specific coin account info
    def get_coin_account_info(self, currency):
        return self._request_without_params(GET, SPOT_COIN_ACCOUNT_INFO + str(currency))

    # query ledger record not paging
    def get_ledger_record(self, currency, after='', before='', limit='', tp=''):
        params = {}
        if after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit
        elif tp:
            params['type'] = tp
        return self._request_with_params(GET, SPOT_LEDGER_RECORD + str(currency) + '/ledger', params, cursor=True)

    # take order
    def take_order(self, instrument_id, side, client_oid='', type='', size='', price='', order_type='0', notional='',
                   margin_trading=''):
        params = {'instrument_id': instrument_id, 'side': side, 'client_oid': client_oid, 'type': type, 'size': size,
                  'price': price, 'order_type': order_type, 'notional': notional, 'margin_trading': margin_trading}
        return self._request_with_params(POST, SPOT_ORDER, params)

    def take_orders(self, params):
        return self._request_with_params(POST, SPOT_ORDERS, params)

    # revoke order
    def revoke_order(self, instrument_id, order_id='', client_oid=''):
        params = {'instrument_id': instrument_id}
        if order_id:
            return self._request_with_params(POST, SPOT_REVOKE_ORDER + str(order_id), params)
        elif client_oid:
            return self._request_with_params(POST, SPOT_REVOKE_ORDER + str(client_oid), params)

    def revoke_orders(self, params):
        return self._request_with_params(POST, SPOT_REVOKE_ORDERS, params)

    def modify_order(self, instrument_id, cancel_on_fail='', order_id='', client_oid='', request_id='', new_size='',
                     new_price=''):
        params = {}
        if cancel_on_fail:
            params['cancel_on_fail'] = cancel_on_fail
        elif order_id:
            params['order_id'] = order_id
        elif client_oid:
            params['client_oid'] = client_oid
        elif request_id:
            params['request_id'] = request_id
        elif new_size:
            params['new_size'] = new_size
        elif new_price:
            params['new_price'] = new_price
        return self._request_with_params(POST, SPOT_MODIFY + str(instrument_id), params)

    def batch_modify_order(self, amend_data):
        params = amend_data
        return self._request_with_params(POST, SPOT_MODIFY_BATCH, params)

    # query orders list v3
    def get_orders_list(self, instrument_id, state, after='', before='', limit=''):
        params = {'instrument_id': instrument_id, 'state': state}
        if after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_ORDERS_LIST, params, cursor=True)

    # query order info
    def get_order_info(self, instrument_id, order_id='', client_oid=''):
        params = {'instrument_id': instrument_id}
        if order_id:
            return self._request_with_params(GET, SPOT_ORDER_INFO + str(order_id), params)
        elif client_oid:
            return self._request_with_params(GET, SPOT_ORDER_INFO + str(client_oid), params)

    def get_orders_pending(self, instrument_id, after='', before='', limit=''):
        params = {'instrument_id': instrument_id}
        if after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_ORDERS_PENDING, params, cursor=True)

    def get_fills(self, instrument_id, order_id='', from_c='', to='', limit=''):
        params = {'instrument_id': instrument_id, 'order_id': order_id}
        if from_c:
            params['from'] = from_c
        if to:
            params['to'] = to
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_FILLS, params, cursor=True)

    def place_algo_order(self, instrument_id, mode, order_type, size, side,
                         trigger_price='', algo_price='', algo_type='',
                         callback_rate='', algo_variance='', avg_amount='', limit_price='',
                         sweep_range='', sweep_ratio='', single_limit='', time_interval='',
                         tp_trigger_price='', tp_price='', tp_trigger_type='', sl_trigger_type='', sl_trigger_price='',
                         sl_price=''):
        params = {'instrument_id': instrument_id, 'mode': mode, 'order_type': order_type, 'size': size, 'side': side}
        if order_type == '1':
            params['trigger_price'] = trigger_price
            params['algo_price'] = algo_price
            params['algo_type'] = algo_type
        elif order_type == '2':
            params['callback_rate'] = callback_rate
            params['trigger_price'] = trigger_price
        elif order_type == '3':
            params['algo_variance'] = algo_variance
            params['avg_amount'] = avg_amount
            params['limit_price'] = limit_price
        elif order_type == '4':
            params['sweep_range'] = sweep_range
            params['sweep_ratio'] = sweep_ratio
            params['single_limit'] = single_limit
            params['limit_price'] = limit_price
            params['time_interval'] = time_interval
        elif order_type == '5':
            params['tp_trigger_price'] = tp_trigger_price
            params['tp_price'] = tp_price
            params['tp_trigger_type'] = tp_trigger_type
            params['sl_trigger_type'] = sl_trigger_type
            params['sl_trigger_price'] = sl_trigger_price
            params['sl_price'] = sl_price
        return self._request_with_params(POST, SPOT_ALGO_PLACE, params)

    def cancel_algo_order(self, instrument_id, algo_ids, order_type):
        params = {'instrument_id': instrument_id, 'algo_ids': algo_ids, 'order_type': order_type}
        return self._request_with_params(POST, SPOT_ALGO_CANCEL, params)

    def get_trade_fee(self, category='', instrument_id=''):
        params = {}
        if category:
            params['category'] = category
        elif instrument_id:
            params['instrument_id'] = instrument_id
        return self._request_with_params(GET, SPOT_TRADE_FEE, params)

    def get_algo_list(self, instrument_id, order_type, status='', algo_ids='', before='', after='', limit=''):
        params = {'instrument_id': instrument_id, 'order_type': order_type}
        if status:
            params['status'] = status
        elif algo_ids:
            params['algo_ids'] = algo_ids
        elif before:
            params['before'] = before
        elif after:
            params['after'] = after
        elif limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_ALGO_LIST, params)

    # query spot coin info
    def get_coin_info(self):
        return self._request_without_params(GET, SPOT_COIN_INFO)

    # query depth
    def get_depth(self, instrument_id, size, depth):
        params = {'instrument_id': instrument_id, 'size': size, 'depth': depth}
        return self._request_with_params(GET, SPOT_DEPTH + str(instrument_id) + '/book', params)

    # query ticker info
    def get_ticker(self):
        return self._request_without_params(GET, SPOT_TICKER)

    # query specific ticker
    def get_specific_ticker(self, instrument_id):
        return self._request_without_params(GET, SPOT_SPECIFIC_TICKER + str(instrument_id) + '/ticker')

    def get_deal(self, instrument_id, limit=''):
        params = {}
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_DEAL + str(instrument_id) + '/trades', params)

    # query k-line info
    def get_kline(self, instrument_id, granularity='', start='', end=''):
        params = {}
        if start:
            params['start'] = start
        if end:
            params['end'] = end
        if granularity:
            params['granularity'] = granularity

        # 按时间倒叙 即由结束时间到开始时间
        return self._request_with_params(GET, SPOT_KLINE + str(instrument_id) + '/candles', params)

        # 按时间正序 即由开始时间到结束时间
        # data = self._request_with_params(GET, SPOT_KLINE + str(instrument_id) + '/candles', params)
        # return list(reversed(data))
