using UnityEngine;
using UnityEngine.AI;

public class BlueFactionCombat : MonoBehaviour
{
    //Script
    private RNGMovement Movement;
    private AttackPointWarrior Weapon;
    
    //GameObject components
    private Rigidbody2D rb;
    private Animator anim;
    private Transform target;
    private BlueState blueState;
    NavMeshAgent agent;

    //Variables
    private bool hasRun = false;
    private bool hasChased = false;
    private float attackRange = 1.3f;
    public float speed = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Movement = GetComponent<RNGMovement>();
        Transform child = transform.GetChild(0);
        Weapon = child.GetComponent<AttackPointWarrior>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Exit();
    }
    
    void Update()
    {
        if (blueState == BlueState.Exit && !hasRun) Exit();
        else if (blueState == BlueState.Chasing) Chase();
        else if (blueState == BlueState.Attacking) Attacking();
    }

    void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        hasRun = true;
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
        //Vector2 direction = new Vector2(x, y);

        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            Movement.facingDirection *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        agent.SetDestination(target.position);
        //rb.linearVelocity = direction.normalized * speed;
    }

    void Attacking()
    {
        agent.ResetPath();
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
    void EnableHit(string direction) => Weapon.EnableAttack(direction);
    void DisableHit() => Weapon.DisableAttack();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Goblin") && !hasChased)
        {
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
        hasRun = false;
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
