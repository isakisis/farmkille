using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropController : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    [SerializeField] public Tilemap objectsNonColliding;

    [SerializeField] Tile crop0;
    [SerializeField] Tile crop1;
    [SerializeField] Tile crop2;

    float growthTime = 0;
    TiledObjectPickupBehaviour tiledObjectPickupBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        tiledObjectPickupBehaviour = GetComponent<TiledObjectPickupBehaviour>();
        tiledObjectPickupBehaviour.tilemap = objectsNonColliding;
        tiledObjectPickupBehaviour.placed = true;
        tiledObjectPickupBehaviour.SetTile(crop0);
    }

    // Update is called once per frame
    void Update()
    {
        if (tiledObjectPickupBehaviour.placed) {
            Tile prevTile = TileToDrawForTime(growthTime);
            growthTime += Time.deltaTime; 
            Tile currTile = TileToDrawForTime(growthTime);

            if (currTile != prevTile) {
                tiledObjectPickupBehaviour.SetTile(currTile);
            }
        }
    }

    Tile TileToDrawForTime(float time) {
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
}
