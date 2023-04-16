using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    SpriteRenderer heldItemSpriteRenderer;

    public AudioSource audioSource;
    public AudioClip pickupSfx;
    public AudioClip deliverSfx;

    public Tilemap selectionTilemap;
    public Vector3Int lastSelection;
    public Tile selectionTile;

    [SerializeField] float pickUpOffset = -0.25f;

    public ScoreManager scoreManager;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();  
        heldItemSpriteRenderer = GetComponentsInChildren<SpriteRenderer>(true).Where(
            comp => comp.name == "HeldItem"
        ).First();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 motionVector = new Vector2(
            horizontal, 
            vertical
        ).normalized;

        Vector3 offset = 0.5f * lastMotionVector.normalized;
        Vector3Int locationOnMap = gridManager.WorldToCell(new Vector3(transform.position.x, transform.position.y + pickUpOffset) + offset);

        if (locationOnMap != lastSelection) {
            selectionTilemap.SetTile(lastSelection, null);
            selectionTilemap.SetTile(locationOnMap, selectionTile);
        }
        lastSelection = locationOnMap;

        if (gridManager != null) {
            //Debug.Log(gridManager.isLocationBarn(locationOnMap));
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (heldItem) {
                InteractionBehaviour interaction = heldItem.GetComponent<InteractionBehaviour>();
                if (interaction != null) {
                    interaction.Action(locationOnMap);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log(locationOnMap);

            if (heldItem) {
                if (gridManager.isLocationBarn(locationOnMap)) {
                    if (isHeldItemCrop())
                    {
                        //Debug.Log("Held item is crop");

                        CropController cropController = heldItem.GetComponentInChildren<CropController>();

                        //Debug.Log(cropController.isCropCompleted());

                        if (cropController.isCropCompleted())
                        {
                            scoreManager.UpdateScore(10);

                            heldItem = null;
                            heldItemSpriteRenderer.sprite = null;
                            heldItemSpriteRenderer.enabled = false;

                            audioSource.PlayOneShot(deliverSfx);
                        }
                    } 
                } else if (gridManager.IsLocationEmpty(locationOnMap)) {
                    bool succeeded = gridManager.PutDown(locationOnMap, heldItem);
                    if (succeeded) {
                        heldItem = null;
                        heldItemSpriteRenderer.sprite = null;
                        heldItemSpriteRenderer.enabled = false;
                    }
                } 
            } else {
                if (!heldItem) {
                    heldItem = gridManager.PickUp(locationOnMap);
                    if (heldItem) {
                        PickUpBehaviour pickUpBehaviour = heldItem.GetComponent<PickUpBehaviour>();
                        Sprite s = pickUpBehaviour.getSprite();
                        Debug.Log("heldItemSpriteRenderer.sprite =");
                        Debug.Log(s);
                        heldItemSpriteRenderer.sprite = s;
                        heldItemSpriteRenderer.enabled = true;

                        audioSource.PlayOneShot(pickupSfx);
                    }
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

    private bool isHeldItemCrop() {
        return heldItem.GetComponentInChildren<CropController>() != null;
    }
}

