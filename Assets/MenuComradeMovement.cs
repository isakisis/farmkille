using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuComradeMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-1f, -1f, 0) * 1f;
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector2.Reflect(lastVelocity.normalized, 
                                        collision.contacts[0].normal);
    
        rb.velocity = direction * Mathf.Max(speed, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(-0.3f,-0.3f,0) * 1.5f * Time.deltaTime,Space.World);
        transform.Rotate(new Vector3(0,0,10f) * 1f * Time.deltaTime,Space.Self);
    }
}
