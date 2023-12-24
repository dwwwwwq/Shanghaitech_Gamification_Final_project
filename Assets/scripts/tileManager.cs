using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    
    private Tile currentPlayerTile;
    public void PlayerEnteredTile(Tile tile)
    {
        currentPlayerTile = tile;
        Debug.Log(currentPlayerTile);
    }

    public void RotateCurrentPlayerTileLeft()
    {
        if (currentPlayerTile != null)
        {
            // 向左旋转格子
            currentPlayerTile.RotateTileLeft();

            // currentPlayerTile.NotifyWallsToRotateLeft();
        }
    }

    public void RotateCurrentPlayerTileRight()
    {
        if (currentPlayerTile != null)
        {
            // 向右旋转格子
            currentPlayerTile.RotateTileRight();

            // currentPlayerTile.NotifyWallsToRotateRight();
        }
    }
}
