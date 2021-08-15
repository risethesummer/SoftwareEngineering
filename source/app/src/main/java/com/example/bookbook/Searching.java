package com.example.bookbook;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.Toast;

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
                Button button = (Button)v;
                try {
                    execute_searching(button, search_box.getText().toString());
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
                Button button = (Button)v;
                try {
                    execute_searching(button, search_box.getText().toString());
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
                Button button = (Button)v;
                try {
                    execute_searching(button, search_box.getText().toString());
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
                Button button = (Button)v;
                try {
                    execute_searching(button, search_box.getText().toString());
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
                Button button = (Button)v;
                try {
                    execute_searching(button, search_box.getText().toString());
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    void execute_searching(Button filter, String data) throws IOException, JSONException {
        Vector<Movie> result = new Vector<>();

    }

    void Show_movies_list(){
        ListIterator<Movie> iterator = list.listIterator();
        while(iterator.hasNext()){
            Movie movie_info = new Movie();
            movie_info = iterator.next();

            View Frame_movie = movie_info.create_Frame(inflater);
            Movie finalMovie_info = movie_info;
            Frame_movie.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent movie_intent = new Intent(Searching.this, InformationMovie.class);
                    movie_intent.putExtra("movie", finalMovie_info);
                    startActivity(movie_intent);
                }
            });
            List_viewLayout.addView(Frame_movie);
        }
    }
}