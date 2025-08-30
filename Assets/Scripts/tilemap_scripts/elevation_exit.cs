using UnityEngine;

public class elevation_exit : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] mountainBoundary;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = true;

            }
            foreach (Collider2D boundary in mountainBoundary)
            {
                boundary.enabled = false;

            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
