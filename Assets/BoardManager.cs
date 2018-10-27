using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class BoardManager : MonoBehaviour
{
    public GameObject board;


    private List<GameObject> boards;


    // Use this for initialization
    void Start()
    {
        boards = new List<GameObject>();
        foreach (var b in FindObjectsOfType<Board>())
        {
            boards.Add(b.gameObject);
        }
    }

    public void Create()
    {
        int count = new Random().Next(3, 5);
        float yPlus = 0;
        for (int i = 0; i < count; i++)
        {
            float x = new Random(Guid.NewGuid().GetHashCode()).Next(50, 351);
            float y = new Random(Guid.NewGuid().GetHashCode()).Next(100, 200);
            yPlus += y;
            GameObject newBoard = Instantiate(board, transform);
            newBoard.transform.localPosition = new Vector3(x, -yPlus);
            newBoard.transform.localScale = Vector3.one;
            boards.Add(newBoard);
        }
    }

    public void DestroyBoard(GameObject b)
    {
        boards.Remove(b);
        Destroy(b);
    }

    public void Move(GameObject landBoard)
    {
        float minValue = landBoard.GetComponent<RectTransform>().anchoredPosition.y;

        float bottomY = GameObject.Find("bottom").GetComponent<RectTransform>().anchoredPosition.y;

        float moveDistance = minValue - bottomY;

        for (int i = 0; i < boards.Count; i++)
        {
            RectTransform rt = boards[i].GetComponent<RectTransform>();
            rt.DOAnchorPosY(rt.anchoredPosition.y - Mathf.Abs(moveDistance), .5f);
        }

        if (Mathf.Abs((int) moveDistance) == 0)
        {
            return;
        }

//        Create();
    }
}