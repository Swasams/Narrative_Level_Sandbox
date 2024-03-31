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

    [Header("Current Scene")]
    [SerializeField] private int currentScene;

    [Header("Controls")]
    [SerializeField] private bool canMove;
    [SerializeField] private bool canActivateAttack;

    private GameObject player;

    private void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player");
        target = player.transform;

        currentScene = SceneManager.GetActiveScene().buildIndex;

        canMove = true;
        canActivateAttack = true;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.position) <= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.eulerAngles = target.eulerAngles;
        }
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) >= followDistance)
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            anim.SetBool("Idle", true);
        }
        else
        {
            if (!canMove)
            {
                return;
            }

            anim.SetBool("Idle", false);

            moveDirection = target.position - transform.position;

            anim.SetFloat("Horizontal", moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.y);
            anim.SetFloat("Speed", moveDirection.sqrMagnitude);
        }

        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            if (canAttack)
            {
                QuickAttack();
            }
            else
            {
                
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

    public void QuickAttack()
    {
        if (!canActivateAttack)
        {
            return;
        }

        canAttack = false;
        Attack();
    }

    private void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackCircleRadius, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("We hit: " + player.name);

            SceneManager.LoadScene(currentScene);
        }
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetCanActivateAttack(bool canActivateAttack)
    {
        this.canActivateAttack = canActivateAttack;
    }

    public void SetLookDirection(Vector2 lookDirectionX, Vector2 lookDirectionY)
    {
        anim.SetFloat("LastMoveX", lookDirectionX.x);
        anim.SetFloat("LastMoveY", lookDirectionY.y);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetVisiblity(bool isVisible)
    {
        if (isVisible)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pause"))
        {
            canMove = false;
            canActivateAttack = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pause"))
        {
            canMove = true;
            canActivateAttack = true;
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
