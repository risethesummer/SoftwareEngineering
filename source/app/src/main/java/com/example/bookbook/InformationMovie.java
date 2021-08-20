package com.example.bookbook;

import android.content.Entity;
import android.content.Intent;
import android.media.Image;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RatingBar;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.MalformedURLException;
import java.net.URL;
import com.squareup.picasso.Picasso;


public class InformationMovie extends AppCompatActivity {
    private Movie info;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_information_movie);

        get_info();
        set_content();

        findViewById(R.id.book_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Book();
            }
        });

    };

    private void get_info() {
        info = (Movie) getIntent().getSerializableExtra("movie");
    }
    private void set_content(){
        EditText movie_name = (EditText)findViewById(R.id.MovieName);
        movie_name.setText(info.Name);

        EditText movie_date = (EditText)findViewById(R.id.Date);
        movie_date.setText(info.Year);

        EditText movie_theater = (EditText)findViewById(R.id.AvailableTheater);
        movie_theater.setText(info.Theaters.toString());

        RatingBar rating = (RatingBar)findViewById(R.id.ratingBar);
        rating.setRating(info.IMDB_score/2);

        ImageView poster = (ImageView) findViewById(R.id.Movie_poster);
        Picasso.with(this).load(info.imgUrl).into(poster);
    };

    private void Book(){
        Intent book_ticket_activity = new Intent(InformationMovie.this, BookTicket.class);
        book_ticket_activity.putExtra("movie", info);
        startActivity(book_ticket_activity);
    };
}