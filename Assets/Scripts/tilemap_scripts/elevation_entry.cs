using UnityEngine;

public class elevation_entry : MonoBehaviour
{
    //public Collider2D[] mountainColliders;
    //public Collider2D[] mountainBoundary;
    public string CharacterElevated = "Character Elevated";
    public void Start()
    {
        Debug.Log("Entry script is working");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Character has touched the entry");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Debug.Log("Character layer is indeed equal to Character, now converting to Elevated");
            collision.gameObject.layer = LayerMask.NameToLayer(CharacterElevated);

            Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));
            //foreach (Collider2D mountain in mountainColliders)
            //{
            //    Physics2D.IgnoreCollision(mountain, other, true);

            //}
            //foreach (Collider2D boundary in mountainBoundary)
            //{
            //    Physics2D.IgnoreCollision(boundary, other, false);

            //}
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
