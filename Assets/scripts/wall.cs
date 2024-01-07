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
        // Debug.Log("IsMovable="+isMovable);
    }

    IEnumerator MoveWall(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + direction * moveSpeed;

        float distance = Vector3.Distance(startPosition, endPosition);

        float elapsedTime = 0f;

        int wallAndBoundaryLayerMask = (1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.NameToLayer("Boundary"));

// 在当前位置到终点之间进行射线检测
RaycastHit hit;
if (Physics.Raycast(startPosition, direction, out hit, distance, wallAndBoundaryLayerMask))
{
    // 如果与 "Wall" 或 "Boundary" 层的碰撞体发生了碰撞，将终点设置在碰撞点的前一点
    endPosition = hit.point - direction.normalized * 0.08f; // 将位置调整为碰撞点的前一点，防止贴合
}

// 进行平滑移动

while (elapsedTime < 1f)
{
    transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime);
    elapsedTime += Time.deltaTime * moveSpeed / distance;  // 根据距离进行调整
    yield return null;
}

transform.position = endPosition;
    }

    private void CheckBoundary()
    {
        // 获取边界的碰撞体
        Collider boundaryCollider = GameObject.FindWithTag("Boundary").GetComponent<Collider>();

        // 如果边界存在
        if (boundaryCollider != null)
        {
            // 检测是否碰到了边界
            if (!boundaryCollider.bounds.Contains(transform.position))
            {
                // 如果碰到了边界，执行相应的处理，比如标记为不可移动
                MarkAsUnmovable();
            }
        }
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

    CheckBoundary();
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