using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true; //카운트다운으로 시간 측정
    public float gameTime = 0; //게임의 최대 시간
    public bool isTimeOver = false; //true이면 타이머 정지
    public float displayTime = 0; //표시되는 시간

    float times = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(isCountDown){
            displayTime = gameTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeOver == false)
        { //카운트다운
            times += Time.deltaTime;
            if (isCountDown)
            {
                displayTime = gameTime - times;
                if (displayTime <= 0.0f)
                {
                    displayTime = 0.0f;
                    isTimeOver = true;
                }
            }

            else //카운트업
            {
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            Debug.Log("TIMES: " + displayTime);
        }
    }
}
