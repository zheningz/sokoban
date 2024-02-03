using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Manual setting")]
    public int targetNum;

    [Header("Auto detection")]
    public int finishedNum = 0;

    public void CheckGoal()
    {
        // 框架仅识别目标数量与箱子数量是否一致
        if (targetNum == finishedNum)
        {
            Debug.Log("Level finished");
            
            // 加载下一关
            // StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
