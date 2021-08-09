package com.example.bookbook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import java.util.HashMap;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;


public class SignIn extends AppCompatActivity {
    public static retrofit2.Retrofit retrofit;
    public static RetrofitInterfaceUser retrofitInterfaceUser;
    private String BASE_URL = "https://localhost:5001";
    public User user = new User();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

        retrofit = new Retrofit.Builder().baseUrl(BASE_URL)
               .addConverterFactory(GsonConverterFactory.create()).build();

        retrofitInterfaceUser = retrofit.create(RetrofitInterfaceUser.class);

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
        final EditText email = findViewById(R.id.editUsername);
        final EditText pass = findViewById(R.id.editPass);
        HashMap<String,String> map = new HashMap<>();

        map.put("username", email.getText().toString());
        map.put("password", pass.getText().toString());
        Call<User> call = retrofitInterfaceUser.executeLogin(map);

        call.enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {
                if (response.code() == 200){
                    user = response.body();
                    startActivity(new Intent(SignIn.this, MainActivity.class));
                }
                else if (response.code() == 404){
                    Toast.makeText(SignIn.this, "Wrong Credentials", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {
                Toast.makeText(SignIn.this, t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }
}