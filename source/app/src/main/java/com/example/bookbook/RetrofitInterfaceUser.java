package com.example.bookbook;

import java.util.HashMap;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface RetrofitInterfaceUser {

    @POST("/login")
    Call<User> executeLogin(@Body HashMap<String, String> map);
    @POST("/signup")
    Call<Void> executeSignUp(@Body HashMap<String, String> map);
    @POST("/edit_info")
    Call<User> executeEditInfo(@Body HashMap<String, String> map);
}
