using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    //Interaction Variables
    private Vector2 boxSize = new Vector2(0.3f, 0.6f);
    public GameObject interactIcon;

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
    [Header("Stamina")]
    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;
    public float rechargeRate;
    private Coroutine recharge;

    //Health

    [Header("Health")]
    public Image healthBar;
    public float health, maxHealth;
    public float healthCost;
    public float healthRechargeRate;
    public GameObject deathMenu;

    //Hunger
    [Header("Hunger")]
    public Image hungerBar;
    public float hunger, maxHunger;
    public float hungerCost;

    //Acorns
    public int acornWealth = 0;
    public TextMeshProUGUI AcornWealthText;

    //Quest
    public Quest quest;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public DialogueManager dialogue;

    //Timer
    private bool _timerActive;
    private float _currentTime;
    [SerializeField] private TMP_Text _text;

    //Pause Menu
    public bool isPaused;
    public GameObject pauseMenu;

//START
    private void Start()
    {
        _currentTime = 0;
        rb = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        interactIcon.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Theme");
    }

//UPDATE
    void Update()
    {

        //Timer
        if (_timerActive)
        {
            _currentTime = _currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

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

        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        //Hunger
        if (hunger < 0)
        {
            hunger = 0;
        }
        hunger -= hungerCost * Time.deltaTime;
        hungerBar.fillAmount = hunger / maxHunger;

        //Health
        if (hunger <= 0)
        {
            health -= healthCost * Time.deltaTime;
        }
        healthBar.fillAmount = health / maxHealth;

        if(health <= 0)
        {
            health = 0;
            PlayerDeath();
        }

        //Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(rb.rotation >= 0){rb.SetRotation(0);}

        //Acorns
        AcornWealthText.text = acornWealth.ToString();

        //Quest Journal
        
        if(quest.goal.IsReached())
        {
            titleText.text = quest.title;
            descriptionText.text = quest.isReachedDescription;
        }
        else
        {
            titleText.text = quest.title;
            descriptionText.text = quest.description;
        }

        if(!quest.isActive)
        {
            titleText.text = "";
            descriptionText.text = "No current quests.";
        }
        
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

    //Stamina Recharge
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

    //Finishing a quest
    public void HandInQuest()
    {
        if(quest.isComplete == false)
        {
            Debug.Log("COMPLETE");
            FindObjectOfType<AudioManager>().Play("QuestComplete");
            acornWealth += quest.acornReward;
            if (quest.item != null){Inventory.instance.Add(quest.item);}
            if (quest.item != null){Inventory.instance.Remove(quest.itemDelete);}
            quest.Complete();
            dialogue.EndDialogue();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    //Death
    public void PlayerDeath()
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        deathMenu.SetActive(true);
        return;

    }

    //Interact Icon
    public void OpenInteractIcon(){
        interactIcon.SetActive(true);
    }
    public void CloseInteractIcon(){
        interactIcon.SetActive(false);
    }

    //Timer

    public void StartTimer()
    {
        _timerActive = true;
    }

    public void StopTimer()
    {
        _timerActive = false;
    }

    //Pause Menu

    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
		pauseMenu.SetActive(isPaused);
    }

}
