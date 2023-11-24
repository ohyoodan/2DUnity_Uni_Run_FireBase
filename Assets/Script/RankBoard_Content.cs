using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankBoard_Content : MonoBehaviour
{   [SerializeField]TextMeshProUGUI textNickName;
    [SerializeField]TextMeshProUGUI textRankNum;
    [SerializeField]TextMeshProUGUI textHighScore;

    public void NickNameSet(string NickNameInput){
        textNickName.text=NickNameInput;
    }
    public void RankNumSet(string RankNumberInput){
        textRankNum.text=RankNumberInput;
    }
    public void HighScoreSet(string HighScoreInput){
        textHighScore.text="HighScore: "+HighScoreInput;
    }
}
