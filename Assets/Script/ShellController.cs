using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float deleteTime = 3.0f; //제거할 시간 설정
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime); //제거 설정
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //무슨 오브젝트에 닿으면 제거
    }
}
