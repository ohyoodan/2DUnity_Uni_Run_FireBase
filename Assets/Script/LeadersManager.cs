using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Extensions;
using System;

public class LeadersManager : MonoBehaviour
{   public bool end=false;
    public DataSnapshot dataSnapshot;
    public List<UserData> RankList;
    DatabaseReference reference=FirebaseDatabase.DefaultInstance.RootReference;
    void Awake() {
        RankList = new List<UserData>();        
        RankUpload();
    }

    void Update()
    {
        if(end){
            end=false;
            //RankSort();
        }
    }
    void RankUpload(){
        reference.Child("users").GetValueAsync().ContinueWithOnMainThread(task=>{
            if(task.IsFaulted){

            }else if(task.IsCompleted){
            dataSnapshot = task.Result;
            end= true;
            }
        });
    }

    void RankSort(){
            Debug.Log(dataSnapshot.Value.ToString());
            foreach(var data in dataSnapshot.Children){
            UserData userData = new UserData(data.Child("Nickname").Value.ToString());
            userData.HighScore=int.Parse(data.Child("HighScore").Value.ToString());
            RankList.Add(userData);
            }
            RankList.Sort();
            foreach(UserData userData in RankList){
            Debug.Log(userData.HighScore);
            }
        
    }
}
