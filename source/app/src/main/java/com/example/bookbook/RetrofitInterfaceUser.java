package com.example.bookbook;

import java.util.HashMap;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface RetrofitInterfaceUser {

    @POST("/login")
    Call<sign_in> executeLogin(@Body HashMap<sign_in, String> map);
    @POST("/signup")
    Call<Void> executeSignUp(@Body HashMap<String, String> map);
}
