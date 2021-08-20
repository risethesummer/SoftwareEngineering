package com.example.bookbook;

import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

public class BookTicket extends AppCompatActivity {
    private  Movie info;
    private  TextView show_time_textBox;
    private  TextView cinema_textBox;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_ticket);

        info = (Movie) getIntent().getSerializableExtra("movie");

        cinema_textBox = findViewById(R.id.listbox_theater_booking);
        cinema_textBox.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                choose_cinema();
            }
        });

        show_time_textBox = findViewById(R.id.listbox_date_booking);
        show_time_textBox.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!show_time_textBox.isEnabled() || cinema_textBox.getText().toString() == "Cinema"){
                    Toast.makeText(BookTicket.this, "You must choose a cinema first", Toast.LENGTH_SHORT).show();
                    return;
                }
                choose_showtime();
            }
        });

        findViewById(R.id.book_ticket_next1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (cinema_textBox.getText().toString() != "Cinema"
                 && show_time_textBox.getText().toString() != "Date"){
                    startActivity(new Intent());
                }
            }
        });

    }

    private void choose_cinema(){

        String[] List_items;
        List_items = (String[]) info.Theaters.toArray();
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(BookTicket.this);
        mBuilder.setTitle("Choose a cinema");
        mBuilder.setSingleChoiceItems(List_items, -1, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                cinema_textBox.setText(List_items[which]);
                if (which == -1){
                    show_time_textBox.setText("Date");
                }
                dialog.dismiss();
            }
        });

        mBuilder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                if (cinema_textBox.getText().toString() == "Cinema"){
                    show_time_textBox.setText("Date");
                }
            }
        });
        mBuilder.create().show();
    }

    private void choose_showtime() {
        String[] List_items;
        List_items = (String[]) info.Theaters.toArray();
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(BookTicket.this);
        mBuilder.setTitle("Choose a showtime");
        mBuilder.setSingleChoiceItems(List_items, -1, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                show_time_textBox.setText(List_items[which]);
                dialog.dismiss();
            }
        });

        mBuilder.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
            }
        });
        mBuilder.create().show();
    }
}