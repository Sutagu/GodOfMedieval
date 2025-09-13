using UnityEngine;

public class BlueFactionCombat : MonoBehaviour
{
    //Script
    private RNGMovement Movement;
    
    //GameObject components
    private Rigidbody2D rb;
    private Animator anim;
    private Transform target;
    private BlueState blueState;

    //Variables
    private bool hasRun = false;
    private bool hasChased = false;
    public int damage = 15;
    private float attackRange = 1.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Movement = GetComponent<RNGMovement>();
        ChangeState(BlueState.Exit);
    }
    
    void Update()
    {
        if (blueState == BlueState.Exit && !hasRun) Exit();
        else if (blueState == BlueState.Chasing) Chase();
        else if (blueState == BlueState.Attacking) Attacking();
    }

    void Exit()
    {
        Debug.Log("Still within exit");
        hasRun = true;
        rb.linearVelocity = Vector2.zero;
        setAnim("horizontal", 0);
        setAnim("vertical", 0);
        Movement.isRandom = true;
        StartCoroutine(Movement.resumeRNG());
    }

    void Chase()
    {
        float[] values = getVectorValues();
        float x = values[0];
        float y = values[1];
        if (values[2] < attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(BlueState.Attacking);
            return;
        }
        Vector2 direction = new Vector2(x, y);

        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            Movement.facingDirection *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        rb.linearVelocity = direction.normalized;
    }

    void Attacking()
    {
        float[] values = getVectorValues();
        float x = values[0];
        float y = values[1];
        if (y > 0.7f) setAnim("isAttackUp", true);
        else if (y < -0.7f) setAnim("isAttackDown", true);
        else setAnim("isAttack", true);

        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            Movement.facingDirection *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (values[2] > attackRange + 0.5f)
        {
            ChangeState(BlueState.Chasing);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision")) return;
        collision.gameObject.GetComponent<CharacterHealth>().ChangeHealth(-damage);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goblin") && !hasChased)
        {
            Debug.Log("Entering trigger");
            Movement.isRandom = false;
            target = collision.gameObject.transform;
            setAnim("horizontal", 0);
            setAnim("vertical", 0);
            hasChased = true;
            ChangeState(BlueState.Chasing);
        }
        return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goblin"))
        {
            Debug.Log("Exit trigger");
            hasChased = false;
            ChangeState(BlueState.Exit);
        }
    }

    float[] getVectorValues()
    {
        Vector3 targetPos = target.position;
        Vector3 selfPos = transform.position;
        float x = (targetPos.x - selfPos.x);
        float y = (targetPos.y - selfPos.y);
        float distance = Vector2.Distance(selfPos, targetPos);

        return new float[] {x,y,distance};
    }

    void ChangeState(BlueState newState)
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Changing state to: " + newState);
        Movement.isRandom = false;

        switch (blueState)
        {
            case BlueState.Exit:
                setAnim("isExit", false); 
                break;
            case BlueState.Chasing:
                setAnim("isChasing", false);
                break;
            case BlueState.Attacking:
                setAnim("isAttack", false);
                setAnim("isAttackUp", false);
                setAnim("isAttackDown", false);
                setAnim("isAtk",false);
                break;
        }
        blueState = newState;

        switch (blueState)
        {
            case BlueState.Exit:
                setAnim("isExit", true);
                break;
            case BlueState.Chasing:
                setAnim("isChasing", true);
                break;
            case BlueState.Attacking:
                setAnim("isAtk", true);
                break;
        }
    }

    void setAnim(string name, float s)
    {
        anim.SetFloat(name, Mathf.Abs(s));
    }

    void setAnim(string name, bool active)
    {
        anim.SetBool(name, active);
    }

    public enum BlueState
    {
        Exit,
        Chasing,
        Attacking
    }

    
}
