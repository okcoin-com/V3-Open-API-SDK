
# http header
API_URL = 'https://www.okcoin.com'
CONTENT_TYPE = 'Content-Type'
OK_ACCESS_KEY = 'OK-ACCESS-KEY'
OK_ACCESS_SIGN = 'OK-ACCESS-SIGN'
OK_ACCESS_TIMESTAMP = 'OK-ACCESS-TIMESTAMP'
OK_ACCESS_PASSPHRASE = 'OK-ACCESS-PASSPHRASE'


ACEEPT = 'Accept'
COOKIE = 'Cookie'
LOCALE = 'Locale='

APPLICATION_JSON = 'application/json'

GET = "GET"
POST = "POST"
DELETE = "DELETE"

SERVER_TIMESTAMP_URL = '/api/general/v3/time'

# account
WALLET_INFO = '/api/account/v3/wallet'
CURRENCY_INFO = '/api/account/v3/wallet/'
COIN_TRANSFER = '/api/account/v3/transfer'
COIN_WITHDRAW = '/api/account/v3/withdrawal'
LEDGER_RECORD = '/api/account/v3/ledger'
TOP_UP_ADDRESS = '/api/account/v3/deposit/address'
ASSET_VALUATION = '/api/account/v3/asset-valuation'
COINS_WITHDRAW_RECORD = '/api/account/v3/withdrawal/history'
COIN_WITHDRAW_RECORD = '/api/account/v3/withdrawal/history/'
COIN_TOP_UP_RECORDS = '/api/account/v3/deposit/history'
COIN_TOP_UP_RECORD = '/api/account/v3/deposit/history/'
CURRENCIES_INFO = '/api/account/v3/currencies'
COIN_FEE = '/api/account/v3/withdrawal/fee'
SUB_BAN = '/api/account/v3/sub-account'

# fiat
FIAT_DEPOSIT = '/api/account/v3/fiat/deposit'
FIAT_CANCEL_DEPOSIT = '/api/account/v3/fiat/cancel_deposit'
FIAT_DEPOSIT_DETAIL = '/api/account/v3/fiat/deposit/detail'
FIAT_DEPOSIT_DETAILS = '/api/account/v3/fiat/deposit/details'
FIAT_WITHDRAWAL = '/api/account/v3/fiat/withdraw'
FIAT_CANCEL_WITHDRAWAL = '/api/account/v3/fiat/cancel_withdraw'
FIAT_WITHDRAWAL_DETAIL = '/api/account/v3/fiat/withdraw/detail'
FIAT_WITHDRAWAL_DETAILS = '/api/account/v3/fiat/withdraw/details'
FIAT_CHANNEL = '/api/account/v3/fiat/channel'


# spot
SPOT_ACCOUNT_INFO = '/api/spot/v3/accounts'
SPOT_COIN_ACCOUNT_INFO = '/api/spot/v3/accounts/'
SPOT_LEDGER_RECORD = '/api/spot/v3/accounts/'
SPOT_ORDER = '/api/spot/v3/orders'
SPOT_ORDERS = '/api/spot/v3/batch_orders'
SPOT_REVOKE_ORDER = '/api/spot/v3/cancel_orders/'
SPOT_REVOKE_ORDERS = '/api/spot/v3/cancel_batch_orders/'
SPOT_ORDERS_LIST = '/api/spot/v3/orders'
SPOT_ORDERS_PENDING = '/api/spot/v3/orders_pending'
SPOT_ORDER_INFO = '/api/spot/v3/orders/'
SPOT_FILLS = '/api/spot/v3/fills'
SPOT_COIN_INFO = '/api/spot/v3/instruments'
SPOT_DEPTH = '/api/spot/v3/instruments/'
SPOT_TICKER = '/api/spot/v3/instruments/ticker'
SPOT_SPECIFIC_TICKER = '/api/spot/v3/instruments/'
SPOT_DEAL = '/api/spot/v3/instruments/'
SPOT_KLINE = '/api/spot/v3/instruments/'
SPOT_MODIFY = '/api/spot/v3/amend_order/'
SPOT_MODIFY_BATCH = '/api/spot/v3/amend_batch_orders/'
SPOT_ALGO_PLACE = '/api/spot/v3/order_algo'
SPOT_ALGO_CANCEL = '/api/spot/v3/cancel_batch_algos'
SPOT_TRADE_FEE = '/api/spot/v3/trade_fee'
SPOT_ALGO_LIST = '/api/spot/v3/algo'

# lever
LEVER_ACCOUNT = '/api/margin/v3/accounts'
LEVER_COIN_ACCOUNT = '/api/margin/v3/accounts/'
LEVER_LEDGER_RECORD = '/api/margin/v3/accounts/'
LEVER_CONFIG = '/api/margin/v3/accounts/availability'
LEVER_SPECIFIC_CONFIG = '/api/margin/v3/accounts/'
LEVER_BORROW_RECORD = '/api/margin/v3/accounts/borrowed'
LEVER_SPECIFIC_BORROW_RECORD = '/api/margin/v3/accounts/'
LEVER_BORROW_COIN = '/api/margin/v3/accounts/borrow'
LEVER_REPAYMENT_COIN = '/api/margin/v3/accounts/repayment'
LEVER_ORDER = '/api/margin/v3/orders'
LEVER_ORDERS = '/api/margin/v3/batch_orders'
LEVER_REVOKE_ORDER = '/api/margin/v3/cancel_orders/'
LEVER_REVOKE_ORDERS = '/api/margin/v3/cancel_batch_orders'
LEVER_ORDER_LIST = '/api/margin/v3/orders'
LEVEL_ORDERS_PENDING = '/api/margin/v3/orders_pending'
LEVER_ORDER_INFO = '/api/margin/v3/orders/'
LEVER_FILLS = '/api/margin/v3/fills'
LEVER_MARK_PRICE = '/api/margin/v3/instruments/'
LEVER_AMEND_ORDER = '/api/margin/v3/amend_order/'
LEVER_AMEND_BATCH = '/api/margin/v3/amend_batch_orders/'

# status
STATUS = '/api/system/v3/status'

# oracle
ORACLE = '/api/market/v3/oracle'