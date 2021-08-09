package com.example.bookbook;

import android.content.Context;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.ListIterator;
import java.util.Vector;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

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
                execute_searching(button, search_box.getText().toString());

            }
        });
        findViewById(R.id.button_byNation).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Button button = (Button)v;
                execute_searching(button, search_box.getText().toString());
            }
        });
        findViewById(R.id.button_byActor).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Button button = (Button)v;
                execute_searching(button, search_box.getText().toString());
            }
        });
        findViewById(R.id.button_byGenre).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Button button = (Button)v;
                execute_searching(button, search_box.getText().toString());
            }
        });
        findViewById(R.id.button_byTheatre).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Button button = (Button)v;
                execute_searching(button, search_box.getText().toString());
            }
        });
    }

    void execute_searching(Button filter, String data){
        Vector<Movie> result = new Vector<>();

        HashMap<String,String> map = new HashMap<>();
        map.put(filter.getText().toString(), data);
        Call<List<Movie>> call = SignIn.retrofitInterfaceUser.executeSearching_name(map);

        call.enqueue(new Callback<List<Movie>>() {
            @Override
            public void onResponse(Call<List<Movie>> call, Response<List<Movie>> response) {
                if (response.code() == 200){
                    list = response.body();
                    Show_movies_list();
                }
                else if (response.code() == 404){
                    Toast.makeText(Searching.this, "Wrong Credentials", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<Movie>> call, Throwable t) {
                Toast.makeText(Searching.this, t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }

    void Show_movies_list(){
        ListIterator<Movie> iterator = list.listIterator();
        while(iterator.hasNext()){
            List_viewLayout.addView(iterator.next().create_Frame(inflater));
        }
    }

}