using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player1 : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float gravityScaleAtStart;

    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f); 

    private Rigidbody2D myRigitBody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider; //зашел сюда и осознал боль своего кода...
    private BoxCollider2D myFeetCollider;

    bool isAlive = true;

    void Start()
    {
        myRigitBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigitBody.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
        SpikeDie();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigitBody.velocity.y);
        myRigitBody.velocity = playerVelocity;

        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigitBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", PlayerHasHorizontalSpeed);
    }
    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigitBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            myAnimator.SetBool("Climbing", false);
            myRigitBody.gravityScale = gravityScaleAtStart; 
            return; 
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerClimb = new Vector2(myRigitBody.velocity.x, controlThrow * climbSpeed);
        myRigitBody.velocity = playerClimb;
        myRigitBody.gravityScale = 0f;

        bool PlayerHasVerticalSpeed = Mathf.Abs(myRigitBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", PlayerHasVerticalSpeed);
    }
    private void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigitBody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigitBody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }

    private void SpikeDie()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }
}



