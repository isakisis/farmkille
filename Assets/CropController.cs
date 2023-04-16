using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropController : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    [SerializeField] public Tilemap objectsNonColliding;

    [SerializeField] TileBase crop0;
    [SerializeField] TileBase crop1;
    [SerializeField] TileBase crop2;

    Vector3Int? tileLocation;
    float growthTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        PickUpBehaviour pickUpBehaviour = GetComponent<PickUpBehaviour>();
        pickUpBehaviour.pickUp = PickedUp;
        pickUpBehaviour.putDown = Placed;
        tileLocation = gridManager.WorldToCell(transform.position);
        if (tileLocation.HasValue) {
            objectsNonColliding.SetTile(tileLocation.Value, crop0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tileLocation.HasValue) {
            TileBase prevTile = TileToDrawForTime(growthTime);
            growthTime += Time.deltaTime; 
            TileBase currTile = TileToDrawForTime(growthTime);

            if (currTile != prevTile) {
                objectsNonColliding.SetTile(tileLocation.Value, currTile);

                if (currTile == null) {
                    Destroy(gameObject);
                }
            }
        }
    }

    TileBase TileToDrawForTime(float time) {
        if (time > 9) {
            return crop2;
        } else if (time > 6) {
            return crop2;
        } else if (time > 3) {
            return crop1;
        } else {
            return crop0;
        }
    }

    void PickedUp() {
        objectsNonColliding.SetTile(tileLocation.Value, null);
        tileLocation = null;
    }

    void Placed(Vector3Int newTileLocation) {
        tileLocation = newTileLocation;
        objectsNonColliding.SetTile(tileLocation.Value, TileToDrawForTime(growthTime));
    }
}
