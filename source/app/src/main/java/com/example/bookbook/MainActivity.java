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
import com.android.volley.toolbox.StringRequest;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

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