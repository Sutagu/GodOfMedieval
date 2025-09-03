using UnityEngine;

public class elevation_entry : MonoBehaviour
{

    public string CharacterElevated = "Character Elevated";
    public string Character = "Character";
    public void Start()
    {
        Debug.Log("Entry script is working");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character") && collision is CapsuleCollider2D)
        {
            collision.gameObject.layer = LayerMask.NameToLayer(CharacterElevated);
            Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            return;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Character Elevated") && collision is CapsuleCollider2D)
        {
            collision.gameObject.layer = LayerMask.NameToLayer(Character);
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;

            Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));
            return;
        }
    }
}
