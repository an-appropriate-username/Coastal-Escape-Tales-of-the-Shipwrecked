using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Interaction Variables
    private Vector2 boxSize = new Vector2(0.3f, 0.6f);

    // Movement Variables 
    public float moveSpeed = 1.5f;
    public float sprint = 1f;
    public float currentSpeed;

    //Player Variables
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    //UI Variables

    //Stamina
    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;
    public float rechargeRate;
    private Coroutine recharge;

    //Hunger
    public Image hungerBar;
    public float hunger, maxHunger;
    public float hungerCost;


//START
    private void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

//UPDATE
    void Update()
    {

        //Inputs
        if(Input.GetKeyDown(KeyCode.E))
        CheckInteraction();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed + sprint;
            stamina -= runCost * Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                currentSpeed = moveSpeed;
            }  
            staminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        //Hunger
        if (hunger < 0) hunger = 0;
        hunger -= hungerCost * Time.deltaTime;
        hungerBar.fillAmount = hunger / maxHunger;

        //Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

//FIXED UPDATE
    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movement.normalized * currentSpeed * Time.fixedDeltaTime); 
        //Sprinting
        rb.AddForce (movement * currentSpeed);
    }

//CHECK INTERACTION
    private void CheckInteraction()
    {
        //Interaction
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if(rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(stamina < maxStamina)
        {
            stamina += rechargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }

    }

}
