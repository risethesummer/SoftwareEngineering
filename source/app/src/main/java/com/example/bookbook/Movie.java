package com.example.bookbook;
import android.content.Context;
import android.content.Intent;
import android.support.v4.view.ViewCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;

import com.squareup.picasso.Picasso;

import java.io.Serializable;
import java.util.List;
import java.util.UUID;

import static android.support.v4.content.ContextCompat.startActivity;


public class Movie implements Serializable {
    String ID;
    String Name;
    int Year;
    String Nation;
    String Genre;
    int required_Age;
    int Duration;
    String Description;
    CompactPerson Director;
    String imgUrl;
    List<CompactPerson> Actors;
    List<Theater> Theaters;
    int IMDB_score;
    String TrailerLink;

    View create_Frame(LayoutInflater inflater, Context context){
        View result = inflater.inflate(R.layout.movie_frame, null);

        TextView name = (TextView)result.findViewById(R.id.textView_name);
        name.setText(this.Name);
        name.setId(ViewCompat.generateViewId());

        TextView nation = (TextView)result.findViewById(R.id.textView_nation);
        nation.setText(this.Theaters.toString());
        nation.setId(ViewCompat.generateViewId());

        TextView cinema = (TextView)result.findViewById(R.id.textView_cinema);
        cinema.setText(this.Theaters.toString());

        ImageView poster = (ImageView) result.findViewById(R.id.poster);
        Picasso.with(context).load(this.imgUrl).into(poster);

        return result;
    };

    public String getID() {
        return ID;
    }

    public void setID(String ID) {
        this.ID = ID;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public int getYear() {
        return Year;
    }

    public void setYear(int year) {
        Year = year;
    }

    public String getNation() {
        return Nation;
    }

    public void setNation(String nation) {
        Nation = nation;
    }

    public String getGenre() {
        return Genre;
    }

    public void setGenre(String genre) {
        Genre = genre;
    }

    public int getRequired_Age() {
        return required_Age;
    }

    public void setRequired_Age(int required_Age) {
        this.required_Age = required_Age;
    }

    public int getDuration() {
        return Duration;
    }

    public void setDuration(int duration) {
        Duration = duration;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public CompactPerson getDirector() {
        return Director;
    }

    public void setDirector(CompactPerson director) {
        Director = director;
    }

    public String getImgUrl() {
        return imgUrl;
    }

    public void setImgUrl(String imgUrl) {
        this.imgUrl = imgUrl;
    }

    public List<CompactPerson> getActors() {
        return Actors;
    }

    public void setActors(List<CompactPerson> actors) {
        Actors = actors;
    }

    public List<Theater> getTheaters() {
        return Theaters;
    }

    public void setTheaters(List<Theater> theaters) {
        Theaters = theaters;
    }

    public int getIMDB_score() {
        return IMDB_score;
    }

    public void setIMDB_score(int IMDB_score) {
        this.IMDB_score = IMDB_score;
    }

    public String getTrailerLink() {
        return TrailerLink;
    }

    public void setTrailerLink(String trailerLink) {
        TrailerLink = trailerLink;
    }
}
