using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float moveDistance = 4f; // 墙移动的距离
    public float moveSpeed = 5f;    // 墙移动的速度

    private bool isMoving = false;  // 是否正在移动
    private bool isHoldingSpace = false;  // 是否按住空格键
    private Transform player;       // 玩家的 Transform

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // 检测按住空格键
    if (Input.GetKey(KeyCode.Space))
        {
            isHoldingSpace = true;
            StartCoroutine(MoveWall());
        }
        else
        {
            isHoldingSpace = false;
        }
    }

    IEnumerator MoveWall()
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.position + player.forward * moveDistance;

        float elapsedTime = 0f;

        while (elapsedTime < 1f && isHoldingSpace)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = endPosition;

        isMoving = false;
    }
}
