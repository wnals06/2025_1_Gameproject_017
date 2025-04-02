using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawaner : MonoBehaviour
{
    public GameObject coinPrefabs;                     //���� ������
    public GameObject misslePrefabs;                   //�̻��� ������

    [Header("���� Ÿ�̹� ����")]
    public float minSpawninterval = 0.5f;               //�ּ� ���� ���� (��)
    public float maxSpawnInterval = 2.0f;               //�ִ� ���� ���� (��)

    [Header("���� ���� Ȯ�� ����")]
    [Range(0, 100)]                                      //����Ƽ ui���� �� �� �ְ� �Ѵ�.
    public int coinSpawnChange = 50;                      //������ ������ Ȯ�� (0~100)
     
    public float timer = 0.0f;
    public float nextSpawnTime;                           //���� ���� �ð�

    //Start is called before the first frame update
    void Start()
    {
        SetNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;          //�ð��� 0���� ���� �����Ѵ�.

        //���� �ð��� �Ǹ� ������Ʈ ����
        if(timer >= nextSpawnTime)
        {
            SpawnObject();                  //�Լ��� ȣ�� ���ش�.
            timer = 0.0f;                  //�ð��� �ʱ�ȭ �����ش�.
            SetNextSpawnTime();               //�ٽ� �Լ��� ����
        }
    }
    void SpawnObject()
    {
        Transform spawnTransform = transform;              //������ ������Ʈ�� ��ġ�� ȸ�� ���� �����´�.

        //Ȯ���� ���� ���� �Ǵ� �̻��� ����
        int randomvalue = Random.Range(0, 100);                //0~100�� �������� �̾Ƴ���.
        if (randomvalue < coinSpawnChange)
        {
            Instantiate(coinPrefabs, spawnTransform.position, spawnTransform.rotation);         //���� �������� �ش���ġ�� �����Ѵ�.
        }
     else
        {
            Instantiate(misslePrefabs, spawnTransform.position, spawnTransform.rotation);     //�̻��� �������� �ش���ġ�� �����Ѵ�.
        }
    }
    void SetNextSpawnTime()
    {
        //�ּ�-�ִ� ������ ������ �ð� ����
        nextSpawnTime = Random.Range(minSpawninterval, maxSpawnInterval);
    }
}
