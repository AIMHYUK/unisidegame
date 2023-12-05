using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI를 사용할 때 필요

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject nextbutton;
    public GameObject restartButton;

    Image titleImage;
    void Start() //첫 프레임 호출전에 호출
    {
        Invoke("InactiveImage", 1.0f); //이미지숨기기
        panel.SetActive(false); //패널 숨기기(버튼)
    }

    void Update() //1프레임마다 호출
    {
        if(PlayerController.gameState == "gameclear"){ //게임클리어
            mainImage.SetActive(true);
            panel.SetActive(true); //이미지,패널 표시

            //Restart 버튼 무효화
            Button bt = restartButton.GetComponent<Button>(); //버튼 컴포넌트 가져옴
            bt.interactable = false; //비활성
            mainImage.GetComponent<Image>().sprite = gameClearSpr; //반투명
            PlayerController.gameState = "gameclear";
        }
        else if (PlayerController.gameState == "gameover"){ //게임오버
            mainImage.SetActive(true);
            panel.SetActive(true); //이미지,패널 표시

            //Next버튼 무효화
            Button bt = nextbutton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";   
        }
        else if(PlayerController.gameState == "playing"){ //게임중
        
        }
    }
    void InactiveImage(){
        mainImage.SetActive(false); //왜 이미지만 따로 만들었을까?
    }
}
