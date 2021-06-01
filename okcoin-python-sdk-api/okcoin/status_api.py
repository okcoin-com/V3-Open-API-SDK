from .client import Client
from .consts import *


class StatusAPI(Client):
    def __init__(self, api_key, api_secret_key, passphrase, use_server_time=False):
        Client.__init__(self, api_key, api_secret_key, passphrase, use_server_time)

    def get_status(self, status=''):
        params = {}
        if status:
            params['status'] = status
        return self._request_with_params(GET, STATUS, params)
