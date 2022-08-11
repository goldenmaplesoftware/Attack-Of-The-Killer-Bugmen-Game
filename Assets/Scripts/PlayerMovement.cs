using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;



    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpColldown; //Create delay between wall jump
    private float horizontalInput;

   

    private void Awake()
    {
        ///Grabs references for rigid body and animator from objects

  
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
  
        




    }


    private void Update()
    {

        if (!CanMove()) ///
            return;
       
        horizontalInput = Input.GetAxis("Horizontal");

       

        ///This flips the main character to face left or right
        if (horizontalInput > 0.01f) 
        {
            transform.localScale = new Vector3(1.848f, 1.9611f, 1);
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1.848f, 1.9611f,1);
        }

       

        ///Set animator paramaters
        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded", isGrounded());


        ///Wall jump mechanic
        if (wallJumpColldown > 0.018f)
        {
            
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }

            else
            {
                body.gravityScale = 2.0f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }

        else 
        {
            wallJumpColldown += Time.deltaTime;
        }

        print(onWall());


    }

    private void Jump()
    {

        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }



        else if (onWall() && !isGrounded())
        {

            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3.0f, 0);
                transform.localScale=new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            
        
        }



            }


    /// <summary>
    /// Checks if the player is on the ground or not
    /// </summary>
    /// <returns></returns>
    private bool isGrounded() 
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.3f,groundLayer);
        return raycastHit.collider!=null;
    }


    private bool onWall()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.55f, wallLayer);
        return raycastHit.collider != null;
    }



    public bool canAttack() 
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    bool CanMove()
    {
        bool can = true;

        if (FindObjectOfType<InventorySystem>().isOpen) ///If inventory is open
            can = false;

        if (FindObjectOfType<InteractionSystem>().isExamining) ///If examine is open
            can = false;

        return can;

    }


}
