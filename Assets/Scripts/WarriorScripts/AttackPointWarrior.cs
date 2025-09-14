using UnityEngine;

public class AttackPointWarrior : MonoBehaviour
{
    public int damage = 10;
    public PolygonCollider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        if (attackCollider == null) return;
        attackCollider.enabled = false;
        //SaveShape();
    }

    public void EnableAttack(string direction)
    {
        attackCollider.pathCount = 1;
        if (direction.Equals("Up")) attackCollider.SetPath(0, attackUp);
        else if (direction.Equals("Down")) attackCollider.SetPath(0, attackDown);
        else attackCollider.SetPath(0, attackHorizontal);
        attackCollider.enabled = true;
    }

    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goblin"))
        {
            other.GetComponent<CharacterHealth>().ChangeHealth(-damage);
        }
    }
    //Use the below to get polygon collider shape, call on start
    //private void SaveShape()
    //{
    //    Vector2[] savedPoints = attackCollider.points;
    //    foreach (Vector2 p in savedPoints)
    //        Debug.Log($"new Vector2({p.x}f, {p.y}f),");
    //}

    private Vector2[] attackHorizontal = new Vector2[]
    {
        new Vector2(0.4528465f, 1.142323f),
        new Vector2(0.640376f, -0.4931683f),
        new Vector2(1.314171f, -0.7184473f),
        new Vector2(1.364094f, 0.1225531f),
        new Vector2(1.151603f, 0.7489254f)
    };
    private Vector2[] attackDown = new Vector2[]
    {
        new Vector2(1.373077f, -0.1411564f),
        new Vector2(-0.8973778f, 0.09408405f),
        new Vector2(-0.4899653f, -0.9000722f),
        new Vector2(0.4862427f, -1.058006f),
        new Vector2(1.230307f, -0.6616915f)
    };
    private Vector2[] attackUp = new Vector2[]
    {
        new Vector2(-0.1222978f, 0.9485903f),
        new Vector2(-0.9518653f, 0.8690152f),
        new Vector2(-1.379925f, 0.4560572f),
        new Vector2(-1.379925f, 0.4560572f),
        new Vector2(1.255119f, -0.08328748f),
        new Vector2(0.788354f, 0.6883835f)
    };
}
