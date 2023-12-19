using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab; //발사되는 포탄
    public float delayTime = 3.0f;
    public float fireSpeedX = -4.0f; //발사벡터
    public float fireSpeedY = 0.0f;
    public float length = 8.0f;

    GameObject player;
    GameObject gateObj;
    float passedTimes = 0; //경과시간

    // Start is called before the first frame update
    void Start()
    {
        //Transform tr = transform.Find("gate");
        //gateObj = tr.gameObject; 이거는 왜 안되는지 모르겠음. 결국 tag를 만들어서 해결..
        gateObj = GameObject.FindGameObjectWithTag("gate");
        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 찾기
    }

    // Update is called once per frame
    void Update()
    {
        passedTimes += Time.deltaTime;
        float d = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(d);
        if(CheckLength(player.transform.position)) 
        {
            if(passedTimes > delayTime)
            {
                passedTimes = 0; //발사
                Vector3 pos = new Vector3(gateObj.transform.position.x, gateObj.transform.position.y, transform.position.z);//발사위치
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity); //프리팹으로 옵젝 만드는 메서드
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>(); //발사방향
                Vector2 v = new Vector2(fireSpeedX, fireSpeedY);
                rbody.AddForce(v, ForceMode2D.Impulse);


            }
        }
    }
    bool CheckLength(Vector2 targetPos) //포대와 플레이어와의 거리가 length보다 작으면 true
    {
        bool ret = false;
        float d = Vector2.Distance(gateObj.transform.position, targetPos);
        Debug.Log(d);
        if(length >= d)
        {
            ret = true;
        }
        return ret;
    }
}
