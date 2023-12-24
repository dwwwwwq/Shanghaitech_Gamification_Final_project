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

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 碰到玩家，通知 TileManager
            TileManager.Instance.PlayerEnteredTile(this);
        }
    }
}