using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public string direction = "left";
    public float range = 0.0f;
    Vector3 defPos;

    // Start is called before the first frame update
    void Start()
    {
        if(direction == "right")
        {
            transform.localScale = new Vector2(-1, 1); //방향변경
        }
        defPos = transform.position; //시작위치
    }

    // Update is called once per frame
    void Update()
    {
        if(range > 0.0f)
        {
            if(transform.position.x < defPos.x - (range/2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1); //방향변경
            }
            if (transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1); //방향변경    
            }
        }
    }
    void FixedUpdate()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        if (direction == "right")
        {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
     {
        if(direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1); //방향변경
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1); //방향변경
        }       
    }
    //circle collier2D에 isTrigger=true로 했더니 떨어졌다(바닥이 없는거처럼). 끄면 안떨어지는데 뭐가 문제일까
}
