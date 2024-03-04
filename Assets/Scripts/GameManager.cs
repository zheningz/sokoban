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

    public void CheckWord()
    {
        foreach (var be in map.bes)
        {
            int x = be.Key.Item1;
            int y = be.Key.Item2;

            string word = "";
            List<char> charList = new List<char>();
            for (int i = x -1; i >= 0; i--)
            {
                if (!map.letters.ContainsKey((i, y)))
                    break;
                charList.Insert(0, map.letters[(i, y)].name);
            }
            word = new string(charList.ToArray());
            Debug.Log("The word along x-axis is "+ word);

            word = "";
            charList = new List<char>();
            for (int i = y + 1; ; i++)
            {
                if (!map.letters.ContainsKey((x, i)))
                    break;
                charList.Insert(0, map.letters[(x, i)].name);
            }
            word = new string(charList.ToArray());
            Debug.Log("The word along y-axis is " + word);
        }
    }

    public void CheckTarget()
    {
        int num = 0;
        foreach (var target in map.targets)
        {
            if (map.items.ContainsKey(target))
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
