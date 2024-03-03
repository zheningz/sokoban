using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    string[] map;

    public string level = "1-1";

    public GameObject player;
    public GameObject wall;
    public GameObject box;
    public GameObject target;
    public GameObject isbox;
    public GameObject[] letters;

    // keep track of objects
    public Dictionary<(int, int), GameObject> boxes;
    public HashSet<(int, int)> walls;
    public List<(int, int)> targets;

    private void Awake()
    {
        boxes = new Dictionary<(int, int), GameObject>();
        walls = new HashSet<(int, int)>();
        targets = new List<(int, int)>();

        // load prefabs
        player = LoadPrefab("Player");
        wall = LoadPrefab("Wall");
        box = LoadPrefab("Box");
        target = LoadPrefab("Target");
        isbox = LoadPrefab("Is");

        letters = new GameObject[26];
        for (char c = 'A'; c <= 'Z'; c++)
        {
            letters[c - 'A'] = LoadPrefab(c.ToString());
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

        // draw map
        int y_pos = map.Length / 2;
        foreach (var row in map)
        {
            int x_pos = -map.Length / 2;
            for (int i = 0; i < row.Length; i++)
            {
                // player
                if (row[i] == '@')
                {
                    Instantiate(player, new Vector3(x_pos, y_pos), Quaternion.identity);
                }

                // wall
                if (row[i] == '#')
                {
                    Instantiate(wall, new Vector3(x_pos, y_pos), Quaternion.identity);
                    walls.Add((x_pos, y_pos));
                }

                // box or object
                if (row[i] == '+')
                {
                    GameObject boxObj = Instantiate(box, new Vector3(x_pos, y_pos), Quaternion.identity);
                    boxes.Add((x_pos, y_pos), boxObj);
                }

                // target
                if (row[i] == '.')
                {
                    Instantiate(target, new Vector3(x_pos, y_pos), Quaternion.identity);
                    targets.Add((x_pos, y_pos));
                }

                // is
                if (row[i] == '-')
                {
                    GameObject isObj = Instantiate(isbox, new Vector3(x_pos, y_pos), Quaternion.identity);
                    boxes.Add((x_pos, y_pos), isObj);
                }

                // letters
                if (row[i] >= 'A' && row[i] <= 'Z')
                {
                    GameObject letterObj = Instantiate(letters[row[i] - 'A'], new Vector3(x_pos, y_pos), Quaternion.identity);
                    boxes.Add((x_pos, y_pos), letterObj);
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
