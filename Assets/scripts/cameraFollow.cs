using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 玩家的 Transform
    public float distanceBehindPlayer = 5f;  // 摄像机跟随的距离
    public float heightAbovePlayer = 2f;  // 摄像机距离玩家上方的高度

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference not set in CameraFollow script.");
            return;
        }

        // 获取玩家的位置
        Vector3 playerPosition = player.position;

        // 计算摄像机的位置
        Vector3 targetPosition = playerPosition - player.forward * distanceBehindPlayer + Vector3.up * heightAbovePlayer;

        // 设置摄像机的位置
        transform.position = targetPosition;

        // 使摄像机始终朝向玩家
        transform.LookAt(playerPosition);
    }
}