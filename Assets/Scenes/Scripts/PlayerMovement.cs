using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("기본 이동 설정")]
    public float movespeed = 5f;            //이동 속도 변수 설정
    public float jumpForce = 5f;            //점프의 힘 값을 준다.
    public float turnSpeed = 10f;           //회전속도

    [Header("점프 개선 설정")] 
    public float fallMultiplier = 2.5f;            //하강 중력 배율
    public float lowJumpMultiplier = 2.0f;         //짧은 점프 배율

    [Header("지면 감지 설정")]
    public float coyoteTime = 0.15f;           //지면 관성 시간
    public float coyoteTimeCounter;            //관성 타이머
    public bool realGrounded = true;           //실제 지면 상태

    [Header("글라이더 설정")]
    public GameObject gliderObject;                  //글라이더 오브젝트
    public float gliderFallSpeed = 1.0f;             //글라이더 낙하속도
    public float gliderMoveSpeed = 7.0f;            //글라이더 이동속도
    public float gliderMaxTime = 5.0f;              //최대 사용 시간
    public float gliderTimeLeft;                     //남은 사용 시간
    public bool isGliding = false;                  //글라이딩 중인지 여부
     

    public bool isGrounded = true;          //땅에 있는지 체크 하는 변수 (true/false)

    public int coinCount = 0;               //코인 획득 변수 선언
    public int totalcoins = 5;             // 총 코인 획득 필요변수 선언
    
    public Rigidbody rb;    //플레이어 강체를 선언
   
    // Start is called before the first frame update
    void Start()
    {




        //글라이더 오브젝트 초기화
        if (gliderObject != null)
        {
            gliderObject.SetActive(false);                         //시작시 비활성화
        }

        gliderTimeLeft = gliderMaxTime;                         //글라이더 시간 초기화

        coyoteTimeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //지면 감지 안정화
        updateGroundedState();

        //움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //이동 방향 벡터
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);     //이동 방향 감지

        
        if (movement.magnitude > 0.1f)      //입력이 있을 때만 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);      //이동 방향을 바라보도록 자연스럽게 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * turnSpeed * Time.deltaTime);
        }

        //G키로 글라이더 제어 (누르는 동안만 비활성화)
        if (Input.GetKey(KeyCode.G) && !isGrounded && gliderTimeLeft > 0)         //G키를 누르면서 땅에 있지 않고 글라이더 남은 시간이 있을 때(3가지 조건)
        {
            if(!isGliding)                                  //글라이더 활성화 (누르고 있는 동안)
            {
                //글라이더 활성화 함수 (아래정의)
            }

            //글라이더 사용시간 감소
            gliderTimeLeft -= Time.deltaTime;

            //글라이더 시간이 다 되면 비활성화
            if(gliderTimeLeft <= 0)
            {
                //글라이더 비활성화 함수 (아래정의)
            }
        }
        else if (isGliding)
        {
            //G키를 때면 글라이더 비활성화
        }

        if(isGliding)     //움직임 처리
        {
            //글라이더 사용 중 이동
            ApplyGliderMovement(moveHorizontal, moveVertical);
        }
        else //(기존 움직임 코드들을 else 문안에 넣는다.)
        {
            //속도로 직접이동
            rb.velocity = new Vector3(moveHorizontal * movespeed, rb.velocity.y, moveVertical * movespeed);

            // 착지 점프 구현
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;       //하강시 중력강화
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))      //상승 중 점프 버튼을 때면 낮게 점프
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }





        //속도로 직접 이동
        rb.velocity = new Vector3(moveHorizontal * movespeed, rb.velocity.y, moveVertical * movespeed);

        //착시 점프 높이 구현
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;  //하강 시 중력 강화
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))        //상승 중 점프 버튼을 떼면 낮게 점프
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    
        //점프 입력
        if (Input.GetButtonDown("Jump") && isGrounded)   //&& 두 값을 만족할때 ->(스페이스 버튼을 눌려을때와 isGrounded 가 true 일때)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //위쪽으로 설정한 힘만큼 강체에 준다.
            isGrounded = false;                                      //점프를 하는 순간 땅에서 떨어졌기 때문에 false라고 해준다.
            realGrounded = false;
            coyoteTimeCounter = 0;                                   //코요테 타임 즉시 리셋
        }

        //지면에 있으면 글라이더 시간 회복 및 글라이더 비활성화
        if(isGrounded)
        {
            if(isGliding)
            {
                DisableGlider();
            }

            //지상에 있을 때 시간 회복
            gliderTimeLeft = gliderMaxTime;
        }
    }

    //글라이더 활성화 함수

    void EnableGlider()
    {
        isGliding = true;

        //글라이더 오브젝트 표시
        if (gliderObject != null)
        {
            gliderObject.SetActive(true);
        }

        //하강 속도 초기화
        rb.velocity = new Vector3(rb.velocity.x, gliderFallSpeed, rb.velocity.z);
    }

    //글라이더 비활성화 함수
    void DisableGlider()
    {
        isGliding = false;

        //글라이더 오브젝트 숨기기
        if (gliderObject != null)
        {
            gliderObject.SetActive(false);
        }

        //즉시 낙하 하도록 중력 적용
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    //글라이더 이동 적용

    void ApplyGliderMovement(float horizontal, float vertical)                 //수평과 수직을  함수의 인수로 받는다.
    {
        //글라이더 효과 : 천천히 떨어지고 수평 방향으로 더 빠르게 이동

        Vector3 gliderVelocity = new Vector3(
           horizontal * gliderMoveSpeed,           //X축
          gliderFallSpeed,                         //Y축 (일정한 속도로 천천히 하강)
          vertical * gliderMoveSpeed               //Z축
            );
    }

    void OnCollisionEnter(Collision collision)       //충돌처리 함수(변경)
    {
        if (collision.gameObject.tag == "Ground")            //충돌이 일어난 물체의 tag가 ground일 경우
        {
            isGrounded = true;                               //땅과  충돌하면 true로 변경한다.
        }
    }



    void OnCollisionStay(Collision collision)                //지면과의 충돌이 유지되는지 확인(추가)
    {
        if (collision.gameObject.CompareTag("Ground"))               //충돌이 유지되는 물체의 Tag가 Ground일 경우
        {
            realGrounded = true;                                     //충돌이 유지되기 때문에 true
        }
    }

    private void OnCollisionExit(Collision collision)              //지면에서 떨어졌는지 확인(추가)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            realGrounded = false;                                 //지면에서 떨어졌기 때문에 false
        }
    }

    void OnTriggerEnter(Collider other)   //지면과 충돌이 유지되는지 확인 (추가)
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

    //지면 상태 업데이트 함수
    void updateGroundedState()
    {
        if (realGrounded)          //실제 지면에 있으면 코요테 타임 리셋
        {
            coyoteTimeCounter = coyoteTime;
            isGrounded = true;
        }
        else
        {
            //실제로는 지면에 없지만 코요테 타임 내에 있으면 여전히 지면으로 판단
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;             //시간을 지속적으로 감소시킨다.
                isGrounded = true;
            }
            else
            {
                isGrounded = false;                       //타임이 끝나면 FALSE
            }
        }
    }
    
    

}

