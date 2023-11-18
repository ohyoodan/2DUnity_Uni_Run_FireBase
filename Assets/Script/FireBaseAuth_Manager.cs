using UnityEngine;
using Firebase.Auth;

public class FireBaseAuth_Manager : MonoBehaviour
{
    private FirebaseAuth auth;
    public static FireBaseAuth_Manager instance=null;
    public bool Loginsuccess =false;
     private void Awake() {
        if(null==instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        auth=FirebaseAuth.DefaultInstance;
        
    }
    
    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email,password).ContinueWith
        (task=> 
        { 
                if(task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Debug.Log(email + "성공.\n");   
                    Loginsuccess =true;
                }else
                {
                    Debug.Log("실패.\n");
                }
        }
        );
        
    }
    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email,password).ContinueWith
        (
            task => 
            {
                if(task.IsCanceled)
                {
                    Debug.Log("회원가입 실패 \n");
                    return;
                }
                if(task.IsFaulted)
                {
                    Debug.Log("회원가입 실패 \n");
                    return;
                }
                string[] str;
                str=email.Split("@");
                
                Database.instance.Email_Create(auth.CurrentUser.UserId,str[0]);
            }
        );
    }
    public void LogOut(){
        auth.SignOut();
    }

}
