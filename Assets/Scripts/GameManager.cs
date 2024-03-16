using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    MapGenerator map;
    WordBook wordBook;

    private void Awake()
    {
        map = FindObjectOfType<MapGenerator>();

        wordBook = new WordBook();
        wordBook.Load();

        // test search function
        wordBook.SearchWords("ap");
    }

    public void CheckWord()
    {
        string word;
        foreach (var be in map.bes)
        {
            int x = be.Key.Item1;
            int y = be.Key.Item2;

            // check along x-axis
            List<char> charList = new List<char>();
            int i = x - 1;
            for ( ; i >= 0; i--)
            {
                if (!map.letters.ContainsKey((i, y)))
                    break;
                charList.Insert(0, map.letters[(i, y)].name);
            }
            word = new string(charList.ToArray());
            // check if the word is in the wordbook
            foreach (var key in wordBook.words.Keys)
            {
                if (word.Equals(key))
                {
                    Debug.Log("The word along x-axis is " + word);

                    // remove letters
                    while (i < x - 1)
                    {
                        i++;
                        Destroy(map.letters[(i, y)].obj);
                        map.letters.Remove((i, y));
                    }

                    // instantiate the item
                    map.InstantiateItem(x + 1, y, key);
                    return;
                }
            }

            // check along y-axis
            charList = new List<char>();
            int j = y + 1;
            for ( ; ; j++)
            {
                if (!map.letters.ContainsKey((x, j)))
                    break;
                charList.Insert(0, map.letters[(x, j)].name);
            }
            word = new string(charList.ToArray());
            // check if the word is in the wordbook
            foreach (var key in wordBook.words.Keys)
            {
                if (word.Equals(key))
                {
                    Debug.Log("The word along y-axis is " + word);

                    // remove letters
                    while (j > y + 1)
                    {
                        j--;
                        Destroy(map.letters[(x, j)].obj);
                        map.letters.Remove((x, j));
                    }

                    // instantiate the item
                    map.InstantiateItem(x, y - 1, key);
                    return;
                }
            }
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
            Debug.Log("Level clear");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
