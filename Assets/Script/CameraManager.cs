using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f; //스크롤 제한

    public GameObject subScreen;

    public bool isForceScrollX = false; //x축 강제 스크롤 플래그
    public float forceScrollSpeedX = 0.5f; //1초간 움직일 x의 거리
    public bool isForceScrolly = false; //y축 강제 스크롤
    public float forceScrollSpeedY = 0.5f; //1초간 움직일 y의 거리

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //플레이어 찾기
        if (player != null)
        {
            //카메라 좌표 갱신
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;
            if(isForceScrollX){
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime); //가로 강제스크롤
            }
            if(isForceScrolly){
                x = transform.position.y + (forceScrollSpeedY * Time.deltaTime); //가로 강제스크롤
            }
            //양끝에 이동 제한

            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }

            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3; //카메라위치에 벡터3 만들기

            if (subScreen != null) //서브스크린 스크롤
            {
                y = subScreen.transform.position.y;
                x = subScreen.transform.position.x;
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            } //subscreen은 카메라 이동량의 절반만큼 옆으로 움짐임
        }
    }
}
