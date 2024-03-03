using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    MapGenerator map;

    private void Awake()
    {
        map = FindObjectOfType<MapGenerator>();
    }

    public void CheckTarget()
    {
        int num = 0;
        foreach (var target in map.targets)
        {
            if (map.boxes.ContainsKey(target))
                num++;
        }
        if (num == map.targets.Count)
        {
            Debug.Log("Win");

            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);

        // play animation
        // load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
