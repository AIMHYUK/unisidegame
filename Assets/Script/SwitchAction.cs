using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool on = false; //스위치상태
    // Start is called before the first frame update
    void Start()
    {
        if(on)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;   
        } //이미지애니메이션
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(on)
            {
                on = false; //스위치 눌리면 이동블록 멈춤
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Stop();
            }
            else
            {
                on = true;
                GetComponent<SpriteRenderer>().sprite = imageOn;
                MovingBlock movBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movBlock.Move();
            }
        }
    }
}
