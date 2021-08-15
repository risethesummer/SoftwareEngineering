package com.example.bookbook;

import android.app.DatePickerDialog;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;


public class SignUp extends AppCompatActivity {
    private TextView date_ofBirth;
    private DatePickerDialog.OnDateSetListener DateSet_listener;
    private static final String TAG = "SignUp";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);

        date_ofBirth = (TextView) findViewById(R.id.DoBBox);

        date_ofBirth.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setDate();
            }
        });

        DateSet_listener = new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                month += 1;
                Log.d(TAG, "onDateSet: dd/mm/yyy: " + dayOfMonth + "/" + month + "/" + year);

                date_ofBirth.setText(dayOfMonth + "/" + month + "/" + year);
            }
        };

        findViewById(R.id.SubmitButton).setOnClickListener(new View.OnClickListener(){
            @Override
            public void onClick(View v){
                handle_signUp_Dialog();
            }
        });
    }

    private void setDate() {
        Calendar cal = Calendar.getInstance();
        int year = cal.get(Calendar.YEAR);
        int month = cal.get(Calendar.MONTH);
        int day = cal.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(
                SignUp.this, android.R.style.Theme_Holo_Light_Dialog_MinWidth,
                DateSet_listener,
                year,month,day
        );

        datePickerDialog.getWindow().setBackgroundDrawable(
                new ColorDrawable(Color.TRANSPARENT));
        datePickerDialog.show();
    }

    private void handle_signUp_Dialog(){
        final EditText username = findViewById(R.id.UsernameBox);
        final EditText email = findViewById(R.id.emailBox);
        final EditText pass = findViewById(R.id.PasswordBox);
        final EditText name = findViewById(R.id.FullnameBox);
        final TextView DoB = findViewById(R.id.DoBBox);
        final EditText Address = findViewById(R.id.AddressBox);

        Map<String, String> postParam= new HashMap<String, String>();
        postParam.put("account", username.getText().toString());
        postParam.put("email", email.getText().toString());
        postParam.put("password", pass.getText().toString());
        postParam.put("name", name.getText().toString());
        postParam.put("dayOfBirth", DoB.getText().toString());
        postParam.put("address", Address.getText().toString());

        String  url = "https://bookbook3wishes.azurewebsites.net/api/account/register";

        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, url, new JSONObject(postParam), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                try {
                    if(response.get("State").equals("Success")){
                        Toast.makeText(SignUp.this, "Register successfully", Toast.LENGTH_LONG).show();
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
                    Toast.makeText(SignUp.this, "This username already has", Toast.LENGTH_LONG).show();
                }
                else if (response.statusCode == 403){
                    Toast.makeText(SignUp.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });
        MySingleton.getInstance(SignUp.this).addToRequestQueue(req);
    }

}