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
import android.widget.TextView;
import android.widget.Toast;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.HashMap;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.squareup.picasso.Picasso;

import org.json.JSONArray;
import org.json.JSONException;


public class InformationMovie extends AppCompatActivity {
    private Movie info;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_information_movie);
        info = (Movie) getIntent().getSerializableExtra("movie");

        set_content();

        findViewById(R.id.book_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Book();
            }
        });

    };
    private void set_content(){
        TextView movie_name = (TextView) findViewById(R.id.MovieName);
        movie_name.setText(info.name);

        TextView movie_date = findViewById(R.id.Date);
        movie_date.setText(String.valueOf(info.year));

        RatingBar rating = (RatingBar)findViewById(R.id.ratingBar);
        rating.setRating(info.imdbStar/2);

        ImageView poster = (ImageView) findViewById(R.id.Movie_poster);
        Picasso.get().load("https://bookbook3wishes.azurewebsites.net/api/image/" + info.imageID).into(poster);
    };

    private void Book(){
        String url = "https://bookbook3wishes.azurewebsites.net/api/product/ticket/" + info.id;
        ArrayList<Ticket> tickets = new ArrayList<>();
        JsonArrayRequest req = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                ObjectMapper objectMapper = new ObjectMapper();
                for (int i= 0; i < response.length(); i++){
                    try {
                        tickets.add(objectMapper.readValue(response.getString(i), Ticket.class));
                    } catch (IOException | JSONException e) {
                        e.printStackTrace();
                        return;
                    }
                }
                Intent book_ticket_activity = new Intent(InformationMovie.this, BookTicket.class);
                book_ticket_activity.putExtra("Tickets", tickets);
                startActivity(book_ticket_activity);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(InformationMovie.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                else if (response.statusCode == 403){
                    Toast.makeText(InformationMovie.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });
        MySingleton.getInstance(InformationMovie.this).addToRequestQueue(req);

    };
}