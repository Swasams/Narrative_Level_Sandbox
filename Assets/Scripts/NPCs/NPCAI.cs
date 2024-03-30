using UnityEngine;

public class NPCAI : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("Movement")]
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float followDistance;
    Vector2 moveDirection;

    private GameObject player;

    private void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

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
            anim.SetBool("Idle", false);

            moveDirection = target.position - transform.position;

            anim.SetFloat("Horizontal", moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.y);
            anim.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }
}
