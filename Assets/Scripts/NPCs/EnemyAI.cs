using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    Vector2 moveDirection;

    [Header("Attack Settings")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform attackRadiusRef;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackCircleRadius;
    [SerializeField] private float attackRange;

    [Header("Attack Delay")]
    [SerializeField] private bool canAttack;
    [SerializeField] private float attackDelay;

    private GameObject player;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) <= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.eulerAngles = target.eulerAngles;
        }
        else
        {
            //Attack Code
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) >= followDistance)
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            anim.Play("Idle");
        }
        else
        {
            moveDirection = target.position - transform.position;

            anim.SetFloat("Horizontal", moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.y);
            anim.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            if (canAttack)
            {
                SlamAttack();
            }
            else
            {
                anim.SetBool("NewAttack", false);
            }
        }

        if (!canAttack)
        {
            attackDelay += Time.deltaTime;
        }

        if (attackDelay >= 3)
        {
            canAttack = true;
            attackDelay = 0;
        }
    }

    public void SlamAttack()
    {
        anim.SetBool("NewAttack", true);
        canAttack = false;
        Attack();
    }

    private void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackCircleRadius, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("We hit: " + player.name);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackRadiusRef.position, followDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackCircleRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackRadiusRef.position, attackRange);
    }
}