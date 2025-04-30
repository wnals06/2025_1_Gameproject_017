using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public int cardValue;                     //ī�尪 (ī��ܰ�)
    public Sprite cardImage;                 //ī�� �̹���
    public TextMeshPro cardText;             //ī�� �ؽ�Ʈ
                                             //

    
    public void lniCard(int value, Sprite image)                      //ī�� ���� �ʱ�ȭ �Լ�
    {
        cardValue = value;
        cardImage = image;

        //ī�� �̹��� ����
        GetComponent<SpriteRenderer>().sprite = image;                  //�ش� �̹����� ī�忡 ǥ���Ѵ�.

        //ī�� �ؽ�Ʈ ���� (�ִ� ���)
        if (cardText != null)
        {
            cardText.text = cardValue.ToString();                       //ī�尪�� ǥ���Ѵ�.
        }
    }
}
