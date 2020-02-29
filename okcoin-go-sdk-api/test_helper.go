package okex

/*
 Get a http client
*/

func GetDefaultConfig() *Config {
	var config Config

	// flt. 201812. For swap test env.
	config.Endpoint = "https://www.okcoin.com/"
	config.WSEndpoint = "wss://real.okcoin.com:10442/ws/v3"
	config.ApiKey = ""
	config.SecretKey = ""

	// flt. 20190225. For swap test env only
	config.Endpoint = "https://www.okcoin.com/"
	config.ApiKey = ""
	config.SecretKey = ""

	config.TimeoutSecond = 45
	config.IsPrint = true
	config.I18n = ENGLISH

	config.WSEndpoint = "wss://real.okcoin.com:10442/ws/v3"

	////
	////// flt. 20190225.
	////// For future test env only. coinmainweb.new.docker.okex.com --> 192.168.80.97
	//config.Endpoint = "http://coinmainweb.new.docker.okex.com/"
	//config.ApiKey = ""
	//config.SecretKey = ""
	//
	//// flt. 20190305. For spot websocket & restful api test env only
	//config.WSEndpoint = "ws://192.168.80.62:10442/"
	//config.Endpoint = "http://192.168.80.62:8814/"
	//config.ApiKey = ""
	//config.SecretKey = ""

	//
	// flt. 20190306. For account restful api env only
	//config.Endpoint = "http://coinmainweb.new.docker.okex.com/"
	//config.ApiKey = ""
	//config.SecretKey = ""

	// flt. 20190306. For margin restful api env only
	//config.Endpoint = "http://192.168.80.118:8814/"
	//config.ApiKey = ""
	//config.SecretKey = ""

	// flt. 20190822. For latest version changed api. www.okex.com is binded to 149.129.82.222
	config.Endpoint = "https://www.okcoin.com/"

	// set your own ApiKey, SecretKey, Passphrase here
	config.ApiKey = ""
	config.SecretKey = ""
	config.Passphrase = ""

	return &config
}

func NewTestClient() *Client {
	// Set OKEX API's config
	client := NewClient(*GetDefaultConfig())
	return client
}
