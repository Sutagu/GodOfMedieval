using UnityEngine;

public class elevation_exit : MonoBehaviour
{
    //public Collider2D[] mountainColliders;
    //public Collider2D[] mountainBoundary;
    public string Character = "Character";
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character Elevated"))
        {
            Debug.Log("Character layer is indeed equal to Character Elevated, now converting to Character");
            collision.gameObject.layer = LayerMask.NameToLayer(Character);
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

            Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));
            //foreach (Collider2D mountain in mountainColliders)
            //{
            //    Physics2D.IgnoreCollision(mountain, other, true);

            //}
            //foreach (Collider2D boundary in mountainBoundary)
            //{
            //    Physics2D.IgnoreCollision(boundary, other, false);

            //}
        }
    }
}
