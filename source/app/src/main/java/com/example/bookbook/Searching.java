package com.example.bookbook;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.view.ViewCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.ListIterator;
import java.util.Vector;

public class Searching extends AppCompatActivity {
    private List<Movie> list = new ArrayList<>();;
    private LayoutInflater inflater;
    private LinearLayout List_viewLayout;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_searching);

        inflater = (LayoutInflater)getBaseContext() .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        List_viewLayout = (LinearLayout) findViewById(R.id.SearchList_Layout);

        choose_filter();
    }

    private void choose_filter() {

        EditText search_box = (EditText)findViewById(R.id.search_box);

        findViewById(R.id.button_byName).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    execute_searching("name", search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }

            }
        });
        findViewById(R.id.button_byNation).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    execute_searching("nation", search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }

            }
        });
        findViewById(R.id.button_byActor).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    execute_searching("actor", search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
        findViewById(R.id.button_byGenre).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    execute_searching("genre", search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
        findViewById(R.id.button_byTheatre).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                try {
                    execute_searching("theatre", search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    void execute_searching(String filter, String data) throws IOException, JSONException {
        Vector<Movie> result = new Vector<>();
        String url = "https://bookbook3wishes.azurewebsites.net/api/movie/" + filter + '/' + data;

        JsonArrayRequest req = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                ObjectMapper objectMapper = new ObjectMapper();
                for (int i= 0; i < response.length(); i++){
                    try {
                        list.add(objectMapper.readValue(response.getString(i), Movie.class));
                    } catch (IOException | JSONException e) {
                        e.printStackTrace();
                    }
                }
                Show_movies_list();
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(Searching.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                if (response.statusCode == 403){
                    Toast.makeText(Searching.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });

        //TODO MySingleTon
        MySingleton.getInstance(Searching.this).addToRequestQueue(req);
    }

    void Show_movies_list(){
        for (int i = 0; i < list.size(); i++) {

            Movie movie_info = list.get(i);
            View Frame_movie = movie_info.create_Frame(inflater, Searching.this);
            Frame_movie.setId(ViewCompat.generateViewId());

            int finalI = i;
            Frame_movie.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent movie_intent = new Intent(Searching.this, InformationMovie.class);
                    Movie chosen = list.get(finalI);
                    movie_intent.putExtra("movie", chosen);
                    startActivity(movie_intent);
                }
            });
            List_viewLayout.addView(Frame_movie);
        }
    }
}