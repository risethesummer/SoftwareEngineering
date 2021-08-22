package com.example.bookbook;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.ServerError;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonObjectRequest;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.DataInput;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.HashMap;
import java.util.Map;


public class SignIn extends AppCompatActivity {

    public static User user = new User();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

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

        findViewById(R.id.ForgetPasswordButton).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(SignIn.this, ResetPassword.class));
            }
        });
    }

    private void handle_loginDialog() {
        final EditText email = findViewById(R.id.editUsername);
        final EditText pass = findViewById(R.id.editPass);

        Map<String, String> postParam= new HashMap<String, String>();
        postParam.put("account", email.getText().toString());
        postParam.put("password", pass.getText().toString());

        String  url = "https://bookbook3wishes.azurewebsites.net/api/account/login";

        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, url, new JSONObject(postParam), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                ObjectMapper objectMapper = new ObjectMapper();
                try {
                    user = objectMapper.readValue(response.toString(), User.class);
                } catch (IOException e) {
                    e.printStackTrace();
                }
                Toast.makeText(SignIn.this, "Login successfully",Toast.LENGTH_SHORT).show();
                finish();
                startActivity(new Intent(SignIn.this, MainActivity.class));
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

                // As of f605da3 the following should work
                NetworkResponse response = error.networkResponse;
                if (response.statusCode == 400){
                    Toast.makeText(SignIn.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                if (response.statusCode == 403){
                    Toast.makeText(SignIn.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        }
        );
        MySingleton.getInstance(SignIn.this).addToRequestQueue(req);
    }

}