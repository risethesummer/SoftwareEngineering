package com.example.bookbook;

import java.time.*;
import java.time.format.*;

public class Ticket extends Product {
    String MovieId;
    DateTimeFormatter showtime;
    String TheaterId;
    String Seat;

    public String getMovieId() {
        return MovieId;
    }

    public void setMovieId(String movieId) {
        MovieId = movieId;
    }

    public DateTimeFormatter getShowtime() {
        return showtime;
    }

    public void setShowtime(DateTimeFormatter showtime) {
        this.showtime = showtime;
    }

    public String getTheaterId() {
        return TheaterId;
    }

    public void setTheaterId(String theaterId) {
        TheaterId = theaterId;
    }

    public String getSeat() {
        return Seat;
    }

    public void setSeat(String seat) {
        Seat = seat;
    }
}

//DateTimeFormatter format = DateTimeFormatter.ofPattern("h:mm a");
//        LocalTime time = LocalTime.parse(text, format);
//        System.out.println(time);