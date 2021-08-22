package com.example.bookbook;
import android.widget.Button;

import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class Order {
    String id;
    HashMap<Product, Integer> products;
    DateTimeFormatter purchased_time;
    long total_price;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public HashMap<Product, Integer> getProducts() {
        return products;
    }

    public void setProducts(HashMap<Product, Integer> products) {
        this.products = products;
        calculate_price();
    }

    public DateTimeFormatter getPurchased_time() {
        return purchased_time;
    }

    public void setPurchased_time(DateTimeFormatter purchased_time) {
        this.purchased_time = purchased_time;
    }

    public long getTotal_price() {
        return total_price;
    }

    public void setTotal_price(long total_price) {
        this.total_price = total_price;
    }

    private void calculate_price(){
        total_price = 0;
        for (Map.Entry prod : this.products.entrySet() ){
            Product product = (Product) prod.getKey();
            total_price += product.price;
        }
    }
}
