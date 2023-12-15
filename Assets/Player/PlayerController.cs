using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int score = 0; //점수
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    public float jump = 10.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;

    Animator animator;
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = " ";
    string oldAnime = " ";
    public static string gameState = "playing"; //게임상태
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        gameState = "playing"; //게임중
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState != "playing")
        { //처음에 확인해야 못움직이게 할 수 있음
            return;
        }
        axisH = Input.GetAxisRaw("Horizontal"); //방향키 어디 눌렸는지 우측이면 +0.1f
        if (axisH > 0.0f)
        {
            Debug.Log("오른쪽 이동");
            transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("왼쪽 이동");
            transform.localScale = new Vector2(-1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {

        if (gameState != "playing")
        { //처음에 확인해야 못움직이게 할 수 있음
            return;
        }
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }
        if (onGround && goJump)
        {
            Debug.Log("점프!");
            Vector2 jumpPw = new Vector2(0, jump);
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);
            goJump = false;
        }
        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            nowAnime = jumpAnime;
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }

        //점프
    }
    public void Jump()
    {
        goJump = true;
        Debug.Log("점프 버튼 눌림");
    }

    //접촉시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            score = item.value;
            Destroy(collision.gameObject); //아이템 제거

        }
    }
    public void Goal()
        {
            animator.Play(goalAnime);
            gameState = "gameclear";
            Debug.Log("클리어");
            GameStop();
        }
        public void GameOver()
        {
            animator.Play(deadAnime);
            gameState = "gameover";
            Debug.Log("게임오버");
            GetComponent<CapsuleCollider2D>().enabled = false; //플레이어 충돌판정 비활성
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //플레이어 위로 튀어오르게
            GameStop();
        }
        void GameStop()
        { //게임중지
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            rbody.velocity = new Vector2(0, 0); //속도를 0으로 해서 강제정지
        }
    }

