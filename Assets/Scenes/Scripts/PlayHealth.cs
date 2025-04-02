using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHelth : MonoBehaviour
{

    public int maxLives = 3;                //최대 생명력
    public int currentLives;                //현재 생명력

    public float invincibleTime = 1.0f;         //피격 후 무적시간(반복 피격 방지)
    public bool islnvincible = false;           //무적 여부의 값

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;                     //생명력 초기화
    }

    private void OnTriggerEnter(Collider other)              //트리거 영역 안에 들어왔나를 검사하는 함수
    { 
        //코인 수집
        if (other .CompareTag("missile"))                   //미사일과 충돌하면
        {
            currentLives--;
            Destroy(other.gameObject);                 //미사일 오브젝트를 없앤다.
            
            if(currentLives <= 0)                   //지금 체력이 0이하일 경우
            {
                GameOver();
            }
        }
    }

    void GameOver()          //게임 오버 처리
    {

        gameObject.SetActive(false);           //플레이어 비활성화
        Invoke("RestartGame", 3.0f);           //3초후 현재 씬 재시작
    }
}
