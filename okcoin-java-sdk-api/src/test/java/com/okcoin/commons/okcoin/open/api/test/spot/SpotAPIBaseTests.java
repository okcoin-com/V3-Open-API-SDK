package com.okcoin.commons.okcoin.open.api.test.spot;

import com.okcoin.commons.okcoin.open.api.config.APIConfiguration;
import com.okcoin.commons.okcoin.open.api.test.BaseTests;
import com.okcoin.commons.okcoin.open.api.enums.I18nEnum;

public class SpotAPIBaseTests extends BaseTests {

    public APIConfiguration config() {
        final APIConfiguration config = new APIConfiguration();

        config.setEndpoint("https://www.okcoin.com/");

        // apiKey，api注册成功后页面上有
        config.setApiKey("");
        // secretKey，api注册成功后页面上有
        config.setSecretKey("");
        config.setPassphrase("");

        config.setPrint(true);
        config.setI18n(I18nEnum.SIMPLIFIED_CHINESE);

        return config;
    }

}
