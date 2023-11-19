using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro_Manager : MonoBehaviour
{
    [SerializeField]TMP_InputField emailField;
    [SerializeField]TMP_InputField passField;
    [SerializeField]FireBaseAuth_Manager fireBaseAuth_Manager;
    void Update(){
        if(fireBaseAuth_Manager.Loginsuccess){
            Scene();
        }
    }
    void Scene(){
        SceneManager.LoadScene(1); 
    }
    public void Create_account(){
        fireBaseAuth_Manager.Create(emailField.text,passField.text);
    }
    public void Login(){
        fireBaseAuth_Manager.Login(emailField.text,passField.text);
    }
}
