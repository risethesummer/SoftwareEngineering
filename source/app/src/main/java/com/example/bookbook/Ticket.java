package com.example.bookbook;

import java.time.*;
import java.time.format.*;

public class Ticket extends Product {
    int MovieID;
    DateTimeFormatter showtime;
}

//DateTimeFormatter format = DateTimeFormatter.ofPattern("h:mm a");
//        LocalTime time = LocalTime.parse(text, format);
//        System.out.println(time);