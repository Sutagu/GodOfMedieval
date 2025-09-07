using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private int damage = 15;
    public Collider2D attackCollider;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        if(attackCollider == null) return;
        attackCollider.enabled = false;

    }

    public void EnableAttack()
    {
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
}
