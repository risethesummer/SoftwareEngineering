package com.example.bookbook;
import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.UUID;

public class Order {
    String id;
    HashMap<Product, Integer> products;
    DateTimeFormatter purchased_time;
    long total_price;
}
