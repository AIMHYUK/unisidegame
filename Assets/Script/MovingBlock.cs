using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //x이동거리
    public float moveY = 0.0f; //x이동거리
    public float times = 0.0f; //시간
    public float weight = 0.0f; //정지시간
    public bool isMoveWhenOn = false; //올라갔을때 움직이기

    public bool isCanMove = true; //움직임
    float perDX;
    float perDY; //1프레임당 x,y이동값
    Vector3 defPos; //초기위치
    bool isReverse = false; //반전여부

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime; //FixedUpdate가 호출될때까지의 간격(여기에 문제있음)
        Debug.Log("시간은" + timestep);
        perDX = moveX / (1.0f / timestep * times);
        perDY = moveY / (1.0f / timestep * times);
        Debug.Log("x이동거리은" + perDX);
        Debug.Log("y이동거리은" + perDY);
        //1프레임의 x,y 이동값

        if(isMoveWhenOn)
        {
            isCanMove = false; //밟고 올라가면 움직임
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate() 
    {
        if(isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if(isReverse)
            {
                //반대방향 이동
                //이동량이 양수 && 이동위치가 초기위치보다 작음 OR 이동량이 음수 && 이동위치가 초기위치보다 큼
                if((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    endX = true; //x방향 이동종료
                }
                if((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true; //y방향 이동종료
                }
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z)); //블록이동

            }
            else
            {
                //정방향 이동
                //이동량이 양수 && 이동위치가 초기위치보다 큼 OR 이동량이 음수 && 이동위치가 초기위치보다 작음
                if((perDX >= 0.0f && x >= defPos.x) || (perDX < 0.0f && x <= defPos.x))
                {
                    endX = true; //x방향 이동종료
                }
                if((perDY >= 0.0f && y >= defPos.y) || (perDY < 0.0f && y <= defPos.y))
                {
                    endY = true; //y방향 이동종료
                }
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v); //블록이동  
            }
            if(endX && endY) //이동종료
            {
                if(isReverse)
                {
                    //정면 방향이동으로 돌아가기전 초기위치로 돌리기(위치 어긋남 방지)
                    transform.position = defPos;
                }
                isReverse = !isReverse;
                isCanMove = false;
                if(isMoveWhenOn == false) //올라갔을때 움직이는 값 꺼지면
                {
                    Invoke("Move", weight); //weight만큼 지연 후 이동
                }
            }
            
        }
    }

    public void Move()
    {
        isCanMove = true;
    }
    public void Stop()
    {
        isCanMove = false;
    }

    //접촉시작
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform); //접촉한게 플레이어면 이동블록의 자식으로
            //자식으로 해야하는 이유 => 플레이어가 물리 시뮬레이션 적용되어있어서 자식으로 안하면 게임 오브젝트가 미끄러져 떨어짐
            //블록의 자식이어야 같이 움직임
            if(isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null); //접촉한게 플레이어면 이동블록의 자식에서 제외
        }
        
    }
}
