using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MapGenerator map;

    private void Awake()
    {
        map = FindObjectOfType<MapGenerator>();
    }

    private void Update()
    {
        int dx = 0;
        int dy = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            dx++;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            dx--;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            dy++;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            dy--;

        if (dx != 0 || dy != 0)
        {
            int nx = (int)transform.position.x + dx;
            int ny = (int)transform.position.y + dy;

            if (IsWall(nx, ny))
                return;

            if (IsBox(nx, ny))
            {
                int nnx = dx + nx;
                int nny = dy + ny;
                if (IsWall(nnx, nny) || IsBox(nnx, nny))
                    return;
                GameObject box = GetBox(nx, ny);
                box.transform.position = new Vector3(nnx, nny);
                map.boxes.Remove((nx, ny));
                map.boxes.Add((nnx, nny), box);
            }
            transform.position = new Vector3(nx, ny);
            FindObjectOfType<GameManager>().CheckTarget();
        }
    }

    private bool IsWall(int x, int y)
    {
        return map.walls.Contains((x, y));
    }

    private bool IsBox(int x, int y)
    {
        return map.boxes.ContainsKey((x, y));
    }

    private GameObject GetBox(int x, int y)
    {
        return map.boxes[(x, y)];
    }
}
