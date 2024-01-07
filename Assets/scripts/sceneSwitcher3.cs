namespace level3 {
    

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher1 : MonoBehaviour
{
    public string nextSceneName = "level1"; // 指定要切换到的下一个场景

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 当玩家进入触发区域时，触发场景切换事件
            SceneManager.LoadScene(nextSceneName); // 直接加载场景
            // EventManager.Instance.TriggerSceneSwitchCountEvent();
        }
    }
}
}