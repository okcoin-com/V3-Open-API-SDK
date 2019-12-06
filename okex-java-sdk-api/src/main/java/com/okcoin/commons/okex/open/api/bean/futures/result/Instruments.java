package com.okcoin.commons.okex.open.api.bean.futures.result;

/**
 * futures contract products <br/>
 *
 * @author Tony Tian
 * @version 1.0.0
 * @date 2018/2/26 10:49
 */
public class Instruments {
    /**
     * The id of the futures contract
     */
    private String instrument_id;
    /**
     * Currency
     */
    private String underlying_index;
    /**
     * Quote currency
     */
    private String quote_currency;
    /**
     * Minimum amount: $
     */
    private String tick_size;
    /**
     * Unit price per contract
     */
    private String contract_val;
    /**
     * Effect of time
     */
    private String listing;
    /**
     * Settlement price
     */
    private String delivery;
    /**
     * Minimum amount: cont
     */
    private String trade_increment;

    public String getInstrument_id() { return instrument_id; }

    public void setInstrument_id(String instrument_id) { this.instrument_id = instrument_id; }


    public String getQuote_currency() {
        return quote_currency;
    }

    public void setQuote_currency(String quote_currency) {
        this.quote_currency = quote_currency;
    }

    public String getTick_size() { return tick_size; }

    public void setTick_size(String tick_size) { this.tick_size = tick_size; }

    public String getContract_val() {
        return contract_val;
    }

    public void setContract_val(String contract_val) {
        this.contract_val = contract_val;
    }

    public String getListing() {
        return listing;
    }

    public void setListing(String listing) {
        this.listing = listing;
    }

    public String getDelivery() {
        return delivery;
    }

    public void setDelivery(String delivery) {
        this.delivery = delivery;
    }

    public String getTrade_increment() {
        return trade_increment;
    }

    public void setTrade_increment(String trade_increment) {
        this.trade_increment = trade_increment;
    }

    public String getUnderlying_index() { return underlying_index; }

    public void setUnderlying_index(String underlying_index) { this.underlying_index = underlying_index; }
}
