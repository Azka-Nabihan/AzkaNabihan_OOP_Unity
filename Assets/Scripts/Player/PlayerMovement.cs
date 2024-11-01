using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / timeToFullSpeed;
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveDirection != Vector2.zero)
        {
            rb.velocity += moveVelocity * moveDirection * Time.deltaTime;
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
                Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
            );
        }
        else
        {
            rb.velocity += GetFriction() * Time.deltaTime;
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x) rb.velocity = new Vector2(0, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < stopClamp.y) rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    public Vector2 GetFriction()
    {
        return new Vector2(
            rb.velocity.x != 0 ? (rb.velocity.x > 0 ? stopFriction.x : -stopFriction.x) : 0,
            rb.velocity.y != 0 ? (rb.velocity.y > 0 ? stopFriction.y : -stopFriction.y) : 0
        );
    }

    public void MoveBound() { }

    public bool IsMoving()
    {
        return rb.velocity != Vector2.zero;
    }
}
