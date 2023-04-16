using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RabbitController : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;

    public Vector2 lastMotionVector;
    public bool moving;
    GameObject barn;
    Rigidbody2D rigidbody2d;
    Animator animator;

    float idleDelay = 3f;
    Vector2 stationaryVector = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Barn");
        if (objs.Length > 0)
        {
            barn = objs[0];
        }

        rigidbody2d = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();

        StartCoroutine(Hop());
    }

    // Update is called once per frame
    IEnumerator Hop()
    {
        float horizontal = 0;
        float vertical = 0;
        bool hop = true;

        while(true) {
            if (hop)
            {
                float xProbability = Random.Range(0, 1f);
                float yProbability = 1f - xProbability;

                Vector3 currentPos = transform.position;
                Vector3 direction = barn.transform.position - currentPos;
                horizontal = (xProbability > 0.4) ? direction.x * (1 + Random.Range(-0.5f, 1)) : 0f;
                vertical = (yProbability > 0.4) ? direction.y * (1 + Random.Range(-0.5f, 1)) : 0f;

                Vector2 motionVector = new Vector2(
                    horizontal + Random.Range(-1f, 3f),
                    vertical + Random.Range(-1f, 3f)
                ).normalized;

                rigidbody2d.velocity = motionVector * speed;
                idleDelay = 1.0f + Random.Range(-0.5f, 2);
            } else {
                horizontal = 0;
                vertical = 0;
                rigidbody2d.velocity = stationaryVector;
                idleDelay = 1.0f + Random.Range(-1f, 2);
            }

            animator.SetFloat("horizontal", horizontal);
            animator.SetFloat("vertical", vertical);

            moving = horizontal != 0 || vertical != 0;
            animator.SetBool("moving", moving);
            
            if (horizontal != 0 || vertical != 0)
            {
                lastMotionVector = new Vector2(
                    horizontal,
                    vertical
                ).normalized;
                animator.SetFloat("lastHorizontal", horizontal);
                animator.SetFloat("lastVertical", vertical);
            }
            hop = !hop;
            yield return new WaitForSeconds(idleDelay);
        }
    }
}
