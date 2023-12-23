using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab1;  // 在 Unity 编辑器中关联 Cube 预制体
    public GameObject tilePrefab2;

    void Start()
    {
        // 初始化格子
        Tile tile1 = tilePrefab1.GetComponent<Tile>();

        Tile tile2 = tilePrefab2.GetComponent<Tile>();
        

        
        // 测试旋转
        tile1.RotateTile(45f);
        tile2.RotateTile(60f);
    }
}
