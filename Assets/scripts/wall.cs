using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 墙的属性
    public bool isRotatable;  // 是否可旋转
    public float rotationAngle;  // 旋转角度
    private Tile attachedTile;  // 指向墙所在的格子的引用
    private bool isMovable=false;
    private Coroutine moveCoroutine;
    private float moveSpeed = 4f;         // 移动速度


    // 设置所在的格子
    public void SetAttachedTile(Tile tile)
    {
        attachedTile = tile;
    }

    // 检查墙是否在特定的格子上
    public bool IsOnTile(Tile tileToCheck)
    {
        return attachedTile == tileToCheck;
    } 

    public void DisableWallCollision()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void EnableWallCollision()
    {
        GetComponent<Collider>().enabled = true;
    }

    public void MarkAsMovable()
    {
        isMovable=true;
        // Debug.Log(isMovable);
    }

    public void MarkAsUnmovable()
    {
        isMovable=false;
        Debug.Log("IsMovable="+isMovable);
    }

    IEnumerator MoveWall(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction * moveSpeed;

        float distance = Vector3.Distance(startPosition, endPosition);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed / distance;  // 根据距离进行调整

            yield return null;
        }

        transform.position = endPosition;
    }



    void Update()
{
    if (Input.GetKeyDown(KeyCode.W) && isMovable)
    {
        float cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;
        Vector3 moveDirection = Quaternion.Euler(0, cameraRotationY, 0) * Vector3.forward;
        moveCoroutine = StartCoroutine(MoveWall(moveDirection));
    }
    else if (Input.GetKeyUp(KeyCode.W) || !isMovable)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }

    if (Input.GetKeyDown(KeyCode.S) && isMovable)
    {
        float cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;
        Vector3 moveDirection = Quaternion.Euler(0, cameraRotationY, 0) * Vector3.back;
        moveCoroutine = StartCoroutine(MoveWall(moveDirection));
    }
    else if (Input.GetKeyUp(KeyCode.S) || !isMovable)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
}




    // 在墙被旋转时调用的方法
    // public void RotateWithTileLeft(Vector3 tileRotationCenter)
    // {
    //     // 获取墙相对于格子旋转中心的位置
    //     Vector3 relativePosition = transform.position - tileRotationCenter;

    //     // 围绕格子旋转中心旋转墙
    //     transform.RotateAround(tileRotationCenter, Vector3.up, -90f);

    //     // 将位置设置回原来的相对位置
    //     transform.position = tileRotationCenter + Quaternion.Euler(0, -90f, 0) * relativePosition;
    // }

    // public void RotateWithTileRight(Vector3 tileRotationCenter)
    // {
    //     // 获取墙相对于格子旋转中心的位置
    //     Vector3 relativePosition = transform.position - tileRotationCenter;

    //     // 围绕格子旋转中心旋转墙
    //     transform.RotateAround(tileRotationCenter, Vector3.up, 90f);

    //     // 将位置设置回原来的相对位置
    //     transform.position = tileRotationCenter + Quaternion.Euler(0, 90f, 0) * relativePosition;
    // }
}