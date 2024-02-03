using UnityEngine;

public class Box : MonoBehaviour
{
    public LayerMask detectLayer;
    public float moveRatio = 1;

    public bool CheckMove(Vector2 dir)
    {
        // ±‹√‚ºÏ≤‚µΩ◊‘…Ì
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 0.5f, detectLayer);

        if (!raycastHit)
        {
            Move(dir);
            return true;
        }
        else
            return false;
    }

    void Move(Vector2 dir)
    {
        transform.Translate(dir * moveRatio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedNum++;
            FindObjectOfType<GameManager>().CheckGoal();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            FindObjectOfType<GameManager>().finishedNum--;
        }
    }
}
