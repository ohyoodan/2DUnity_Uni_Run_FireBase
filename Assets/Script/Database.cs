using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
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

   public void DatabaseUser_Create(string UID,string nickName){
      userData = new UserData(nickName);
      string json = JsonUtility.ToJson(userData);
      reference.Child("users").Child(UID).SetRawJsonValueAsync(json);
  }

public string userNickNameOut(){
        return auth.CurrentUser.DisplayName;
    }
  public void UserFile_Create(string nickName){
  UserProfile userProfile= new UserProfile{
      DisplayName=nickName, 
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
  }
  public void CurrentUser_ScoreAdd(int score){
    reference.Child("users").Child(user.UserId).Child("HighScore").SetValueAsync(score);
  }
  public void User_Auth_Current(){
    user=auth.CurrentUser;
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
      highScore=int.Parse(snapshot.Child(user.UserId).Child("HighScore").Value.ToString());
      end=true;      
      }
    });
    
  }

  public void LogOut(){
        auth.SignOut();
  }
}
  
[System.Serializable]
public class UserData{
public UserData(string nickName){NickName=nickName;}
public UserData(string nickName,int highScore){NickName=nickName;HighScore=highScore;}
public string NickName;
public int HighScore=0;
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


