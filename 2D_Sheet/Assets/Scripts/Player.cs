using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed= 5f;
    Rigidbody2D myRigitBody;
    Animator myAnimator;

    bool isAlive = true;
    void Start()
    {
        myRigitBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigitBody.velocity.y);
        myRigitBody.velocity = playerVelocity;

        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigitBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", PlayerHasHorizontalSpeed);
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
