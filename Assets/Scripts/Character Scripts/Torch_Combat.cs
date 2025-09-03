using UnityEngine;

public class Torch_Combat : MonoBehaviour
{
    //Script
    private warrior_blue_movement Movement;

    //Gameobject components
    private Rigidbody2D rb;
    private Animator anim;
    private Transform target;

    //Variables
    public float speed = 2;
    public float attackRange = 1.3f;
    public int damage = 15;
    private EnemyState enemyState;
    private bool hasRun = false;
    public Transform AttackPoint;

    //Used in update for debugging states
    //private float timer = 0f;
    //private float interval = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Movement = GetComponent<warrior_blue_movement>();
        enemyState = EnemyState.Exit;
    }
    //Causing damage when collided
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collision")) return;
        collision.gameObject.GetComponent<CharacterHealth>().ChangeHealth(-damage);
    }

    
    //Chasing player based on agro
    void Update()
    {
        if (enemyState==EnemyState.Chasing)
        {
            Chase();
        }else if(enemyState== EnemyState.Attacking)
        {
            Attacking();
        }else if(enemyState == EnemyState.Exit && !hasRun)
        {
            Exit();
        }

        //timer += Time.deltaTime;
        //if (timer >= interval)
        //{
        //    timer = 0f;
        //    foreach (AnimatorControllerParameter param in anim.parameters)
        //    {
        //        Debug.Log($"Name: {param.name}, Value: {anim.GetBool(param.name)}");
        //    }
        //}
    }

    void Chase()
    {
        Vector3 targetPos = target.position;
        Vector3 selfPos = transform.position;
        float x = (targetPos.x - selfPos.x);
        float y = (targetPos.y - selfPos.y);
        float distance = Vector2.Distance(selfPos, targetPos);
        //float distance = Mathf.Sqrt(x * x + y * y);
        
        if (distance < attackRange)
        {

            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Attacking);
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
        Vector3 targetPos = target.position;
        Vector3 selfPos = transform.position;
        float x = (targetPos.x - selfPos.x);
        float y = (targetPos.y - selfPos.y);
        float distance = Mathf.Sqrt(x * x + y * y);

        if ((x > 0 && transform.localScale.x < 0) || (x < 0 && transform.localScale.x > 0))
        {
            Movement.facingDirection *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

        
        if (distance > attackRange)
        {
            ChangeState(EnemyState.Chasing);
        }
    }
    void Exit()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetFloat("horizontal", 0);
        anim.SetFloat("vertical", 0);
        StartCoroutine(Movement.resumeMovement());
        hasRun = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Warrior"))
        {
            Movement.isMoving = false;
            target = collision.gameObject.transform;
            anim.SetFloat("horizontal", 0);
            anim.SetFloat("vertical", 0);
            ChangeState(EnemyState.Chasing);
            
        }
        return;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior"))
        {
            ChangeState(EnemyState.Exit);
        }
    }

    void ChangeState(EnemyState newState)
    {
        Debug.Log("Changing state to:" + newState);
        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("isIdle", false);
                break;
            case EnemyState.Chasing:
                anim.SetBool("isChasing", false);
                break;
            case EnemyState.Attacking:
                anim.SetBool("isAttacking", false);
                break;
            case EnemyState.Exit:
                anim.SetBool("isExit", false);
                hasRun = false;
                break;


        }
        enemyState = newState;

        switch (newState)
        {
            case EnemyState.Idle:
                anim.SetBool("isIdle", true);
                break;
            case EnemyState.Chasing:
                anim.SetBool("isChasing", true);
                break;
            case EnemyState.Attacking:
                anim.SetBool("isAttacking", true);
                break;
            case EnemyState.Exit:
                anim.SetBool("isExit", true);
                break;
        }
    }

    public enum EnemyState
    {
        Exit,
        Idle,
        Chasing,
        Attacking,
    }
}
