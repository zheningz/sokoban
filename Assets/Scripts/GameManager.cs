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
        // ��ܽ�ʶ��Ŀ�����������������Ƿ�һ��
        if (targetNum == finishedNum)
        {
            Debug.Log("Level finished");
            
            // ������һ��
            // StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
