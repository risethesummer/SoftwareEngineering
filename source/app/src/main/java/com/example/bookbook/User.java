package com.example.bookbook;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.widget.ArrayAdapter;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;


import retrofit2.Call;

public class User {
    private String ID;
    private String name;
    private String DoB;
    private String Address;
    private String Email;

    private HashMap<String,String> putData(){
        HashMap<String,String> map = new HashMap<>();
        map.put("ID", this.ID);
        map.put("Email", this.Email);
        map.put("name", this.name);
        map.put("DoB", this.DoB);
        map.put("Address", this.Address);
        return map;
    };

    private List<String> View_info(){
        List<String> info = new ArrayList<String>();
        info.add(ID); info.add(Email); info.add(name); info.add(DoB);info.add(Address);
        return info;
    }
}
