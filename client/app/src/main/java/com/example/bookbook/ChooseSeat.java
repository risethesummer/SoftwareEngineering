package com.example.bookbook;

import android.app.DownloadManager;
import android.content.Intent;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.JsonRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ChooseSeat extends AppCompatActivity implements View.OnClickListener {
    private HashMap<Button, Boolean> seatList = new HashMap<Button, Boolean>();
    private String seat_choose;
    private int count = 0;
    private Ticket ticket;
    private String theaterID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_choose_seat);

        ticket = (Ticket) getIntent().getSerializableExtra("ticket");
        theaterID = (String) getIntent().getSerializableExtra("theater");

        set_upButtons(5, "A_");
        set_upButtons(7, "B_");
        set_upButtons(7, "C_");

        book_ticket();
    }

    private void book_ticket() {
        findViewById(R.id.Book_Button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                order();
            }
        });
    }

    private void order() {
        if (seatList.isEmpty()) {
            Toast.makeText(ChooseSeat.this, "You must choose your seat first", Toast.LENGTH_LONG).show();
            return;
        }
        for (Map.Entry ele : seatList.entrySet() ){
            if ((Boolean) ele.getValue() == true){
                Button seat = (Button) ele.getKey();
                seat_choose = seat.getResources().getResourceEntryName(seat.getId());
            }
        }

        String url = "https://bookbook3wishes.azurewebsites.net/api/order";

        HashMap<String,String> params = new HashMap<>();

        params.put("sessionID", SignIn.user.getSessionID());
        params.put("theaterID", theaterID);
        params.put("productID", ticket.getId());

        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, url, new JSONObject(params), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                try {
                    if(response.get("state").equals("Success")){
                        Toast.makeText(ChooseSeat.this, "Order successfully", Toast.LENGTH_LONG).show();
                        finish();
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(ChooseSeat.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                else if (response.statusCode == 403){
                    Toast.makeText(ChooseSeat.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });

        MySingleton.getInstance(ChooseSeat.this).addToRequestQueue(req);

    }

    @RequiresApi(api = Build.VERSION_CODES.M)
    @Override
    public void onClick(View v) {
        boolean selected = seatList.get((Button) v);

        if(selected == false && count == 1) return;

        selected = !selected;
        seatList.put((Button)v, selected);

        if (selected){ v.setForeground(getResources().getDrawable(R.drawable.selected_seat));
            count = 1;
        }
        else{
            v.setForeground(getResources().getDrawable(R.drawable.empty_seat));
        }

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