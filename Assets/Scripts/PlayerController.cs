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

            int nnx = dx + nx;
            int nny = dy + ny;

            if (IsLetter(nx, ny))
            {
                if (IsMovable(nnx, nny))
                    return;
                Letter letter = map.letters[(nx, ny)];
                letter.obj.transform.position = new Vector3(nnx, nny);

                map.letters.Remove((nx, ny));
                map.letters.Add((nnx, nny), letter);
            } 
            else if (IsBe(nx, ny))
            {
                if (IsMovable(nnx, nny))
                    return;
                Be be = map.bes[(nx, ny)];
                be.obj.transform.position = new Vector3(nnx, nny);

                map.bes.Remove((nx, ny));
                map.bes.Add((nnx, nny), be);
            }
            else if (IsItem(nx, ny))
            {
                if (IsMovable(nnx, nny))
                    return;
                Item item = map.items[(nx, ny)];
                item.obj.transform.position = new Vector3(nnx, nny);

                map.items.Remove((nx, ny));
                map.items.Add((nnx, nny), item);
            }

            transform.position = new Vector3(nx, ny);
            FindObjectOfType<GameManager>().CheckWord();
            FindObjectOfType<GameManager>().CheckTarget();
        }
    }

    private bool IsMovable(int x, int y)
    {
        return IsWall(x, y) || IsLetter(x, y) || IsBe(x, y) || IsItem(x, y);
    }

    private bool IsWall(int x, int y)
    {
        return map.walls.Contains((x, y));
    }

    private bool IsLetter(int x, int y)
    {
        return map.letters.ContainsKey((x, y));
    }

    private bool IsBe(int x, int y)
    {
        return map.bes.ContainsKey((x, y));
    }

    private bool IsItem(int x, int y)
    {
        return map.items.ContainsKey((x, y));
    }
}
