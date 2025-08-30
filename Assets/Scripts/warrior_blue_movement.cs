using UnityEngine;
using System.Collections;

public class warrior_blue_movement : MonoBehaviour
{
    public float speed = 2;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    public float updateInterval = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(updateRandomMovement()); 
    }

    private IEnumerator updateRandomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval);

            //float horizontal = (Random.Range(0, 1f) > 0.5 ? Random.Range(5f,10f) : Random.Range(-10f,-5f));
            //float vertical = (Random.Range(0, 1f) > 0.5 ? Random.Range(5f, 10f) : Random.Range(-10f, -5f));
      
                float horizontal = Random.Range(-0.5f,0.5f);
                float vertical = Random.Range(-0.5f,0.5f);

                if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }

                anim.SetFloat("horizontal", Mathf.Abs(horizontal));
                anim.SetFloat("vertical", Mathf.Abs(vertical));


                rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;

                yield return new WaitForSeconds(3f);
                rb.linearVelocity = Vector2.zero;
            horizontal = 0;
            vertical = 0;
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

        }
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
