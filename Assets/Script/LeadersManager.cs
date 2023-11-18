using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Extensions;
using UnityEditor.U2D.Aseprite;
using Unity.VisualScripting;
using System.Linq;

public class LeadersManager : MonoBehaviour
{   public bool end=false;
    public DataSnapshot dataSnapshot;
    public List<UserData> RankList;
    public List<object> list;
    DatabaseReference reference=FirebaseDatabase.DefaultInstance.RootReference;
    void Awake() {
        RankList = new List<UserData>();        
        //list = new List<object>();
        RankUpload();
    }

    void Update()
    {
        if(end){
            end=false;
           RankSort();
        }
    }
    void RankUpload(){
        reference.Child("users").GetValueAsync().ContinueWithOnMainThread(task=>{
            if(task.IsFaulted){

            }else if(task.IsCompleted){
            dataSnapshot = task.Result;
            end= true;
            list =dataSnapshot.Children.ToList<object>();
            Debug.Log(dataSnapshot.Children.ToString());
            }
        });
    }


    void RankSort(){
         foreach(var data in dataSnapshot.Children){
            UserData userData = new UserData();
            userData.NickName = data.Child("Nickname").Value.ToString();
            userData.highScore=int.Parse(data.Child("highScore").Value.ToString());
            RankList.Add(userData);
            }
        RankList.Sort();
        foreach(UserData userData in RankList){
            Debug.Log(userData.highScore);
        }
        
    }
}
