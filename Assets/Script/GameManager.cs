using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수
    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트    
    public event Action GameStartEvent;
    bool gameStart = false;
    public bool GameStart { get { return gameStart; } }
    private int score = 0; // 게임 점수
    [SerializeField] Text GameCountText;

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        GameStartEvent += GameStartboolStart;
    }

    void Start()
    {
        StartCoroutine(GameStartCountGO());
    }

    public void GameOverRestart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOut(){
        SceneManager.LoadScene(1);
    }
    IEnumerator GameStartCountGO()
    {
        GameCountText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        GameCountText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        GameCountText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        GameCountText.text = "Game Start";
        GameStartEvent();
        yield return new WaitForSecondsRealtime(0.5f);
        GameCountText.text = "";
        GameStartEvent -= GameStartboolStart;
    }
    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        // 게임 오버가 아니라면
        if (!isGameover)
        {
            // 점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    public void OnPlayerDead()=>StartCoroutine("OnPlayerDead_Cor");
    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    IEnumerator OnPlayerDead_Cor()
    {
        //게임 초기화
        gameStart = false;
        // 현재 상태를 게임 오버 상태로 변경
        isGameover = true;
        
        Database.instance.Get();
        while(!Database.instance.end){
            yield return null;
        }
        //게임 오버 이벤트 실행
        if(Database.instance.highScore<score){
            Database.instance.CurrentUser_ScoreAdd(score);
        }
        
        // 게임 오버 UI를 활성화
        gameoverUI.SetActive(true);//뷰
    }
    
    void GameStartboolStart()
    {
        gameStart = true;
    }
}