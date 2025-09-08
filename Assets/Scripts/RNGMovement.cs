using UnityEngine;
using System.Collections;

public class RNGMovement : MonoBehaviour
{
    //GameObject components
    private Rigidbody2D rb;
    private Animator anim;

    //Variables
    private float speed = 1.5f;
    private float interval = 2f;
    public int facingDirection = 1;
    private float horizontal;
    private float vertical;
    public bool isRandom = true;
    public bool playerControl = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (!playerControl) StartCoroutine(RNGRunner());
    }

    private IEnumerator RNGRunner()
    {
        while (true)
        {
            if (isRandom)
            {
                Debug.Log("Randomly moving");
                interval = RNGRange(true);
                horizontal = RNGRange(false);
                vertical = RNGRange(false);
                Vector2 direction = new Vector2(horizontal, vertical).normalized;

                if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0)) Flip();
                
                setAnim("horizontal", horizontal);
                setAnim("vertical", vertical);
                rb.linearVelocity = direction * speed;

                yield return new WaitForSeconds(1.5f);

                rb.linearVelocity = Vector2.zero;
                setAnim("horizontal", 0);
                setAnim("vertical", 0);
            }
            yield return new WaitForSeconds(interval);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision"))
        {
            horizontal = -horizontal;
            vertical = -vertical;
            if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0)) Flip();
            rb.linearVelocity = new Vector2(horizontal,vertical).normalized;

            setAnim("horizontal", horizontal);
            setAnim("vertical", vertical);
            StartCoroutine(resumeRNG());
        }
    }

    void FixedUpdate()
    {
        if (playerControl)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0)) Flip();

            setAnim("horizontal", horizontal);
            setAnim("vertical", vertical);
            rb.linearVelocity = new Vector2(horizontal,vertical).normalized * speed;
        }
    }

    public IEnumerator resumeRNG()
    {
        isRandom = false;
        interval = RNGRange(true);
        yield return new WaitForSeconds(interval);
        isRandom = true;
    }

    void setAnim(string axis, float v)
    {
        anim.SetFloat(axis, Mathf.Abs(v));
    }

    float RNGRange(bool x)
    {
        if (x) return Random.Range(1f, 4f);
        return Random.Range(-1f, 1f);
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
