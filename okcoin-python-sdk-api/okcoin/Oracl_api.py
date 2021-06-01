from .client import Client
from .consts import *


class OraclAPI(Client):
    def __init__(self, api_key, api_secret_key, passphrase, use_server_time=False):
        Client.__init__(self, api_key, api_secret_key, passphrase, use_server_time)

    def get_oracle(self):
        return self._request_without_params(GET, ORACLE)