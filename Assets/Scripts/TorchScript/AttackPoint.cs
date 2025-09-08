using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private int damage = 15;
    public PolygonCollider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<PolygonCollider2D>();
        if(attackCollider == null) return;
        attackCollider.enabled = false;

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
        if (other.gameObject.CompareTag("Warrior"))
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
        new Vector2(0.1570845f, 0.8689525f),
        new Vector2(-0.08507967f, 0.9230232f),
        new Vector2(-0.05926991f, 0.7214618f),
        new Vector2(0.07685673f, 0.5066173f),
        new Vector2(0.002745628f, 0.2300241f),
        new Vector2(-0.1812885f, -0.006993026f),
        new Vector2(-0.02869076f, -0.1364826f),
        new Vector2(0.2029012f, -0.08381438f),
        new Vector2(0.3176848f, 0.232125f),
        new Vector2(0.3473964f, 0.5075361f)
    };
    private Vector2[] attackDown = new Vector2[]
    {
        new Vector2(0.04478371f, 0.07297087f),
        new Vector2(-1.583793f, 0.09361723f),
        new Vector2(-1.202571f, -0.5891298f),
        new Vector2(-0.3501003f, -0.6609299f),
        new Vector2(0.05804563f, -0.3371816f)
    };
    private Vector2[] attackUp = new Vector2[]
    {
        new Vector2(-0.3400822f, 1.21106f),
        new Vector2(-0.8546821f, 1.381285f),
        new Vector2(-1.783106f, 1.355936f),
        new Vector2(-2.12759f, 0.9338297f),
        new Vector2(-1.995714f, 0.5400615f),
        new Vector2(-0.06971908f, 0.8127038f),
    };
}
