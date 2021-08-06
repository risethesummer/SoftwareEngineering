package com.example.bookbook;
import com.google.gson.annotations.SerializedName;

public class sign_in {
    private String email;
    private String password;

    sign_in(String E, String P){
        this.email = E;
        this.password = P;
    }
}
