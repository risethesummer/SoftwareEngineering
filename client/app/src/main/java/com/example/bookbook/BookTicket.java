package com.example.bookbook;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Parcelable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class BookTicket extends AppCompatActivity {
    private  ArrayList<Ticket> all_tickets;
    private  TextView show_time_textBox;
    private  TextView cinema_textBox;
    private  String Theater_ID;
    private Ticket chosen;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_book_ticket);

        all_tickets = (ArrayList<Ticket>) getIntent().getSerializableExtra("Tickets");

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
                    Intent choose_seat = new Intent(BookTicket.this, ChooseSeat.class);
                    choose_seat.putExtra("ticket", chosen);
                    choose_seat.putExtra("theater", Theater_ID);
                    finish();
                    startActivity(choose_seat);
                }
            }
        });

    }

    private void choose_cinema(){

        String[] List_items;
        List<Theater> list = all_tickets.get(0).getTheaters();
        ArrayList<String> theater_list = new ArrayList<>();
        for (int i = 0; i < list.size(); i++){
            theater_list.add(list.get(i).getName());
        }

        List_items = theater_list.toArray(new String[0]);

        AlertDialog.Builder mBuilder = new AlertDialog.Builder(BookTicket.this);
        mBuilder.setTitle("Choose a cinema");
        mBuilder.setSingleChoiceItems(List_items, -1, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                cinema_textBox.setText(List_items[which]);
                if (which == -1){
                    show_time_textBox.setText("Date");
                }

                Theater_ID = list.get(which).id;
                show_time_textBox.setEnabled(true);
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
        List<String> showtime_list = new ArrayList<>();

        for (int i = 0; i < all_tickets.size(); i++){
            showtime_list.add(all_tickets.get(i).getShowTime());
        }

        List_items = showtime_list.toArray(new String[0]);
        AlertDialog.Builder mBuilder = new AlertDialog.Builder(BookTicket.this);
        mBuilder.setTitle("Choose a showtime");
        mBuilder.setSingleChoiceItems(List_items, -1, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                show_time_textBox.setText(List_items[which]);

                if(which == -1) return;

                for (int i = 0; i < all_tickets.size(); i++){
                    if(all_tickets.get(i).getShowTime() == List_items[which]){
                        chosen = all_tickets.get(i);
                        break;
                    }
                }

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