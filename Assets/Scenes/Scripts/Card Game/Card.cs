using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public int cardValue;                     // ī�� ��
    public Sprite cardImage;                  // ī�� �̹���
    public TextMeshPro cardText;              // ī�� �ؽ�Ʈ

    private Vector3 startPosition;            // ���� ��ġ �����
    private Transform startParent;            // ���� �θ� ������Ʈ �����
    private GameManager gameManager;          // GameManager ����

    void Start()
    {
        startPosition = transform.position;
        startParent = transform.parent;
        gameManager = FindObjectOfType<GameManager>(); // �Ǵ� �ܺο��� ����
    }

    public void InitCard(int value, Sprite image)
    {
        cardValue = value;
        cardImage = image;

        // ī�� �̹��� ����
        GetComponent<SpriteRenderer>().sprite = image;

        // ī�� �ؽ�Ʈ ����
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
