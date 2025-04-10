using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;            //이동 속도 변수 설정
    public float jumpForce = 5f;            //점프의 힘 값을 준다.

    public bool isGrounded = true;          //땅에 있는지 체크 하는 변수 (true/false)

    public int coinCount = 0;               //코인 획득 변수 선언
    public int totalcoins = 5;             // 총 코인 획득 필요변수 선언
    
    public Rigidbody rb;    //플레이어 강체를 선언
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //속도로 직접 이동
        rb.velocity = new Vector3(moveHorizontal * movespeed, rb.velocity.y, moveVertical * movespeed);
    
        //점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded)   //&& 두 값을 만족할때 ->(스페이스 버튼을 눌려을때와 isGrounded 가 true 일때)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //위쪽으로 설정한 힘만큼 강체에 준다.
            isGrounded = false;                                      //점프를 하는 순간 땅에서 떨어졌기 때문에 false라고 해준다.
        }
    }

    void OnCollisionEnter(Collision collision)       //충돌처리 함수
    {
        if (collision.gameObject.tag == "Ground")            //충돌이 일어난 물체의 tag가 ground일 경우
        {
            isGrounded = true;                               //땅과  충돌하면 true로 변경한다.
        }
    }


    private void OnTriggerEnter(Collider other)   //트리거 영역안에 들어왔나를 검사하는 함수
    {
        //코인 수집
        if(other.CompareTag("coin"))              // 코인 트리거와 충돌하면
        {
            coinCount++;                          //coinCount = coinCount + 1 코인변수 1을 올려준다.
            Destroy(other.gameObject);           // 코인 오브젝트를 지운다.

            Debug.Log($"코인 수집 : {coinCount}/{totalcoins}");
        }

        //목적지 도착 시 종료 로그 출력
        if(other.gameObject.tag == "Door"&& coinCount == totalcoins)   //모든 코인을 획득 후에 문으로 가면 게임 종료
        {
            Debug.Log("게임클리어");
            //게임 완료 로직 추가 가능
        }
    } 

    
}

