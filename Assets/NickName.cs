using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NickName : MonoBehaviour
{
    [SerializeField]
     TextMeshProUGUI Nick;
    [SerializeField]
     InputField NickNameInput;
    void Start()
    {
        string NickNameCheck=Database.instance.userNickNameOut();
        if(NickNameCheck==null){


         //   Database.instance.UserFile_Create();
        }
    }

}
