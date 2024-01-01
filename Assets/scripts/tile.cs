using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 position;
    public bool isRotatable;
    public float rotationAngle;

    public float rotationSpeed = 5f; // 旋转速度

    private Quaternion targetRotation;
    private List<Wall> wallsOnTile = new List<Wall>();  // 保存格子上的墙的列表


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 碰到玩家，通知 TileManager
            TileManager.Instance.PlayerEnteredTile(this);
        }
        
        if (other.CompareTag("Wall"))
        {
            // 碰到墙，保存引用到列表
            Wall wall = other.GetComponent<Wall>();
            if (wall != null)
            {
                wallsOnTile.Add(wall);
                wall.SetAttachedTile(this);  // 设置墙所在的格子
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            // 离开墙，从列表中移除引用
            Wall wall = other.GetComponent<Wall>();
            if (wall != null)
            {
                wallsOnTile.Remove(wall);
            }
        }
    }

    public void RotateTileLeft()
    {
        // 向左旋转90度
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90f, 0));
        StartCoroutine(RotateOverTime());
    }

    public void RotateTileRight()
    {
        // 向右旋转90度
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90f, 0));
        StartCoroutine(RotateOverTime());
    }

    private IEnumerator RotateOverTime()
{
    float elapsedTime = 0f;
    Quaternion startRotation = transform.rotation;

    int specialLayer = LayerMask.NameToLayer("SpecialLayer");

    foreach (Wall wall in wallsOnTile)
    {
        if (wall.IsOnTile(this))
            {
                wall.DisableWallCollision();
                wall.transform.parent = transform;
            }
        
    }

    while (elapsedTime < 1f)
    {
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);
        elapsedTime += Time.deltaTime * rotationSpeed;
        yield return null;
    }

    transform.rotation = targetRotation;

    // 旋转完成后再设置和解除关系
    

    
    foreach (Wall wall in wallsOnTile)
    {
        wall.transform.parent = null;
        wall.EnableWallCollision();
        wall.gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         // 碰到玩家，通知 TileManager
    //         TileManager.Instance.PlayerEnteredTile(this);
    //     }
    // }

    
}