using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float length = 3.1f; //자동낙하 탐지거리 - 유니티에서 바꾸자
    public bool isDelete = false; //낙하후 제거 여부 - 유니티에서 바꾸자
    bool isFell = false; //낙하 플래그
    float fadeTime = 0.5f; //페이드 아웃 시간
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //플레이어 찾기
        if(player != null)
        {
            float d = Vector2.Distance(transform.position, player.transform.position); //플레이어와의 거리 계산
            if(length >= d) //교재오류
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if(rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; //rigidbody2D 물리 시뮬레이션 , 왜 객체를 계속 만듥까?
                }
            }
        }
        if(isFell)
        {
            //낙하
            fadeTime -= Time.deltaTime; // 이전 프레임과의 차이만큼 시간 차감
            Color col = GetComponent<SpriteRenderer>().color; //컬러값 가져오기
            col.a = fadeTime; //투명도 변경
            GetComponent<SpriteRenderer>().color = col; //컬러값 재설정
            if(fadeTime <= 0.0f)
            {
                Destroy(gameObject); //0보다 작으면 제거(투명)
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision) { //is trigger가 체크되지 않았을때 무언가랑 접촉하면 호출
        if(isDelete)
        {
            isFell = true;  //낙하 플래그 true
        }
        
    }
}
