package com.example.bookbook;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.TextView;

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

    View create_Frame(LayoutInflater inflater){
        View result = inflater.inflate(R.layout.movie_frame, null);

        TextView name = (TextView)result.findViewById(R.id.textView_name);
        name.setText(this.Name);
        TextView nation = (TextView)result.findViewById(R.id.textView_nation);
        nation.setText(this.Theaters.toString());
        TextView cinema = (TextView)result.findViewById(R.id.textView_cinema);
        cinema.setText(this.Theaters.toString());

        result.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //TODO switch screen to movie page
            }
        });

        return result;
    };
}
