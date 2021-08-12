package com.example.bookbook;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.CheckBox;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.Calendar;
import java.util.HashMap;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

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

        findViewById(R.id.SignUpButton).setOnClickListener(new View.OnClickListener(){
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

    private void handle_signUp_Dialog() {
        final EditText username = findViewById(R.id.UsernameBox);
        final EditText email = findViewById(R.id.AddressBox);
        final EditText pass = findViewById(R.id.PasswordBox);
        final EditText name = findViewById(R.id.FullnameBox);
        final TextView DoB = findViewById(R.id.DoBBox);
        final EditText Address = findViewById(R.id.AddressBox);

        HashMap<String,String> map = new HashMap<>();

        map.put("account", username.getText().toString());
        map.put("password", pass.getText().toString());
        map.put("name", name.getText().toString());
        map.put("email", email.getText().toString());
        map.put("dayOfBirth", DoB.getText().toString());
        map.put("address", Address.getText().toString());

        Call<Void> call = SignIn.retrofitInterfaceUser.executeSignUp(map);

        call.enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                if (response.code() == 200){
                    Toast.makeText(SignUp.this, "Sign up successfully", Toast.LENGTH_LONG).show();
                    new Intent(SignUp.this, SignIn.class);
                }
                else if (response.code() == 400){
                    Toast.makeText(SignUp.this, "Email is used", Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Toast.makeText(SignUp.this, t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }

}