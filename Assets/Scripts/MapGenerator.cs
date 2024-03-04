using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    string[] map;

    public string level = "1-1";

    public GameObject player;
    public GameObject wall;
    public GameObject item;
    public GameObject target;
    public GameObject isbox;
    public GameObject[] letter;

    // keep track of objects
    public HashSet<(int, int)> walls;
    public List<(int, int)> targets;
    public Dictionary<(int, int), Letter> letters;
    public Dictionary<(int, int), Be> bes;
    public Dictionary<(int, int), Item> items;

    private void Awake()
    {
        letters = new Dictionary<(int, int), Letter>();
        bes = new Dictionary<(int, int), Be>();
        items = new Dictionary<(int, int), Item>();
        walls = new HashSet<(int, int)>();
        targets = new List<(int, int)>();

        // load prefabs
        item = LoadPrefab("Box");
        player = LoadPrefab("Player");
        wall = LoadPrefab("Wall");
        target = LoadPrefab("Target");
        isbox = LoadPrefab("Is");
        letter = new GameObject[26];
        for (char c = 'a'; c <= 'z'; c++)
        {
            letter[c - 'a'] = LoadPrefab(c.ToString());
        }
    }

    private void Start()
    {
        // load map from txt file
        TextAsset mapTextAsset = Resources.Load<TextAsset>("Levels/" + level);
        if (mapTextAsset == null)
        {
            Debug.Log("File not found");
            return;
        }
        map = mapTextAsset.text.Split('\n');

        FindObjectOfType<Camera>().transform.position = new Vector3(map[0].Length / 2, map.Length / 2, -10);


        // draw map
        int y_pos = map.Length;
        foreach (var row in map)
        {
            int x_pos = 0;
            for (int i = 0; i < row.Length; i++)
            {
                char tileType = row[i];

                switch (tileType)
                {
                    // player
                    case '@':
                        Instantiate(player, new Vector3(x_pos, y_pos), Quaternion.identity);
                        break;

                    // wall
                    case '#':
                        Instantiate(wall, new Vector3(x_pos, y_pos), Quaternion.identity);
                        walls.Add((x_pos, y_pos));
                        break;

                    // target
                    case '.':
                        Instantiate(target, new Vector3(x_pos, y_pos), Quaternion.identity);
                        targets.Add((x_pos, y_pos));
                        break;

                    // is
                    case '-':
                        GameObject isObj = Instantiate(isbox, new Vector3(x_pos, y_pos), Quaternion.identity);
                        bes.Add((x_pos, y_pos), new Be(isObj, BeType.IS));
                        break;

                    // letter
                    default:
                        if (tileType >= 'a' && tileType <= 'z')
                        {
                            GameObject letterObj = Instantiate(letter[row[i] - 'a'], new Vector3(x_pos, y_pos), Quaternion.identity);
                            letters.Add((x_pos, y_pos), new Letter(letterObj, row[i]));
                        }
                        break;
                }
                x_pos++;
            }
            y_pos--;
        }
    }

    private GameObject LoadPrefab(string prefabName)
    {
        return Resources.Load<GameObject>("Prefabs/" + prefabName);
    }
}