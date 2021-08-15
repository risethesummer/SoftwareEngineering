package com.example.bookbook;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.JsonRequest;

import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.util.HashMap;

public class ResetPassword extends AppCompatActivity {
    private String URL_base = "https://bookbook3wishes.azurewebsites.net/api/account/reset";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_reset_password);

        findViewById(R.id.recoverpass_next_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                reset_password();
            }
        });
    }

    private void reset_password() {
        EditText email = (EditText) findViewById(R.id.recover_pass_email_text);
        EditText username = (EditText) findViewById(R.id.recover_pass_username_text);

        HashMap<String, String> params = new HashMap<>();
        params.put("Account",username.getText().toString());
        params.put("Email",email.getText().toString());

        JsonObjectRequest req = new JsonObjectRequest(Request.Method.POST, URL_base, new JSONObject(params), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                Toast.makeText(ResetPassword.this,response.toString(),Toast.LENGTH_LONG).show();
                try {
                    if (response.get("State").equals("Success")) {
                        confirm_passcode(response.get("ID").toString());
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
                    Toast.makeText(ResetPassword.this, "This account is already logged in", Toast.LENGTH_LONG).show();
                }
                if (response.statusCode == 403){
                    Toast.makeText(ResetPassword.this, "The server is closed", Toast.LENGTH_LONG).show();
                }
            }
        });
        MySingleton.getInstance(ResetPassword.this).addToRequestQueue(req);
    }

    private void confirm_passcode(String ID){
        setContentView(R.layout.passcode_confirm);
        findViewById(R.id.passcode_confirm_button).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                sendPassCode(ID);
            }
        });
    }

    private void sendPassCode(String ID) {
        String url = URL_base + '/' + ID;
        EditText passcode = (EditText) findViewById(R.id.passcode_editText);
        HashMap<String,String> params = new HashMap<>();
        params.put("MailCode", passcode.getText().toString());

        JsonRequest req = new JsonObjectRequest(Request.Method.DELETE, url, new JSONObject(params), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                try {
                    if (response.get("State").equals("Success"))
                        create_newPassword(url);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });
        MySingleton.getInstance(ResetPassword.this).addToRequestQueue(req);
    }

    private void create_newPassword(String url){
        setContentView(R.layout.activity_change_pw_forget);
        TextView new_password = (TextView) findViewById(R.id.EnterNewPassword_forget);
        TextView retype_new_password = (TextView) findViewById(R.id.EnterPasswordAgain_forget);

        findViewById(R.id.Confirm_pwd_forget).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (new_password.getText().toString().equals(retype_new_password.getText().toString())){
                    send_newPassword(url, new_password.getText().toString());
                }
                else{
                    Toast.makeText(ResetPassword.this, "password is not match", Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    private void send_newPassword(String url , String new_password){
        HashMap<String,String> params = new HashMap<>();
        params.put("NewPassword", new_password);

        JsonRequest req  = new JsonObjectRequest(Request.Method.PUT, url, new JSONObject(params), new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                try {
                    if (response.get("State").equals("Success")){
                        Toast.makeText(ResetPassword.this, "Change password successfully", Toast.LENGTH_LONG).show();
                        finish();
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });
        MySingleton.getInstance(ResetPassword.this).addToRequestQueue(req);
    }
}