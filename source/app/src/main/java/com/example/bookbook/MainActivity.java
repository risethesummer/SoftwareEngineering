package com.example.bookbook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.denzcoskun.imageslider.ImageSlider;
import com.denzcoskun.imageslider.interfaces.ItemClickListener;
import com.denzcoskun.imageslider.models.SlideModel;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.json.JSONArray;
import org.json.JSONException;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity {

    private List<Movie> movieList = new ArrayList<>();
    private ImageSlider imageSlider;
    private List<SlideModel> slideModelList = new ArrayList<>();
    private List<Movie> top_movie = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        imageSlider = findViewById(R.id.Movie_Slider);
        top_movies();


        imageSlider.setItemClickListener(new ItemClickListener() {
            @Override
            public void onItemSelected(int i) {
                Toast.makeText(MainActivity.this, "Clicked " + String.valueOf(i), Toast.LENGTH_LONG).show();
            }
        });

        findViewById(R.id.SearchBox).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, Searching.class));
            }
        });

        findViewById(R.id.QuitButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                LogOut();
                finish();
                System.exit(0);
            }
        });

        findViewById(R.id.MyAccountButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(MainActivity.this, AccountSetting.class));
            }
        });
    }

    private void top_movies(){
        String url = "https://bookbook3wishes.azurewebsites.net/api/movie/top/" + 3;

        JsonArrayRequest req = new JsonArrayRequest(Request.Method.GET, url, null, new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                ObjectMapper objectMapper = new ObjectMapper();
                for (int i= 0; i < response.length(); i++){
                    try {
                        Movie temp = objectMapper.readValue(response.getString(i), Movie.class);
                        top_movie.add(temp);
                        slideModelList.add(new SlideModel("https://bookbook3wishes.azurewebsites.net/api/image/" + temp.getImageID(), temp.getName()));
                    } catch (IOException | JSONException e) {
                        e.printStackTrace();
                    }
                }
                imageSlider.setImageList(slideModelList, true);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(MainActivity.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                else if (response.statusCode == 403){
                    Toast.makeText(MainActivity.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });
        MySingleton.getInstance(MainActivity.this).addToRequestQueue(req);
    }

    private void setup_SlideShow() {

        new SlideModel("https://wallpaperaccess.com/full/329583.jpg","");
        slideModelList.add(new SlideModel("https://cdn.wallpapersafari.com/46/77/tX3swr.jpg",""));
        slideModelList.add(new SlideModel("https://wallpaperaccess.com/full/107680.jpg",""));
        imageSlider.setImageList(slideModelList, true);


    }

    private void LogOut() {
        String url = "https://bookbook3wishes.azurewebsites.net/api/account/logout/" + SignIn.user.getSessionID();
        StringRequest req = new StringRequest(Request.Method.GET, url, null, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(MainActivity.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                else if (response.statusCode == 403){
                    Toast.makeText(MainActivity.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });
    }
}