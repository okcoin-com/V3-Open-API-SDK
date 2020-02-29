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
    def get_ledger_record(self, currency, after='', before='', limit=''):
        params = {}
        if after:
            params['after'] = after
        if before:
            params['before'] = before
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_LEDGER_RECORD + str(currency) + '/ledger', params, cursor=True)

    # take order
    def take_order(self, instrument_id, side, client_oid='', type='', size='', price='', order_type='0', notional='', margin_trading=''):
        params = {'instrument_id': instrument_id, 'side': side, 'client_oid': client_oid, 'type': type, 'size': size, 'price': price, 'order_type': order_type, 'notional': notional, 'margin_trading': margin_trading}
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

    # query orders list v3
    def get_orders_list(self, instrument_id, state, after='', before='', limit=''):
        params = {'instrument_id': instrument_id, 'state': state}
        if after:
            params['after'] = after
        if before:
            params['before'] = before
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_ORDERS_LIST, params, cursor=True)

    # query order info
    def get_order_info(self, instrument_id, order_id='', client_oid=''):
        params = {'instrument_id': instrument_id}
        if order_id:
            return self._request_with_params(GET, SPOT_ORDER_INFO + str(order_id), params)
        elif client_oid:
            return self._request_with_params(GET, SPOT_ORDER_INFO + str(client_oid), params)

    def get_orders_pending(self, instrument_id='', after='', before='', limit=''):
        params = {'instrument_id': instrument_id}
        if after:
            params['after'] = after
        if before:
            params['before'] = before
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_ORDERS_PENDING, params, cursor=True)

    def get_fills(self, instrument_id, order_id, from_c='', to='', limit=''):
        params = {'instrument_id': instrument_id, 'order_id': order_id}
        if from_c:
            params['from'] = from_c
        if to:
            params['to'] = to
        if limit:
            params['limit'] = limit
        return self._request_with_params(GET, SPOT_FILLS, params, cursor=True)

    # query spot coin info
    def get_coin_info(self):
        return self._request_without_params(GET, SPOT_COIN_INFO)

    # query depth
    def get_depth(self, instrument_id, size='', depth=''):
        params = {}
        if size:
            params['size'] = size
        if depth:
            params['depth'] = depth
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
