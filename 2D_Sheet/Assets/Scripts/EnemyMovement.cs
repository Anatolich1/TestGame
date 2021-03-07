using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    Rigidbody2D enemyBody;

    public EnemyMovement()
    {
        enemySpeed = 1f;
    }

    private void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        if (IsFacingRight())
        {
            enemyBody.velocity = new Vector2(enemySpeed, 0f);
        }
        else
        {
            enemyBody.velocity = new Vector2(-enemySpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)),1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
