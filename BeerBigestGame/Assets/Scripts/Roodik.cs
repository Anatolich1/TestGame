using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roodik : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpspeed = 5f;

    Animator animations;
    Rigidbody2D myRigitBody;
    Collider2D coliders;
    void Start()
    {
        myRigitBody = GetComponent<Rigidbody2D>();
        animations = GetComponent<Animator>();
        coliders = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Flip();
        Jump();
    }

    private void Run()
    {
        float Control = Input.GetAxis("Horizontal");
        Vector2 myRigitBodyVelocity = new Vector2(Control * runSpeed, myRigitBody.velocity.y);
        myRigitBody.velocity = myRigitBodyVelocity;

        bool HorizontalSpeed = Mathf.Abs(myRigitBody.velocity.x) > Mathf.Epsilon;
        animations.SetBool("Running", HorizontalSpeed);
    }

    private void Jump()
    {
        if (!coliders.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 jumpvelocity = new Vector2(0f, jumpspeed);
            myRigitBody.velocity += jumpvelocity;
        }
    }

    private void Flip()
    {
        bool HorizontalSpeed = Mathf.Abs(myRigitBody.velocity.x) > Mathf.Epsilon;
        if (HorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigitBody.velocity.x), 1f); 
        }
    }
}
