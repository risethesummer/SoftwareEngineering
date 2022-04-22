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
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import static android.support.v4.content.ContextCompat.startActivity;


public class Movie implements Serializable {
    String id;
    String name;
    int year;
    String nation;
    String genre;
    int requiredAge;
    int duration;
    String description;
    CompactPerson director;
    String imageID;
    ArrayList<CompactPerson> actors;
    int imdbStar;
    String youtubeLink;

    View create_Frame(LayoutInflater inflater, Context context){
        View result = inflater.inflate(R.layout.movie_frame, null);

        TextView name = (TextView)result.findViewById(R.id.textView_name);
        name.setText(this.name);
        name.setId(ViewCompat.generateViewId());

        TextView nation = (TextView)result.findViewById(R.id.textView_nation);
        nation.setId(ViewCompat.generateViewId());
        nation.setText(this.nation);

        ImageView poster = (ImageView) result.findViewById(R.id.poster);
        Picasso.get().load("https://bookbook3wishes.azurewebsites.net/api/image/" + this.imageID).into(poster);

        return result;
    };

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public String getNation() {
        return nation;
    }

    public void setNation(String nation) {
        this.nation = nation;
    }

    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public int getRequiredAge() {
        return requiredAge;
    }

    public void setRequiredAge(int requiredAge) {
        this.requiredAge = requiredAge;
    }

    public int getDuration() {
        return duration;
    }

    public void setDuration(int duration) {
        this.duration = duration;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public CompactPerson getDirector() {
        return director;
    }

    public void setDirector(CompactPerson director) {
        this.director = director;
    }

    public String getImageID() {
        return imageID;
    }

    public void setImageID(String imageID) {
        this.imageID = imageID;
    }

    public ArrayList<CompactPerson> getActors() {
        return actors;
    }

    public void setActors(ArrayList<CompactPerson> actors) {
        this.actors = actors;
    }

    public int getImdbStar() {
        return imdbStar;
    }

    public void setImdbStar(int imbdStar) {
        this.imdbStar = imbdStar;
    }

    public String getYoutubeLink() {
        return youtubeLink;
    }

    public void setYoutubeLink(String youtubeLink) {
        this.youtubeLink = youtubeLink;
    }
}
