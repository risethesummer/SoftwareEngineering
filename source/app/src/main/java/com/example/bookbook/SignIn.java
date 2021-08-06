package com.example.bookbook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

import java.util.HashMap;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;


public class SignIn extends AppCompatActivity {
    private retrofit2.Retrofit retrofit;
    private RetrofitInterfaceUser retrofitInterfaceUser;
    private String BASE_URL = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

        //retrofit = new Retrofit.Builder().baseUrl(BASE_URL)
         //       .addConverterFactory(GsonConverterFactory.create()).build();

        //retrofitInterfaceUser = retrofit.create(RetrofitInterfaceUser.class);

        findViewById(R.id.LoginButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                handle_loginDialog();
            }
        });

        findViewById(R.id.SignUpButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(SignIn.this, SignUp.class));
            }
        });
    }

    private void handle_loginDialog() {
        EditText email = findViewById(R.id.editEmail);
        EditText pass = findViewById(R.id.editPass);
        HashMap<sign_in,String> map = new HashMap<>();

    }
}