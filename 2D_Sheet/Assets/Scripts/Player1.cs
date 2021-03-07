using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player1 : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    Rigidbody2D myRigitBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityScaleAtStart;
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
        Run();
        Jump();
        FlipSprite();
        ClimbLadder();
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
}



