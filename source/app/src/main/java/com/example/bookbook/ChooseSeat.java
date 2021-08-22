package com.example.bookbook;

import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ChooseSeat extends AppCompatActivity implements View.OnClickListener {
    private HashMap<Button, Boolean> seatList = new HashMap<Button, Boolean>();
    private HashMap<Product, Integer> tickets = new HashMap<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_choose_seat);

        set_upButtons(5, "A_");
        set_upButtons(7, "B_");
        set_upButtons(7, "C_");

        book_ticket();
    }

    private Ticket clone_ticket(String seat){
        Ticket clone = new Ticket();
        clone.setSeat(seat);

        return clone;
    }

    private void book_ticket() {
        findViewById(R.id.Book_Button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                for (Map.Entry ele : seatList.entrySet() ){
                    if ((Boolean) ele.getValue() == true){
                        Button seat = (Button) ele.getKey();
                        clone_ticket(seat.getResources().getResourceEntryName(seat.getId()));
                    }
                }
            }
        });
    }

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    public void onClick(View v) {
        boolean selected = seatList.get((Button) v);
        selected = !selected;
        seatList.put((Button)v, selected);

        if (!selected){ v.setForeground(getResources().getDrawable(R.drawable.empty_seat)); }
        else{ v.setForeground(getResources().getDrawable(R.drawable.selected_seat)); }

    }

    private void set_upButtons(int size, String prefix){
        for (int i = 1; i <= size; i++){
            int id = getResources().getIdentifier(prefix + i, "id", getPackageName());
            Button ele = (Button) findViewById(id);
            ele.setOnClickListener(this);
            seatList.put(ele, false);
        }
    }
}