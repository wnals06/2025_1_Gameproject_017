using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;            //�̵� �ӵ� ���� ����
    public float jumpForce = 5f;            //������ �� ���� �ش�.

    public bool isGrounded = true;          //���� �ִ��� üũ �ϴ� ���� (true/false)

    public int coinCount = 0;               //���� ȹ�� ���� ����
    public int totalcoins = 5;             // �� ���� ȹ�� �ʿ亯�� ����
    
    public Rigidbody rb;    //�÷��̾� ��ü�� ����
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������ �Է�
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�ӵ��� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * movespeed, rb.velocity.y, moveVertical * movespeed);
    
        //���� �Է�
        if (Input.GetButtonDown("Jump") && isGrounded)   //&& �� ���� �����Ҷ� ->(�����̽� ��ư�� ���������� isGrounded �� true �϶�)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  //�������� ������ ����ŭ ��ü�� �ش�.
            isGrounded = false;                                      //������ �ϴ� ���� ������ �������� ������ false��� ���ش�.
        }
    }

    void OnCollisionEnter(Collision collision)       //�浹ó�� �Լ�
    {
        if (collision.gameObject.tag == "Ground")            //�浹�� �Ͼ ��ü�� tag�� ground�� ���
        {
            isGrounded = true;                               //����  �浹�ϸ� true�� �����Ѵ�.
        }
    }


    private void OnTriggerEnter(Collider other)   //Ʈ���� �����ȿ� ���Գ��� �˻��ϴ� �Լ�
    {
        //���� ����
        if(other.CompareTag("coin"))              // ���� Ʈ���ſ� �浹�ϸ�
        {
            coinCount++;                          //coinCount = coinCount + 1 ���κ��� 1�� �÷��ش�.
            Destroy(other.gameObject);           // ���� ������Ʈ�� �����.

            Debug.Log($"���� ���� : {coinCount}/{totalcoins}");
        }

        //������ ���� �� ���� �α� ���
        if(other.gameObject.tag == "Door"&& coinCount == totalcoins)   //��� ������ ȹ�� �Ŀ� ������ ���� ���� ����
        {
            Debug.Log("����Ŭ����");
            //���� �Ϸ� ���� �߰� ����
        }
    } 

    
}

