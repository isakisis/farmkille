using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class SeedBagController : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    [SerializeField] public Tilemap objectsNonColliding;

    [SerializeField] Tile seedBagTile;
    TiledObjectPickupBehaviour tiledObjectPickupBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        tiledObjectPickupBehaviour = GetComponent<TiledObjectPickupBehaviour>();
        tiledObjectPickupBehaviour.tilemap = objectsNonColliding;
        tiledObjectPickupBehaviour.placed = true;
        tiledObjectPickupBehaviour.SetTile(seedBagTile);
    }
}
