from .client import Client
from .consts import *


class AccountAPI(Client):

    def __init__(self, api_key, api_secret_key, passphrase, use_server_time=False):
        Client.__init__(self, api_key, api_secret_key, passphrase, use_server_time)

    # get all currencies list
    def get_currencies(self):
        return self._request_without_params(GET, CURRENCIES_INFO)

    # get wallet info
    def get_wallet(self):
        return self._request_without_params(GET, WALLET_INFO)

    # get specific currency info
    def get_currency(self, currency):
        return self._request_without_params(GET, CURRENCY_INFO + str(currency))

    # coin withdraw
    def coin_withdraw(self, currency, amount, destination, to_address, fee):
        params = {'currency': currency, 'amount': amount, 'destination': destination, 'to_address': to_address,
                  'fee': fee}
        return self._request_with_params(POST, COIN_WITHDRAW, params)

    # query the fee of coin withdraw
    def get_coin_fee(self, currency=''):
        params = {}
        if currency:
            params['currency'] = currency
        return self._request_with_params(GET, COIN_FEE, params)

    # query all recently coin withdraw record
    def get_coins_withdraw_record(self):
        return self._request_without_params(GET, COINS_WITHDRAW_RECORD)

    # query specific coin withdraw record
    def get_coin_withdraw_record(self, currency):
        return self._request_without_params(GET, COIN_WITHDRAW_RECORD + str(currency))

    # query ledger record
    def get_ledger_record(self, currency='', type='', after='', before='', limit=''):
        params = {}
        if currency:
            params['currency'] = currency
        elif type:
            params['type'] = type
        elif after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit
        return self._request_with_params(GET, LEDGER_RECORD, params, cursor=True)

    # query top up address
    def get_top_up_address(self, currency):
        params = {'currency': currency}
        return self._request_with_params(GET, TOP_UP_ADDRESS, params)

    def get_asset_valuation(self, account_type='', valuation_currency=''):
        params = {}
        if account_type:
            params['account_type'] = account_type
        elif valuation_currency:
            params['valuation_currency'] = valuation_currency
        return self._request_with_params(GET, ASSET_VALUATION, params)

    # query top up records
    def get_top_up_records(self):
        return self._request_without_params(GET, COIN_TOP_UP_RECORDS)

    # query top up record
    def get_top_up_record(self, currency):
        return self._request_without_params(GET, COIN_TOP_UP_RECORD + str(currency))

    # coin transfer
    def coin_transfer(self, currency, amount, account_from, account_to, sub_account='', instrument_id='',
                      to_instrument_id=''):
        params = {'currency': currency, 'amount': amount, 'from': account_from, 'to': account_to}
        if sub_account:
            params['sub_account'] = sub_account
        elif instrument_id:
            params['instrument_id'] = instrument_id
        elif to_instrument_id:
            params['to_instrument_id'] = to_instrument_id
        return self._request_with_params('POST', COIN_TRANSFER, params)

    def sub_balance(self, sub_account):
        params = {'sub-account': sub_account}
        return self._request_with_params('GET', SUB_BAN, params)

    def deposit_lightning(self, ccy, amount, to=''):
        params = {'ccy': ccy, 'amount': amount, 'to': to}
        return self._request_with_params('GET', DEPOSIT_LI, params)

    def withdrawal_lightning(self, currency, invoice, memo=''):
        params = {'currency': currency, 'invoice': invoice, 'memo': memo}
        return self._request_with_params(POST, WITHDRAWAL_LI, params)
