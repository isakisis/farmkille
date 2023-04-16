using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PickUpBehaviour;

public class ScarecrowController : MonoBehaviour
{
    [SerializeField] public GridManager gridManager;
    [SerializeField] public Tilemap objectsNonColliding;

    Vector3Int? tileLocation;

    [SerializeField] AnimatedTile scarecrowTile;
    TiledObjectPickupBehaviour tiledObjectPickupBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        tiledObjectPickupBehaviour = GetComponent<TiledObjectPickupBehaviour>();
        tiledObjectPickupBehaviour.tilemap = objectsNonColliding;
        tiledObjectPickupBehaviour.placed = true;
        tiledObjectPickupBehaviour.SetTile(scarecrowTile);
    }
}
