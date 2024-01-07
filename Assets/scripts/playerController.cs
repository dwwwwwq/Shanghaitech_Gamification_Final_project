using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationAmount = 90f;  // 旋转角度
    public float rotationSpeed = 5f;     // 旋转速度
    public float moveSpeed = 5f;         // 移动速度

    private bool isRotating = false;  // 是否正在旋转
    private bool isMoving = false;    // 是否正在移动
    private bool isHoldingSpace = false;  // 是否按住空格键
    private Rigidbody rb;
    private Coroutine moveCoroutine;
    private Wall currentDetectedWall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 不允许物理引擎旋转角色
        currentDetectedWall = null; // 添加这行来初始化 currentDetectedWall
    }

    void Update()
    {
        // 检测按下 A 键
        if (Input.GetKeyDown(KeyCode.A) && !isRotating && !isMoving)
        {
            StartCoroutine(RotateOverTime(transform.eulerAngles.y - rotationAmount));
            TileManager.Instance.RotateCurrentPlayerTileLeft();  // 向左旋转当前格子
        }

        // 检测按下 D 键
        if (Input.GetKeyDown(KeyCode.D) && !isRotating && !isMoving)
        {
            StartCoroutine(RotateOverTime(transform.eulerAngles.y + rotationAmount));
            TileManager.Instance.RotateCurrentPlayerTileRight();  // 向右旋转当前格子
        }

        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.forward);
        }

        // 检测按下 S 键
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.back);
        }

        // 检测按住空格键
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHoldingSpace = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingSpace = false;
            // Debug.Log("isHoldingSpace=" + isHoldingSpace);
        }

        int wallLayerMask = 1 << LayerMask.NameToLayer("Wall"); // 获取 "Wall" 图层的 LayerMask

        // 在这里进行射线检测
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 4f, wallLayerMask);
        Wall closestWall = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            Wall wall = hit.collider.GetComponent<Wall>();
            if (wall != null)
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestWall = wall;
                }
            }
        }

        if (closestWall != null)
        {
            if (isHoldingSpace)
            {
                closestWall.MarkAsMovable();
                currentDetectedWall = closestWall;
            }
            else if (currentDetectedWall != null)
            {
                currentDetectedWall.MarkAsUnmovable();
                currentDetectedWall = null;
            }
        }
        else if (currentDetectedWall != null)
        {
            currentDetectedWall.MarkAsUnmovable();
            currentDetectedWall = null;
        }

        // 在这里加入终止协程的逻辑
        if (!isHoldingSpace && moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // 调用处理不可移动墙的逻辑的方法
        // HandleUnmovableWall();
    }

    // 处理不可移动墙的逻辑的方法
    // private void HandleUnmovableWall()
    // {
    //     if (currentDetectedWall != null && !Physics.Raycast(transform.position, transform.forward, 4f))
    //     {
    //         currentDetectedWall.MarkAsUnmovable();
    //         currentDetectedWall = null;
    //         Debug.Log("MarkAsUnmovable");
    //     }
    // }

    // 逐渐过渡旋转的协程
    IEnumerator RotateOverTime(float targetAngle)
    {
        isRotating = true;
        isMoving = true;

        float startAngle = transform.eulerAngles.y;
        float endAngle = targetAngle;

        if (endAngle > 180f)
        {
            endAngle -= 360f;
        }

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            float currentAngle = Mathf.LerpAngle(startAngle, endAngle, elapsedTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, endAngle, 0);

        isRotating = false;
        isMoving = false;
    }

    void MovePlayer(Vector3 direction)
    {
        if (!isRotating)  // 只有在非旋转状态下才能移动
        {
            Vector3 movement = transform.TransformDirection(direction) * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
    }
}
