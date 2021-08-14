package com.example.bookbook;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.UUID;


public class User {
    private UUID id;
    private String name;
    private String dayOfBirth;
    private String address;
    private String email;

    private HashMap<String,String> putData(){
        HashMap<String,String> map = new HashMap<>();
        map.put("ID", this.id.toString());
        map.put("Email", this.email);
        map.put("name", this.name);
        map.put("DoB", this.dayOfBirth);
        map.put("Address", this.address);
        return map;
    };

    public String getId() {
        return id.toString();
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDayOfBirth() {
        return dayOfBirth;
    }

    public void setDayOfBirth(String dayOfBirth) {
        this.dayOfBirth = dayOfBirth;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    private List<String> View_info(){
        List<String> info = new ArrayList<String>();
        info.add(id.toString()); info.add(email); info.add(name); info.add(dayOfBirth);info.add(address);
        return info;
    }
}
