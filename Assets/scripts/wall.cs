using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 墙的属性
    public bool isRotatable;  // 是否可旋转
    public float rotationAngle;  // 旋转角度
    private Tile attachedTile;  // 指向墙所在的格子的引用

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