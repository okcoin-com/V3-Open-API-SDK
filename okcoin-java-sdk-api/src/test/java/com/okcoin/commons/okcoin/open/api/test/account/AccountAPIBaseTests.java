package com.okcoin.commons.okcoin.open.api.test.account;

import com.okcoin.commons.okcoin.open.api.config.APIConfiguration;
import com.okcoin.commons.okcoin.open.api.test.BaseTests;
import com.okcoin.commons.okcoin.open.api.enums.I18nEnum;

/**
 * Account api basetests
 *
 * @author hucj
 * @version 1.0.0
 * @date 2018/7/04 18:23
 */
public class AccountAPIBaseTests extends BaseTests {

    public APIConfiguration config() {
        APIConfiguration config = new APIConfiguration();

        config.setEndpoint("https://www.okcoin.com/");
        // apiKey，api注册成功后页面上有
        config.setApiKey("");
        // secretKey，api注册成功后页面上有
        config.setSecretKey("");
        config.setPassphrase("");
        //是否打印配置信息
        config.setPrint(true);

        config.setI18n(I18nEnum.SIMPLIFIED_CHINESE);

        return config;
    }


}
