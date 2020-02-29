using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKCoinSDK;
using OKCoinSDK.Models;
using OKCoinSDK.Models.Account;
using OKCoinSDK.Models.Ett;
using OKCoinSDK.Models.Futures;
using OKCoinSDK.Models.Margin;
using OKCoinSDK.Models.Spot;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using swap = OKCoinSDK.Models.Swap;

namespace SampleCS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeneralApi generalApi;
        private AccountApi accountApi;
        private SpotApi spotApi;
        private MarginApi marginApi;

        private string apiKey = "";
        private string secret = "";
        private string passPhrase = "";
        public MainWindow()
        {
            InitializeComponent();
            this.generalApi = new GeneralApi(this.apiKey, this.secret, this.passPhrase);
            this.accountApi = new AccountApi(this.apiKey, this.secret, this.passPhrase);
            this.spotApi = new SpotApi(this.apiKey, this.secret, this.passPhrase);
            this.marginApi = new MarginApi(this.apiKey, this.secret, this.passPhrase);
            this.DataContext = new MainViewModel();
        }
        private void btnSetKey(object sender, RoutedEventArgs e)
        {
            var keyinfo = ((MainViewModel)this.DataContext).KeyInfo;
            apiKey = keyinfo.api_key;
            secret = keyinfo.secret;
            passPhrase = keyinfo.passphrase;
            this.generalApi = new GeneralApi(apiKey, secret, passPhrase);
            this.accountApi = new AccountApi(apiKey, secret, passPhrase);
            this.spotApi = new SpotApi(apiKey, secret, passPhrase);
            this.marginApi = new MarginApi(apiKey, secret, passPhrase);
            Console.WriteLine("完成");
        }

        private async void btnSyncServerTimeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await this.generalApi.syncTimeAsync();
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async void btnGetCurrencies(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.accountApi.getCurrenciesAsync();
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var currencies = resResult.ToObject<List<Currency>>();
                    Console.WriteLine(JsonConvert.SerializeObject(currencies));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWallet(object sender, RoutedEventArgs e)
        {
            //资金账户信息
            try
            {
                var resResult = await this.accountApi.getWalletInfoAsync();
                Console.WriteLine(resResult);
                //if (resResult.Type == JTokenType.Object)
                //{

                //    JToken codeJToken;
                //    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                //    {
                //        var errorInfo = resResult.ToObject<ErrorResult>();
                //        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                //    }
                //}
                //else
                //{
                //    var walletInfo = resResult.ToObject<List<Wallet>>();
                //    Console.WriteLine(JsonConvert.SerializeObject(walletInfo));
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWalletByCurrency(object sender, RoutedEventArgs e)
        {
            //单一账户币种信息
            try
            {
                var resResult = await this.accountApi.getWalletInfoByCurrencyAsync("OKB");
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var walletInfo = resResult.ToObject<List<Wallet>>();
                    Console.WriteLine(JsonConvert.SerializeObject(walletInfo));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeTransfer(object sender, RoutedEventArgs e)
        {
            //资金划转
            try
            {
                var resResult = await this.accountApi.makeTransferAsync(((MainViewModel)this.DataContext).Transfer);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var transferResult = resResult.ToObject<TransferResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(transferResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeWithDrawal(object sender, RoutedEventArgs e)
        {
            //提币
            try
            {
                var resResult = await this.accountApi.makeWithDrawalAsync(((MainViewModel)this.DataContext).WithDrawal);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var withdrawalResult = resResult.ToObject<WithDrawalResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(withdrawalResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWithdrawalFee(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.accountApi.getWithDrawalFeeAsync("eos");
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var fee = resResult.ToObject<List<WithdrawalFee>>();
                    Console.WriteLine(JsonConvert.SerializeObject(fee));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWithdrawalHistory(object sender, RoutedEventArgs e)
        {
            //提币记录
            try
            {
                var resResult = await this.accountApi.getWithDrawalHistoryAsync();
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var history = resResult.ToObject<List<WithDrawalHistory>>();
                    Console.WriteLine(JsonConvert.SerializeObject(history));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWithdrawalHistoryByCurrency(object sender, RoutedEventArgs e)
        {
            //单个币种提币记录
            try
            {
                var resResult = await this.accountApi.getWithDrawalHistoryByCurrencyAsync("eos");
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var history = resResult.ToObject<List<WithDrawalHistory>>();
                    Console.WriteLine(JsonConvert.SerializeObject(history));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetWalletLedger(object sender, RoutedEventArgs e)
        {
            //账单流水查询
            try
            {
                var resResult = await this.accountApi.getLedgerAsync("eos", "2", 1, null, 10);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var walletLedger = resResult.ToObject<List<AccountLedger>>();
                    Console.WriteLine(JsonConvert.SerializeObject(walletLedger));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetDepositAddress(object sender, RoutedEventArgs e)
        {
            //获取充值地址
            try
            {
                var resResult = await this.accountApi.getDepositAddressAsync("eos");

                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetDepositHistory(object sender, RoutedEventArgs e)
        {
            //获取所有币种充值记录
            try
            {
                var resResult = await this.accountApi.getDepositHistoryAsync();
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var history = resResult.ToObject<List<DepositHistory>>();
                    Console.WriteLine(JsonConvert.SerializeObject(history));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetDepositHistoryByCurrency(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.accountApi.getDepositHistoryByCurrencyAsync("eth");
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var history = resResult.ToObject<List<DepositHistory>>();
                    Console.WriteLine(JsonConvert.SerializeObject(history));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getSpotAccountsAsync();
                Console.WriteLine(resResult);
                //if (resResult.Type == JTokenType.Object)
                //{
                //    JToken codeJToken;
                //    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                //    {
                //        var errorInfo = resResult.ToObject<ErrorResult>();
                //        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                //    }
                //}
                //else
                //{
                //    var spotaccount = resResult.ToObject<List<SpotAccount>>();
                //    Console.WriteLine(JsonConvert.SerializeObject(spotaccount));
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotAccountByCurrency(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getAccountByCurrencyAsync("eos");


                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotLedger(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getSpotLedgerByCurrencyAsync("trx", null, null, 10, null);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var walletLedger = resResult.ToObject<List<SpotLedger>>();
                    Console.WriteLine(JsonConvert.SerializeObject(walletLedger));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeMarketOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.makeOrderAsync<SpotOrderMarket>(((MainViewModel)this.DataContext).SpotOrderMarket);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var orderResult = resResult.ToObject<SpotOrderResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(orderResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeLimitOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.makeOrderAsync<SpotOrderLimit>(((MainViewModel)this.DataContext).SpotOrderLimit);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var orderResult = resResult.ToObject<SpotOrderResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(orderResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void btnMakeMarketOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = ((MainViewModel)this.DataContext).SpotOrderMarket;
                var resResult = await this.spotApi.makeOrderBatchAsync<SpotOrderMarket>(new List<SpotOrderMarket>() { order });

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var obj = resResult.Value<JObject>();
                    foreach (var property in obj)
                    {
                        Console.WriteLine(property.Key + ":" + JsonConvert.SerializeObject(property.Value.ToObject<List<SpotOrderResult>>()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void btnMakeLimitOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = ((MainViewModel)this.DataContext).SpotOrderLimit;
                var resResult = await this.spotApi.makeOrderBatchAsync<SpotOrderLimit>(new List<SpotOrderLimit>() { order });

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var obj = resResult.Value<JObject>();
                    foreach (var property in obj)
                    {
                        Console.WriteLine(property.Key + ":" + JsonConvert.SerializeObject(property.Value.ToObject<List<SpotOrderResult>>()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnCancelSpotOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.cancelOrderByOrderIdAsync(this.spot_order_id.Text, this.spot_instrument_id.Text, null);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var orderResult = resResult.ToObject<SpotOrderResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(orderResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnCancelSpotOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = new List<CancelOrderBatch>();
                var order = new CancelOrderBatch()
                {
                    instrument_id = this.spot_instrument_id.Text,
                    order_ids = new List<long>() { long.Parse(this.spot_order_id.Text) },
                };
                orders.Add(order);
                var resResult = await this.spotApi.cancelOrderBatchAsync(orders);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var obj = resResult.Value<JObject>();
                    foreach (var property in obj)
                    {
                        Console.WriteLine(property.Key + ":" + JsonConvert.SerializeObject(property.Value.ToObject<List<SpotOrderResult>>()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotOrders(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getOrdersAsync("ETC-USD", "2", null, null, null);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var orders = resResult.ToObject<List<OrderFullInfo>>();
                    Console.WriteLine(JsonConvert.SerializeObject(orders));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetPendingOrders(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getPendingOrdersAsync("CAI-BTC", null, null, 10);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var orders = resResult.ToObject<List<OrderFullInfo>>();
                    Console.WriteLine(JsonConvert.SerializeObject(orders));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotOrderById(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getOrderByIdAsync(this.spotinstrument_id.Text, this.spotorder_id.Text);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var fills = resResult.ToObject<OrderFullInfo>();
                    Console.WriteLine(JsonConvert.SerializeObject(fills));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void btnGetSpotFills(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getFillsAsync(long.Parse(this.spotorder_id.Text), this.spotinstrument_id.Text, 1, null, 10);
                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotInstruments(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getInstrumentsAsync();

                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var instruments = resResult.ToObject<List<SpotInstrument>>();
                    Console.WriteLine(JsonConvert.SerializeObject(instruments.Take(10)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotBook(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getBookAsync("BTC-USD", null, null);
                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var book = resResult.ToObject<SpotBook>();
                    Console.WriteLine(JsonConvert.SerializeObject(book));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotTicker(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getTickerAsync();

                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var tickers = resResult.ToObject<List<SpotTicker>>();
                    Console.WriteLine(JsonConvert.SerializeObject(tickers.Take(10)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotTickerByInstrument(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getTickerByInstrumentIdAsync("BTC-USDT");


                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var ticker = resResult.ToObject<SpotTicker>();
                    Console.WriteLine(JsonConvert.SerializeObject(ticker));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotTrades(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getTradesAasync("CAI-BTC", null, null, 10);

                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var trades = resResult.ToObject<List<SpotTrade>>();
                    Console.WriteLine(JsonConvert.SerializeObject(trades));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetSpotCandles(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getCandlesAsync("BTC-USDT", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow, 60);


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginAccount(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getAccountsAsync();


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginAccountByInstrument(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getAccountsByInstrumentIdAsync("BTC_USDT");


                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginLedger(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getLedgerAsync("BTC_USD", null, null, null, 3);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var marginLedger = resResult.ToObject<List<MarginLedger>>();
                    Console.WriteLine(JsonConvert.SerializeObject(marginLedger));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetAvailability(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getAvailabilityAsync();

                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetAvailable(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getAvailabilityByInstrumentId("BTC_USDT");

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetBorrowed(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getBorrowedAsync("0", null, null, 10);

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetBorrowByInstrument(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getBorrowedByInstrumentIdAsync("BTC_USDT", "0", null, null, 10);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var borrows = resResult.ToObject<List<Borrowed>>();
                    Console.WriteLine(JsonConvert.SerializeObject(borrows));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeBorrow(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.makeBorrowAsync(this.borrow_instrument_id.Text, this.borrow_currency.Text, this.borrow_amount.Text);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var borrowResult = resResult.ToObject<BorrowResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(borrowResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeRepayment(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.makeRepaymentAsync(long.Parse(this.repay_borrow_id.Text), this.repay_instrument_id.Text, this.repay_currency.Text, this.repay_amount.Text);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var borrowResult = resResult.ToObject<RepaymentResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(borrowResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeMarginMarketOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = ((MainViewModel)this.DataContext).MarginOrderMarket;
                order.order_type = "0";
                var resResult = await this.marginApi.makeOrderAsync<MarginOrderMarket>(order);


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeMarginMarketOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = ((MainViewModel)this.DataContext).MarginOrderMarket;
                order.order_type = "0";
                var resResult = await this.marginApi.makeOrderBatchAsync<MarginOrderMarket>(new List<MarginOrderMarket>() { order });


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeMarginLimitOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.makeOrderAsync<MarginOrderLimit>(((MainViewModel)this.DataContext).MarginOrderLimit);

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnMakeMarginLimitOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = ((MainViewModel)this.DataContext).MarginOrderLimit;
                order.order_type = "0";
                var resResult = await this.marginApi.makeOrderBatchAsync<MarginOrderLimit>(new List<MarginOrderLimit>() { order });

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnCancelMarginOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.cancelOrderByOrderIdAsync(this.margin_order_id.Text, this.margin_instrument_id.Text, null);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var orderResult = resResult.ToObject<MarginOrderResult>();
                    Console.WriteLine(JsonConvert.SerializeObject(orderResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnCancelMarginOrderBatch(object sender, RoutedEventArgs e)
        {
            try
            {
                var orders = new List<MarginCancelOrderBatch>();
                var order = new MarginCancelOrderBatch()
                {
                    instrument_id = this.margin_instrument_id.Text,
                    order_ids = new List<long>() { long.Parse(this.margin_order_id.Text) },
                };
                orders.Add(order);
                var resResult = await this.marginApi.cancelOrderBatchAsync(orders);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var obj = resResult.Value<JObject>();
                    foreach (var property in obj)
                    {
                        Console.WriteLine(property.Key + ":" + JsonConvert.SerializeObject(property.Value.ToObject<List<MarginOrderResult>>()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginOrderById(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getOrderByIdAsync(this.margininstrument_id.Text, this.marginorder_id.Text);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var fills = resResult.ToObject<MarginOrderFullInfo>();
                    Console.WriteLine(JsonConvert.SerializeObject(fills));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginFills(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getFillsAsync(long.Parse(this.marginorder_id.Text), this.margininstrument_id.Text, 1, null, 10);

                JToken codeJToken;
                if (((JObject)resResult).TryGetValue("code", out codeJToken))
                {
                    var errorInfo = resResult.ToObject<ErrorResult>();
                    Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                }
                else
                {
                    var orderResult = resResult.ToObject<List<MarginFill>>();
                    Console.WriteLine(JsonConvert.SerializeObject(orderResult));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void btnMargin_GetLeverage(object sender, RoutedEventArgs e)
        {
            try
            {
                string instrument_id = "";
                var resResult = await this.marginApi.getLeverage(instrument_id);
                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private async void btnGetMarginOrders(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getOrdersAsync("BTC-USDT", "2", null, null, 10);
                if (resResult.Type == JTokenType.Object)
                {
                    JToken codeJToken;
                    if (((JObject)resResult).TryGetValue("code", out codeJToken))
                    {
                        var errorInfo = resResult.ToObject<ErrorResult>();
                        Console.WriteLine("错误代码：" + errorInfo.code + ",错误消息：" + errorInfo.message);
                    }
                }
                else
                {
                    var orders = resResult.ToObject<List<MarginOrderFullInfo>>();
                    Console.WriteLine(JsonConvert.SerializeObject(orders));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btnGetMarginPendingOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.getPendingOrdersAsync("CAI-BTC", null, null, 10);

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void btnGetOrders_margin(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.marginApi.btnGetOrders("btc-usdt", "23458");

                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        private async void btnGetOrder_algoSpot(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.getOrder_algoAsync("BTC-USDT", 1, 2, null, null, null, null);

                Console.WriteLine(resResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // 创建Websocketor对象
        private WebSocketor websocketor = new WebSocketor();
        /// <summary>
        /// WebSocket消息推送侦听
        /// </summary>
        /// <param name="msg">WebSocket消息</param>
        private void handleWebsocketMessage(string msg)
        {
            try
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //this.msgBox.AppendText(msg + Environment.NewLine);//换行标识
                    Console.WriteLine(msg);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace.ToString());
            }
        }

        private async void btnConnect(object sender, RoutedEventArgs e)
        {
            websocketor.WebSocketPush -= this.handleWebsocketMessage;
            websocketor.WebSocketPush += this.handleWebsocketMessage;
            await websocketor.ConnectAsync();
            Console.WriteLine("连接成功");
        }

        //private async void btnSubscribe(object sender, RoutedEventArgs e)
        //{
        //    await websocketor.Subscribe(new List<String>() { "futures/depth:BTC-USDT-191227" });
        //}
        private async void btnSubscribe(object sender, RoutedEventArgs e)
        {
            string channel = "", contract = "", currencyType = "", candle = "", contractID = "";
            Button btn = (Button)sender;
            string btn_type = btn.Content.ToString();
            switch (btn_type)
            {
                case "订阅(spot)":
                    channel = cleanTag(this.channel_spot.Text);
                    contract = cleanTag(this.contract_spot.Text);
                    currencyType = cleanTag(this.currency_spot.Text);
                    candle = cleanTag(this.candle_spot.Text);
                    contractID = this.contract_spot.Text;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(candle) && candle != "请选择时间粒度")
            {
                if (!string.IsNullOrWhiteSpace(contract) && contract != "请选择合约类型")
                {
                    await websocketor.Subscribe(new List<string>() { $"{channel}{candle}s:{contract}" });
                }
                else if (!string.IsNullOrWhiteSpace(currencyType) && currencyType != "请选择币种类型")
                {
                    await websocketor.Subscribe(new List<string>() { $"{channel}{candle}s:{currencyType}" });
                }
                else if (!string.IsNullOrWhiteSpace(contractID))
                {
                    await websocketor.Subscribe(new List<string>() { $"{channel}{candle}s:{contractID}" });
                }
            }
            else if (!string.IsNullOrWhiteSpace(contract) && contract != "请选择合约类型")
            {
                await websocketor.Subscribe(new List<string>() { $"{channel}:{contract}" });
            }
            else if (!string.IsNullOrWhiteSpace(currencyType) && currencyType != "请选择币种类型")
            {
                await websocketor.Subscribe(new List<string>() { $"{channel}:{currencyType}" });
            }
            else if (!string.IsNullOrWhiteSpace(contractID))
            {
                await websocketor.Subscribe(new List<string>() { $"{channel}:{contractID}" });
            }
            else
            {
                await websocketor.Subscribe(new List<string>() { $"{channel}" });
            }
            
        }
        private string cleanTag(string content)
        {
            int sign = content.IndexOf("(");
            return sign > 0 ? content.Substring(0, sign) : content;
        }

        //private async void btnUnSubscribe(object sender, RoutedEventArgs e)
        //{
        //    await websocketor.UnSubscribe(new List<String>() { "swap/depth:BTC-USD-SWAP", "swap/candle60s:BTC-USD-SWAP" });
        //}
        private async void btnUnSubscribe(object sender, RoutedEventArgs e)
        {
            string channel = "", contract = "", currencyType = "", candle = "", contractID = "";
            Button btn = (Button)sender;
            string btn_type = btn.Content.ToString();
            switch (btn_type)
            {
                case "取消订阅(spot)":
                    channel = cleanTag(this.channel_spot.Text);
                    contract = cleanTag(this.contract_spot.Text);
                    currencyType = cleanTag(this.currency_spot.Text);
                    candle = cleanTag(this.candle_spot.Text);
                    contractID = this.contract_spot.Text;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(candle) && candle != "请选择时间粒度")
            {
                if (!string.IsNullOrWhiteSpace(contract) && contract != "请选择合约类型")
                {
                    await websocketor.UnSubscribe(new List<string>() { $"{channel}{candle}s:{contract}" });
                }
                else if (!string.IsNullOrWhiteSpace(currencyType) && currencyType != "请选择币种类型")
                {
                    await websocketor.UnSubscribe(new List<string>() { $"{channel}{candle}s:{currencyType}" });
                }
                else if (!string.IsNullOrWhiteSpace(contractID))
                {
                    await websocketor.UnSubscribe(new List<string>() { $"{channel}{candle}s:{contractID}" });
                }
            }
            else if (!string.IsNullOrWhiteSpace(contract) && contract != "请选择合约类型")
            {
                await websocketor.UnSubscribe(new List<string>() { $"{channel}:{contract}" });
            }
            else if (!string.IsNullOrWhiteSpace(currencyType) && currencyType != "请选择币种类型")
            {
                await websocketor.UnSubscribe(new List<string>() { $"{channel}:{currencyType}" });
            }
            else if (!string.IsNullOrWhiteSpace(contractID))
            {
                await websocketor.UnSubscribe(new List<string>() { $"{channel}:{contractID}" });
            }
            else
            {
                await websocketor.UnSubscribe(new List<string>() { $"{channel}" });
            }
        }

        private async void btnLogin(object sender, RoutedEventArgs e)
        {
            await websocketor.LoginAsync(this.apiKey, this.secret, this.passPhrase);
        }

        private async void btnSubscribeLogin(object sender, RoutedEventArgs e)
        {
            await websocketor.Subscribe(new List<String>() { "futures/account:BTC" });
        }

        private void btnDispose(object sender, RoutedEventArgs e)
        {
            websocketor.Dispose();
        }

        private async void btnProgress(object sender, RoutedEventArgs e)
        {
            // 取消事件侦听
            websocketor.WebSocketPush -= this.handleWebsocketMessage;
            // 添加事件侦听
            websocketor.WebSocketPush += this.handleWebsocketMessage;
            try
            {
                // 建立WebSocket连接
                await websocketor.ConnectAsync();
                // 订阅无需登录的Channel
                await websocketor.Subscribe(new List<String>() { "swap/ticker:BTC-USD-SWAP", "swap/candle60s:BTC-USD-SWAP" });
                // 登录
                await websocketor.LoginAsync(this.apiKey, this.secret, this.passPhrase);
                // 等待登录
                await Task.Delay(500);
                // 订阅需要登录的Channel
                await websocketor.Subscribe(new List<String>() { "futures/account:BTC" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    
        private async void btnOrder_algoSpot(object sender, RoutedEventArgs e)
        {
            try
            {
                var resResult = await this.spotApi.btnOrder_algoSpot("BTC-USDT", "1", "1", "0.05", "sell", "8000", "7500");


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async void btncancel_batch_algosSpot(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> algo_ids = new List<string>();
                algo_ids.Add("1600593327162368");
                string algoids = JsonConvert.SerializeObject(algo_ids);
                var resResult = await this.spotApi.cancel_batch_algosSpot("BTC-USDT", algoids, "1");


                Console.WriteLine(resResult);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
      
        private async void btnSpot_GetTrade_fee(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = await this.spotApi.getTrade_fee();
                Console.WriteLine(content);
            }
            catch(Exception ex)
            {
                Console.WriteLine("错误信息" + ex.Message + "堆栈信息" + ex.StackTrace.ToString());
            }
     
        }
       
        private async void btnAccount_GetSub_AccountAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                string sub_account = "Test";
                string content = await this.accountApi.getsub_accountAsync(sub_account);
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误信息" + ex.Message + "堆栈信息" + ex.StackTrace.ToString());
            }
        }
        private async void btngetAsset_ValuationAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = await this.accountApi.getAsset_ValuationAsync();
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误信息" + ex.Message + "堆栈信息" + ex.StackTrace.ToString());
            }
        }
        private async void btnMargin_SetLeverage(object sender,RoutedEventArgs e)
        {
            try
            {
                string instrument_id = "";
                var data = new { leverage="10" };
                string bodystr = JsonConvert.SerializeObject(data);
                string content = await this.marginApi.SetLeverage(instrument_id,bodystr);
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误信息" + ex.Message + "堆栈信息" + ex.StackTrace.ToString());
            }
        }
        private async void btnMargin_Mark_price(object sender, RoutedEventArgs e)
        {
            try
            {
                string instrument_id = "BTC-USD";
                string content = await this.marginApi.getMark_price(instrument_id);
                Console.WriteLine(content);
            }
            catch(Exception ex)
            {
                Console.WriteLine("错误信息" + ex.Message + "堆栈信息" + ex.StackTrace.ToString());
            }
        }
    }
}
