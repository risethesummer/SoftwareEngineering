package com.example.bookbook;
import java.util.List;
import java.util.UUID;

public class Movie {
    UUID ID;
    String Name;
    int Year;
    String Nation;
    String Genre;
    int required_Age;
    int Duration;
    String Description;
    CompactPerson Director;
    String Thumbnail;
    List<CompactPerson> Actors;
    List<Theater> Theaters;
    int IMDB_score;
    String TrailerLink;

}
