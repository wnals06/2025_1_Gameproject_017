using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public int cardValue;                     // 카드 값
    public Sprite cardImage;                  // 카드 이미지
    public TextMeshPro cardText;              // 카드 텍스트

    private Vector3 startPosition;            // 시작 위치 저장용
    private Transform startParent;            // 시작 부모 오브젝트 저장용
    private GameManager gameManager;          // GameManager 참조

    void Start()
    {
        startPosition = transform.position;
        startParent = transform.parent;
        gameManager = FindObjectOfType<GameManager>(); // 또는 외부에서 주입
    }

    public void InitCard(int value, Sprite image)
    {
        cardValue = value;
        cardImage = image;

        // 카드 이미지 설정
        GetComponent<SpriteRenderer>().sprite = image;

        // 카드 텍스트 설정
        if (cardText != null)
        {
            cardText.text = cardValue.ToString();
        }
    }

    public void ReturnToOriginalPosition()
    {
        transform.position = startPosition;
        transform.SetParent(startParent);

        if (gameManager != null)
        {
            if (startParent == gameManager.handArea)
            {
                gameManager.ArrangeHand();
            }
        }
    }
}
