using UnityEngine;

public class AIChase : MonoBehaviour
{

    public GameObject Player;
    public float speed;
    public float distanceBetween;
    private float distance;
    
    //Movement
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);

        if (distance > 0.3) {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed = Time.deltaTime);

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

    }

}
