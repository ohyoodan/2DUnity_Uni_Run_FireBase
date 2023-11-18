using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro_Manager : MonoBehaviour
{
    [SerializeField]TMP_InputField emailField;
    [SerializeField]TMP_InputField passField;
    void Update(){
        if(FireBaseAuth_Manager.instance.Loginsuccess){
            Scene();
        }
    }
    void Scene(){
        SceneManager.LoadScene(1); 
    }
    public void Create_account(){
        FireBaseAuth_Manager.instance.Create(emailField.text,passField.text);
    }
    public void Login(){
        FireBaseAuth_Manager.instance.Login(emailField.text,passField.text);
    }
}
