package com.example.bookbook;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

public class InformationMovie extends AppCompatActivity {
    private Movie info;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_information_movie);

        get_info();
        set_content();
        Book();
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
    };

    private void Book(){
        findViewById(R.id.book_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

            }
        });
    };
}