using UnityEngine;
using TMPro;
using Doozy.Runtime.Signals;
using System.Collections;
public class NickName : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nick;
    [SerializeField]
    TMP_InputField nickNameInput;
    string nickNameCheck;
    IEnumerator SignalSend_Time(bool b){
        
        yield return new WaitForEndOfFrame();//=> Nody 좀 기달려줘.
        if(b==false)
        {
            Signal.Send("Lobby","NickName",false);
        }
        else if(b==true)
        {
            Signal.Send("Lobby","NickName",true);
            nick.text=nickNameCheck;
        }
    }
    public void SignalSend_(){
        if(Database.instance!=null){
        nickNameCheck=Database.instance.userNickNameOut();
        }   
        bool b;
        if(nickNameCheck==""){
            b=false;
        }else{
            b=true;
        }
        StartCoroutine("SignalSend_Time",b);
        }
    public void CreateUserProfile_Button()=>StartCoroutine(CreateUserProfile_Button_Cor());

    IEnumerator CreateUserProfile_Button_Cor(){
        Database.instance.UserFile_Create(nickNameInput.text);//기다리게 해야하나 일단 대충 보류. 멀티 스레드의 단점?
        while(!Database.instance.end){
        yield return null;
        Debug.Log("로딩 중..");
        }
        Database.instance.end=false;
        nick.text=nickNameInput.text;
        nickNameCheck=nickNameInput.text;
        // 생성 전에 확인 가능
        Database.instance.DatabaseUser_Create(nick.text);
        Signal.Send("Lobby","NickName",true);
    }
}
