package com.example.bookbook;

import java.io.Serializable;
import java.time.*;
import java.time.format.*;
import java.util.ArrayList;
import java.util.List;

public class Ticket extends Product implements Serializable {
     ArrayList<Theater> theaters;
     String movieID;
     String seat;
     String showTime;
     String id;
     String name;
     String type;
     int price;

    public ArrayList<Theater> getTheaters() {
        return theaters;
    }

    public void setTheaters(ArrayList<Theater> theaters) {
        this.theaters = theaters;
    }

    public String getMovieID() {
        return movieID;
    }

    public void setMovieID(String movieID) {
        this.movieID = movieID;
    }

    public String getSeat() {
        return seat;
    }

    public void setSeat(String seat) {
        this.seat = seat;
    }

    public String getShowTime() {
        return showTime;
    }

    public void setShowTime(String showTime) {
        this.showTime = showTime;
    }

    @Override
    public String getId() {
        return id;
    }

    @Override
    public void setId(String id) {
        this.id = id;
    }

    @Override
    public String getName() {
        return name;
    }

    @Override
    public void setName(String name) {
        this.name = name;
    }

    @Override
    public String getType() {
        return type;
    }

    @Override
    public void setType(String type) {
        this.type = type;
    }

    @Override
    public int getPrice() {
        return price;
    }

    @Override
    public void setPrice(int price) {
        this.price = price;
    }
}

//DateTimeFormatter format = DateTimeFormatter.ofPattern("h:mm a");
//        LocalTime time = LocalTime.parse(text, format);
//        System.out.println(time);