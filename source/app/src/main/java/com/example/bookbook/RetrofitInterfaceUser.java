package com.example.bookbook;

import java.util.HashMap;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface RetrofitInterfaceUser {

    @POST("/account/login")
    Call<User> executeLogin(@Body HashMap<String, String> map);
    @POST("/account/register")
    Call<Void> executeSignUp(@Body HashMap<String, String> map);
    @POST("/edit_info")
    Call<User> executeEditInfo(@Body HashMap<String, String> map);
    @POST("/movie/search_by_name")
    Call<List<Movie>> executeSearching_name(@Body HashMap<String, String> map);
    @POST("/movie/search_by_genre")
    Call<List<Movie>> executeSearching_genre(@Body HashMap<String, String> map);
    @POST("/movie/search_by_nation")
    Call<List<Movie>> executeSearching_nation(@Body HashMap<String, String> map);
    @POST("/movie/search_by_actor")
    Call<List<Movie>> executeSearching_actor(@Body HashMap<String, String> map);
    @POST("/movie/search_by_theater")
    Call<List<Movie>> executeSearching_theater(@Body HashMap<String, String> map);
}
