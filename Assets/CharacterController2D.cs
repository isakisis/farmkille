using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] GridManager gridManager;

    Rigidbody2D rigidbody2d;
    Animator animator;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();  
    }

    private void Update()
    {
        Vector2 motionVector = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
        ).normalized;

        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3Int locationOnMap = gridManager.WorldToCell(transform.position);
            if (gridManager.IsLocationEmpty(locationOnMap)) {
                gridManager.AddCropAt(locationOnMap);
            }
        };

        rigidbody2d.velocity = motionVector * speed;

        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
    }
}
