using UnityEngine;
using System.Collections;

public class warrior_blue_movement : MonoBehaviour
{
    public float speed = 1.5f;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public float updateInterval = 2f;
    public bool isMoving = true;
    public float horizontal;
    public float vertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdateRandomMovement());
        Debug.Log("Script Started!");
    }

    private IEnumerator UpdateRandomMovement()
    {
        while (true)
        {
            if (isMoving)
            {
                horizontal = Random.Range(-1f, 1f);
                vertical = Random.Range(-1f, 1f);
                Vector2 moveDir = new Vector2(horizontal, vertical).normalized;

                if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
                {
                    Flip();
                }

                anim.SetFloat("horizontal", Mathf.Abs(horizontal));
                anim.SetFloat("vertical", Mathf.Abs(vertical));

                rb.linearVelocity = moveDir * speed;

                yield return new WaitForSeconds(1.5f);

                rb.linearVelocity = Vector2.zero;
                anim.SetFloat("horizontal", 0);
                anim.SetFloat("vertical", 0);
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("On Collision is called");
        if (collision.gameObject.CompareTag("Collision"))
        {
            Debug.Log("Player has collided");
 
            horizontal = -horizontal ;
            vertical = -vertical ;
            if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
            {
                Flip();
            }

            rb.linearVelocity = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));
            StartCoroutine(resumeMovement());
        }
    }

    IEnumerator resumeMovement()
    {
        isMoving = false;
        yield return new WaitForSeconds(updateInterval);
        isMoving = true;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    if(horizontal > 0 && transform.localScale.x < 0 ||  horizontal < 0 && transform.localScale.x > 0)
    //    {
    //        Flip();
    //    }

    //    anim.SetFloat("horizontal", Mathf.Abs(horizontal));
    //    anim.SetFloat("vertical", Mathf.Abs(vertical));


    //    rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    //}

    void Flip() 
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
