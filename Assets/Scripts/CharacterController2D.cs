using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] GridManager gridManager;
    public Vector2 lastMotionVector;
    Rigidbody2D rigidbody2d;
    Animator animator;
    public bool moving;
    GameObject heldItem;

    public ScoreManager scoreManager;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        Vector2 motionVector = new Vector2(
            horizontal, 
            vertical
        ).normalized;

        if (gridManager != null) {
            Vector3Int locationOnMap = gridManager.WorldToCell(transform.position);
            Debug.Log(gridManager.isLocationBarn(locationOnMap));
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3Int locationOnMap = gridManager.WorldToCell(transform.position);
            if (heldItem) {
                if (gridManager.isLocationBarn(locationOnMap))
                {
                    scoreManager.UpdateScore(10);

                    heldItem = null;
                } else if (gridManager.IsLocationEmpty(locationOnMap)) {
                    bool succeeded = gridManager.PutDown(locationOnMap, heldItem);
                    if (succeeded) {
                        heldItem = null;
                    }
                } 
            } else {
                if (gridManager.IsLocationEmpty(locationOnMap)) {
                    gridManager.AddCropAt(locationOnMap);
                } else if (!heldItem) {
                    heldItem = gridManager.PickUp(locationOnMap);
                }
            }
        };

        rigidbody2d.velocity = motionVector * speed;

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
    }
}

