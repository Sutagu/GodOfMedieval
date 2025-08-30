using UnityEngine;

public class elevation_entry : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] mountainBoundary;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;

            }
            foreach (Collider2D boundary in mountainBoundary)
            {
                boundary.enabled = true;

            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
