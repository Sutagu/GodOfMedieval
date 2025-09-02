using UnityEngine;

public class Torch_Combat : MonoBehaviour
{
    public float speed = 2;
    private Rigidbody2D rb;
    private bool isChasing = false;
    private warrior_blue_movement Movement;
    private Transform target;
    private Animator anim;
    public int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Movement = GetComponent<warrior_blue_movement>();
    }
    //Causing damage when collided
    public int damage = 15;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision")) return;
        collision.gameObject.GetComponent<CharacterHealth>().ChangeHealth(-damage);
    }

    //Chasing player based on agro
    void Update()
    {
        if (isChasing)
        {   
            Debug.Log("Chasing that mofo");
            Vector3 targetPos = target.position;
            Vector3 selfPos = transform.position;
            float x = (targetPos.x - selfPos.x);
            float y = (targetPos.y - selfPos.y);

            Vector2 direction = new Vector2 (x,y);
            if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
            {
                facingDirection *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            anim.SetFloat("horizontal", Mathf.Abs(x));
            anim.SetFloat("vertical", Mathf.Abs(y));
            rb.linearVelocity = direction.normalized;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something has entered the no no square");
        if(collision.gameObject.CompareTag("Warrior"))
        {
            Debug.Log("I found the something");
            Movement.isMoving = false;
            target = collision.gameObject.transform;
            isChasing = true;
            
        }
        return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Something has left the no no square");
        if (collision.gameObject.CompareTag("Warrior"))
        {
        Debug.Log("I lost the something");
            rb.linearVelocity = Vector2.zero;
            isChasing = false;
            StartCoroutine(Movement.resumeMovement());
            anim.SetFloat("horizontal", 0);
            anim.SetFloat("vertical", 0);
        }
    }
}
