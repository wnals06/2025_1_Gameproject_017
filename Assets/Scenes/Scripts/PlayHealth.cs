using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHelth : MonoBehaviour
{

    public int maxLives = 3;                //�ִ� �����
    public int currentLives;                //���� �����

    public float invincibleTime = 1.0f;         //�ǰ� �� �����ð�(�ݺ� �ǰ� ����)
    public bool islnvincible = false;           //���� ������ ��

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives;                     //����� �ʱ�ȭ
    }

    private void OnTriggerEnter(Collider other)              //Ʈ���� ���� �ȿ� ���Գ��� �˻��ϴ� �Լ�
    { 
        //���� ����
        if (other .CompareTag("missile"))                   //�̻��ϰ� �浹�ϸ�
        {
            currentLives--;
            Destroy(other.gameObject);                 //�̻��� ������Ʈ�� ���ش�.
            
            if(currentLives <= 0)                   //���� ü���� 0������ ���
            {
                GameOver();
            }
        }
    }

    void GameOver()          //���� ���� ó��
    {

        gameObject.SetActive(false);           //�÷��̾� ��Ȱ��ȭ
        Invoke("RestartGame", 3.0f);           //3���� ���� �� �����
    }
}
