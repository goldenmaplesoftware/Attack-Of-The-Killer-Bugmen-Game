using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerMove2 : MonoBehaviour
{

  

    ///NPC interaction
    private NPC NPCTalk;

    /// Aiming Mechanics


    public event EventHandler<OnShootEventArgs> OnShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
        public Vector3 shellPosition;


    }



    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Transform aimShellPositionTransform;
    private Animator aimAnimator;



    ///Player Mechanics


    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;
    const float groundCheckRadius = 0.02f;
    [SerializeField] bool jump;
    [SerializeField] int totalJumps;
    int jumpsAvailable;
    private Rigidbody2D playerBody;
    float horizontalValue;
    float runSpeed = 2f;
    bool isRunning;
    [SerializeField] Transform groundCheckCollision;
    [SerializeField] LayerMask groundLayer;
    bool facingRight = true;
    Animator animator;
    [SerializeField] bool isGrounded;
    private int facingDirection=1; //-1 is left, 1 is right

    ///Walls
    private bool isTouchingWall;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] public float wallCheckDistance;
    public Transform wallCheck;
    private bool isWallSlide;
    public float wallSlideSpeed;
    
    /// Forces
    public float movementForceInAir;
    public float airDragMultiplier = 0.195f;
    public float jumpHeightMultiplier = 1.005f;
    public float wallHopForce;
    public float wallJumpForce;

    private bool isWalking;
    public float turnTimer;
    public bool canMove2 = true;
    public bool canFlip = true;
    public bool multipleJumps;
    bool coyoteJump;
    private bool dead;

    bool CanMove()
    {
       
        bool can = true;

        if (FindObjectOfType<InventorySystem>().isOpen) ///If inventory is open
            can = false;

        if (FindObjectOfType<InteractionSystem>().isExamining) ///If examine is open
            can = false;

        return can;

    }


    private void updateAnimations() 
    {

       
        animator.SetBool("isWalking", horizontalValue != 0);
        animator.SetBool("isGrounded",isGrounded);
        animator.SetBool("isWallSlide",isWallSlide);
    }


    private void Awake()
    {
        jumpsAvailable = 2;
        playerBody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        aimTransform = transform.Find("Aim");
        aimAnimator = transform.GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
        aimShellPositionTransform = aimTransform.Find("ShellPosition");


    }

    void Update()
    {

        if (!inDialogue())
        {

            HandleShooting();
        if (!CanMove()) ///
            return;

       
        
        

        horizontalValue =Input.GetAxisRaw("Horizontal");
        ///Player input and animations
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isRunning = true;
          
        }

        ///If running is enabled
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isRunning = false;
        }

        ///If jump is enabled
        if (Input.GetButtonDown("jump")) 
        { 
            Jump(); 
            jump = true; 
        }
         
  


        else if (Input.GetButtonUp("jump")) 
            jump = false;
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerBody.velocity.y * jumpHeightMultiplier);

        }
    }


   
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            aimAnimator.SetTrigger("Shoot"); ///This inititates the pistol shooting animation
            OnShoot?.Invoke(this, new OnShootEventArgs
            {

                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
                shellPosition = aimShellPositionTransform.position,


            });
        }
    }





    private void FixedUpdate()
    {
        updateAnimations();
        GroundCheck();
        Movement(horizontalValue);
        CheckWallSlide();
    }

    private void Flip()
    {

        facingDirection *= -1;
    }

    private void CheckWallSlide() 
    {
        if (isTouchingWall && !isGrounded && playerBody.velocity.y < 0)
        {
            isWallSlide = true;
        }

        else
        {
            isWallSlide = false;
        }
    }



    void GroundCheck() 
    {
        bool wasGrounded = isGrounded; ///Previous state retrival
        isGrounded = false; ///When we are not on the ground

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollision.position,groundCheckRadius,groundLayer);

        if (colliders.Length > 0) ///When we are grounded
        {
            isGrounded = true;
            if (!wasGrounded)
            ///Landing
            {

                jumpsAvailable = totalJumps;
            }

        }

        else 
        {
            if (wasGrounded) 
            {
                Debug.Log("No longer on the ground platform");
                StartCoroutine(CoyotoJumpDelay());   ///This activates the timer for the delay when we leave the platform
            }


        }
            ///This will check if we are touching the wall
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer);
      

    }



    IEnumerator CoyotoJumpDelay() ///Ledge delay
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }


    void CheckInput() 
    {

        if (turnTimer >= 0) 
        {
            turnTimer -= Time.deltaTime;
            canMove2 = true;
            canFlip = true;
        }
    }

    void Jump() 
    {
        ///This is our jump mechanic
        if (isGrounded)
        {
            multipleJumps = true;
            isGrounded = false;
            playerBody.velocity = Vector2.up * jumpPower;
            jumpsAvailable--;
        }

        else 
        {
            if (coyoteJump) ///This is the timer for the ledge platform 
            {
                Debug.Log("Left the platform when jumped");
                multipleJumps = true;
                jumpsAvailable--;

                playerBody.velocity = Vector2.up * jumpPower;
              
            }


            if (multipleJumps && jumpsAvailable>0) /// When we jump off of the ground at any position...
            {
                playerBody.velocity = Vector2.up * jumpPower;
                jumpsAvailable--;
            }
        
        }



    }


    void Movement(float direction)
    {

        #region Move And Run Script
        float xValue = direction * speed*100*Time.fixedDeltaTime;



        if (isRunning) 
            xValue *= runSpeed;



        if (isGrounded) ///Freezes the player in place as wall slide is taking place
        {
            Vector2 targetVelocity = new Vector2(xValue, playerBody.velocity.y);
            playerBody.velocity = targetVelocity;
        }

        else if (!isGrounded && !isWallSlide && direction != 0) 
        {
            Vector2 forceToAdd = new Vector2(movementForceInAir * xValue, 0);
            playerBody.AddForce(forceToAdd);

            if (Mathf.Abs(playerBody.velocity.x) > speed) 
            {
                playerBody.velocity = new Vector2(speed * direction , playerBody.velocity.y);
            }
        
        } 
         
        else if(!isGrounded && !isWallSlide && direction==0)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x*airDragMultiplier,playerBody.velocity.y);
        }



        if (isWallSlide)  ///Wall slide
        {
            if (playerBody.velocity.y < -wallSlideSpeed) 
            {
               
                playerBody.velocity = new Vector2(playerBody.velocity.x, -wallSlideSpeed);
            }
        }


        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;



        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;



        Vector3 localScale = Vector3.one;
        if (angle > 0 || angle < 180)
        {
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
            localScale.x = 1f;
        }

        else
        {
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
            localScale.x = -1f;

        }

  
        if (facingRight && direction < 0) 
        {
           
                transform.localScale = new Vector3(-1, 1, 1); ////Flips right to left
                facingRight = false;
                aimTransform.localScale = -localScale;

        }


        else if(!facingRight && direction > 0)
        {
            transform.localScale = transform.localScale = new Vector3(1, 1, 1);  ////Flips left to right
            facingRight = true;
            aimTransform.localScale = localScale;
        }

        animator.SetBool("isGrounded", isGrounded);
        #endregion
    }

    



    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void OnTriggerStay2D(Collider2D collision) ///This is if the player comes in contact with any NPC
    {
        if (collision.gameObject.tag == "NPC")
        {
            NPCTalk = collision.gameObject.GetComponent<NPC>();
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                collision.gameObject.GetComponent<NPC>().ActivateDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCTalk = null;
    }

    private bool inDialogue() 
    {
        if (NPCTalk != null)
        {
            return NPCTalk.dialogueActive();
        }

        else 
        {
            return false;
        }
    }



}
