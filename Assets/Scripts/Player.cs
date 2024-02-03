using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 moveDir;
    public LayerMask detectLayer;
    public float moveRatio = 1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            moveDir = Vector2.right;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            moveDir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            moveDir = Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            moveDir = Vector2.down;

        if (moveDir != Vector2.zero)
        {
            if (CheckMove(moveDir))
            {
                Move(moveDir);
            }
        }

        moveDir = Vector2.zero;
    }

    private bool CheckMove(Vector2 dir)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, dir, 1f, detectLayer);
        if (!raycastHit)
            return true;
        else
        {
            if (raycastHit.collider.GetComponent<Box>() != null)
                return raycastHit.collider.GetComponent<Box>().CheckMove(dir);
            else
                return false;
        }
    }

    void Move(Vector2 dir)
    {
        transform.Translate(dir * moveRatio);
    }
}
