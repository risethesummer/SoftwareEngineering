package com.example.bookbook;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;


public class User {
    private String sessionID;
    private String account;
    private String name;
    private String email;
    private String dayOfBirth;
    private String address;


    private HashMap<String,String> putData(){
        HashMap<String,String> map = new HashMap<>();
        map.put("ID", this.sessionID);
        map.put("Email", this.email);
        map.put("name", this.name);
        map.put("DoB", this.dayOfBirth);
        map.put("Address", this.address);
        return map;
    };

    public String getSessionID() {
        return sessionID.toString();
    }

    public void setSessionID(String sessionID) {
        this.sessionID = sessionID;
    }

    public String getAccount(){return account;}

    public void setAccount(String account){this.account = account;}

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
        info.add(sessionID); info.add(email); info.add(name); info.add(dayOfBirth);info.add(address);
        return info;
    }
}
