package com.example.bookbook;
import android.widget.Button;

import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class Order {
    String id;
    String product_ID;
    String purchased_time;
    long total_price;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getProducts() {
        return product_ID;
    }

    public void setProducts(String product) {
        this.product_ID = product;
    }

    public String getPurchased_time() {
        return purchased_time;
    }

    public void setPurchased_time(String purchased_time) {
        this.purchased_time = purchased_time;
    }

    public long getTotal_price() {
        return total_price;
    }

    public void setTotal_price(long total_price) {
        this.total_price = total_price;
    }

}
