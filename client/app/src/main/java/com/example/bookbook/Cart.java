package com.example.bookbook;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.android.volley.toolbox.JsonRequest;

import org.json.JSONObject;

public class Cart extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cart);

        findViewById(R.id.PaymentButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                order();
            }
        });
    }

    private void order() {
        String url = "https://bookbook3wishes.azurewebsites.net/api/order";
        JsonRequest<JSONObject> req;
    }
}