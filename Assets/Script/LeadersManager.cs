using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Extensions;
using Doozy.Runtime.UIManager.Components;
using UnityEngine.UI;

public class LeadersManager : MonoBehaviour
{   public bool end=false;
    public DataSnapshot dataSnapshot;
    public List<UserData> RankList;
    DatabaseReference reference;
    [SerializeField]GridLayoutGroup RankBoard;
    [SerializeField]UIStepper uIStepper;
    [SerializeField]UIScrollbar uIScrollbar; 
    void Awake() {
        RankList = new List<UserData>();        
        reference=FirebaseDatabase.DefaultInstance.RootReference;
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
            }
        });
    }

    void RankSort(){
            foreach(var data in dataSnapshot.Children){
            UserData userData = new UserData(data.Child("NickName").Value.ToString());
            userData.HighScore=int.Parse(data.Child("HighScore").Value.ToString());
            RankList.Add(userData);
            }
            RankList.Sort(new SortUserData());
            int RankNum=0;
            foreach(UserData userData1 in RankList){
                RankNum++;
                userData1.Rank=RankNum;
            }
    }

    /// <summary>
    /// 일부로 분해 함 .. -> 나중에 서치 할 경우가 생겼을 때를 위한 것임. 즉 코드에서 실행할 때 위한 것
    /// </summary>
    /// <param name="RankBoardNumber"></param>
    void RankBoard_Contents(float RankBoardNumber){
         int childCount = RankBoard.transform.childCount;
         int RankListNum;
         RankListNum=(int)(RankBoardNumber-1)*childCount;
            for(int i =0; i< childCount; i++){
            RankBoard_Content currentRankBoardContent=RankBoard.transform.GetChild(i).GetComponent<RankBoard_Content>();
            if(RankList.Count>RankListNum){
            currentRankBoardContent.RankNumSet(RankList[RankListNum].Rank.ToString());
            currentRankBoardContent.NickNameSet(RankList[RankListNum].NickName);
            currentRankBoardContent.HighScoreSet(RankList[RankListNum].HighScore.ToString());
            }else{            
            currentRankBoardContent.RankNumSet("-");    
            currentRankBoardContent.NickNameSet("-");
            currentRankBoardContent.HighScoreSet("-");
            }
            RankListNum++;
         }
    }

    void RankBoard_Contents_Value(float RankBoardNumber){
         int childCount = RankBoard.transform.childCount;
         int RankListNum;
         RankListNum=(int)(RankBoardNumber-1)*childCount;
         Debug.Log(RankListNum);
            for(int i =0; i< childCount; i++){
            RankBoard_Content currentRankBoardContent=RankBoard.transform.GetChild(i).GetComponent<RankBoard_Content>();
            if(RankList.Count>RankListNum){
            currentRankBoardContent.RankNumSet(RankList[RankListNum].Rank.ToString());
            currentRankBoardContent.NickNameSet(RankList[RankListNum].NickName);
            currentRankBoardContent.HighScoreSet(RankList[RankListNum].HighScore.ToString());
            }else{            
            currentRankBoardContent.RankNumSet("-");    
            currentRankBoardContent.NickNameSet("-");
            currentRankBoardContent.HighScoreSet("-");
            }
            RankListNum++;
         }
    }
   public void RankBoard_Contents_Button()=>RankBoard_Contents(uIStepper.value);
    
    public void RankBoard_Contents_Value()=>RankBoard_Contents_Value(uIScrollbar.value);
}
    
