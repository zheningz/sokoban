using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public string[] map;
    public GameObject player;
    public GameObject wall;
    public GameObject box;
    public GameObject target;

    // keep track of objects
    public Dictionary<(int, int), GameObject> boxes;
    public HashSet<(int, int)> walls;
    public List<(int, int)> targets;

    private void Awake()
    {
        boxes = new Dictionary<(int, int), GameObject>();
        walls = new HashSet<(int, int)>();
        targets = new List<(int, int)>();
    }

    private void Start()
    {
        int y_pos = 0;
        foreach (var row in map)
        {
            int x_pos = 0;
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
                x_pos++;
            }
            y_pos--;
        }
    }
}
