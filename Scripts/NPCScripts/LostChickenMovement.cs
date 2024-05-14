using UnityEngine;

public class LostChickenMovement : MonoBehaviour
{

    public float moveSpeed; 
    private Rigidbody2D rb;

    public Animator animator;

    public bool isWalking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;

    private int WalkDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
    }

    void Update()
    {
        if (isWalking)
        {
            animator.SetBool("Idle", false);
            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0: 
                    animator.SetBool("Up", true);
                    animator.SetBool("Down", false);
                    animator.SetBool("Right", false);
                    animator.SetBool("Left", false);
                    rb.velocity = new Vector2(0, moveSpeed);
                    break;
                case 1:
                    animator.SetBool("Right", true);
                    animator.SetBool("Down", false);
                    animator.SetBool("Up", false);
                    animator.SetBool("Left", false);
                    rb.velocity = new Vector2(moveSpeed, 0);
                    break;
                case 2:
                    animator.SetBool("Down", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Up", false);
                    animator.SetBool("Left", false);
                    rb.velocity = new Vector2(0, -moveSpeed);
                    break;
                case 3:
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Up", false);
                    animator.SetBool("Down", false);
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    break;
            }
            if(walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            rb.velocity = Vector2.zero;

            animator.SetBool("Idle", true);

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
        
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
