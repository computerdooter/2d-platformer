
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript22 : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public float moveSpeed;
    private bool facingRight;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private float JumpForce;
    public bool isAlive;
    public GameObject reset;
    private Slider healthBar;
    public float health = 3f;
    private float healthBurn = 1f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();      //a variable to control the Player's body
        myAnimator = GetComponent<Animator>();
        isAlive = true;
        reset.SetActive(false);
        healthBar = GameObject.Find("health slider").GetComponent<Slider>();
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal"); //a variable that stores the value of our horizontal movement
        //Debug.Log(horizontal);
        if (isAlive)
        {
            PlayerMovement(horizontal);  //function that controls player on the x axis
            Flip(horizontal);
        }
        else
        {
            return;
        }
        HandleInput();
        isGrounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }


    }

    //Function Defnitions
    private void PlayerMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            jump = false;
            myRigidbody.AddForce(new Vector2(0, JumpForce));
        }
        myRigidbody.velocity = new Vector2(horizontal * moveSpeed, myRigidbody.velocity.y);  //adds velocity to the player's body on the x axis
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight; //resets the bool to the opposite value
            Vector2 theScale = transform.localScale;  //creating a vector 2 and storing the local scale values
            theScale.x *= -1;     //
            transform.localScale = theScale;
        }

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            myAnimator.SetBool("jumping", true);
            //Debug.Log ("I'm jumping");
        }
    }
    //function to test for collisions between the array of groundPoints and the Ground LayerMask

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            //if the player is not moving vertically, test each of the Player's groundPoints for collision with Ground
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; 1 < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject) //if any of the colliders in the array of groundPoints comes into contact with another gameobject, return true.
                    {
                        return true;
                    }
                }
            }
        }
        return false; //if the player is not moving along the y axis, return false.
    }
    
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            myAnimator.SetBool("jumping", false);
        }
  
        if (target.gameObject.tag == "deadly")
        {
            ImDead();
            healthBar.value = 0f;
        }
        if (target.gameObject.tag == "damage")
        {
            UpdateHealth();
        }
        if (target.gameObject.tag == "damage2")
        {
            UpdateHealth();
        }

        void UpdateHealth()
        {
            if (health > 0)
            {
                health -= healthBurn;
                healthBar.value = health;
            }
            if (health <= 0)
            {
                ImDead();
            }
        }

        if (isDashing)
        {
            return;
        }
    }
    public void ImDead()
    {
        isAlive = false;
        myAnimator.SetBool("dead", true);
        reset.SetActive(true);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0f;
        myRigidbody.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        myRigidbody.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}