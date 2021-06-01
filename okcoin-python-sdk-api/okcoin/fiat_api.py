from .client import Client
from .consts import *


class FiatAPI(Client):
    def __init__(self, api_key, api_secret_key, passphrase, use_server_time=False):
        Client.__init__(self, api_key, api_secret_key, passphrase, use_server_time)

    def fiat_deposit(self, channel_id, amount, bank_account_number, to_account=''):
        params = {'channel_id': channel_id, 'amount': amount, 'bank_account_number': bank_account_number}
        if to_account:
            params['to_account'] = to_account
        return self._request_with_params(POST, FIAT_DEPOSIT, params)

    def cancel_deposit(self, deposit_id):
        params = {'deposit_id': deposit_id}
        return self._request_with_params(POST, FIAT_CANCEL_DEPOSIT, params)

    def deposit_detail(self, deposit_id):
        params = {'deposit_id': deposit_id}
        return self._request_with_params(GET,FIAT_DEPOSIT_DETAIL, params)

    def deposit_details(self, channel_id, currency='', status='', after='', before='', limit=''):
        params = {'channel_id': channel_id}
        if currency:
            params['currency'] = currency
        elif status:
            params['status'] = status
        elif after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit
        return self._request_with_params(GET, FIAT_DEPOSIT_DETAILS, params)

    def fiat_withdraw(self, channel_id, amount, trade_pwd, bank_account_number):
        params = {'channel_id':channel_id, 'amount':amount, 'trade_pwd':trade_pwd, 'bank_account_number':bank_account_number}
        return self._request_with_params(POST, FIAT_WITHDRAWAL, params)

    def cancel_withdrawal(self, withdraw_id):
        params = {'withdraw_id': withdraw_id}
        return self._request_with_params(POST, FIAT_CANCEL_WITHDRAWAL, params)

    def withdraw_detail(self, withdraw_id):
        params = {'withdraw_id': withdraw_id}
        return self._request_with_params(GET, FIAT_WITHDRAWAL_DETAIL, params)

    def withdraw_details(self, channel_id,currency='', status='', after='', before='', limit=''):
        params = {'channel_id':channel_id}
        if currency:
            params['currency'] = currency
        elif status:
            params['status'] = status
        elif after:
            params['after'] = after
        elif before:
            params['before'] = before
        elif limit:
            params['limit'] = limit

        return self._request_with_params(GET, FIAT_WITHDRAWAL_DETAILS, params)

    def get_channel(self, id):
        params = {'id': id}
        return self._request_with_params(GET, FIAT_CHANNEL, params)




