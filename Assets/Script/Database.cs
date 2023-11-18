using System.Collections;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System;
public class Database : MonoBehaviour 
{  
  DatabaseReference reference;
  UserData userData;
  public static Database instance=null;
  FirebaseUser user;
  public int highScore;
  public bool end=false;
  private FirebaseAuth auth;
   private void Awake(){
        if(null==instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
      reference = FirebaseDatabase.DefaultInstance.RootReference;
      auth=FirebaseAuth.DefaultInstance;
    }

   public void Email_Create(string UID,string NickName){
      userData = new UserData();
      userData.NickName=NickName;
      UserProfile userProfile= new UserProfile{
      DisplayName=NickName, 
      };
      user.UpdateUserProfileAsync(userProfile).ContinueWithOnMainThread(task=>
      {
        if(task.IsCanceled){
          return;
        }
        if(task.IsFaulted){
          return;
        }
      });
      string json = JsonUtility.ToJson(userData);
      reference.Child("users").Child(UID).SetRawJsonValueAsync(json);
  }

  public void CurrentUser_ScoreAdd(int score){
    reference.Child("users").Child(user.UserId).Child("highScore").SetValueAsync(score);
  }
  
  public void Get(){
    reference.Child("users").GetValueAsync().ContinueWithOnMainThread(task=>
    {
      if(task.IsFaulted){
      Debug.Log("오류");
      }
      else if(task.IsCompleted)
      {
      DataSnapshot snapshot =task.Result;      
      highScore=int.Parse(snapshot.Child(user.UserId).Child("highScore").Value.ToString());
      end=true;      
      }
    });
    
  }
}
  
[System.Serializable]
public class UserData{
public UserData(){}
public string NickName;
public int highScore=0;
public int Rank=0;

}

// public class SortUserData : IComparer<UserData>
// {
//   public int Comparer(UserData x, UserData y){
//   if(x.highScore==y.highScore){
//     return 0;
//   }
//   return x.highScore.CompareTo(y.Rank)*-1;
//   }
//}


