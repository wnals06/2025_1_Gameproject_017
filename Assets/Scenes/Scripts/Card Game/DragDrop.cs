using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public bool isDragging = false;
    public Vector3 StartPosition;
    public Transform StartParent;

    private GameManager gameManager;

    void Start()
    {
        StartPosition = transform.position;
        StartParent = transform.parent;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        StartPosition = transform.position;
        StartParent = transform.parent;
        GetComponent<SpriteRenderer>().sortingOrder = 10;
    }

    void OnMouseUp()
    {
        isDragging = false;
        GetComponent<SpriteRenderer>().sortingOrder = 1;

        if (gameManager == null)
        {
            ReturnToOriginalPosition();
            return;
        }

        if (IsInMergeArea())
        {
            gameManager.MoveCardToMerge(gameObject);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    void ReturnToOriginalPosition()
    {
        transform.position = StartPosition;
        transform.SetParent(StartParent);
    }

    bool IsInMergeArea()
    {
        float distance = Vector3.Distance(transform.position, gameManager.mergeArea.position);
        return distance < 2.5f;
    }
}
