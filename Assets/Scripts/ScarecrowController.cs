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

    // Start is called before the first frame update
    void Start()
    {
        PickUpBehaviour pickUpBehaviour = GetComponent<PickUpBehaviour>();
        pickUpBehaviour.pickUp = PickedUp;
        pickUpBehaviour.putDown = Placed;
        pickUpBehaviour.getSprite = GetSprite;

        tileLocation = gridManager.WorldToCell(transform.position);
        if (tileLocation.HasValue)
        {
            objectsNonColliding.SetTile(tileLocation.Value, scarecrowTile);
        }
    }


    Sprite GetSprite()
    {
        return scarecrowTile.m_AnimatedSprites[0];
    }

    void PickedUp()
    {
        objectsNonColliding.SetTile(tileLocation.Value, null);
        tileLocation = null;
    }

    void Placed(Vector3Int newTileLocation)
    {
        tileLocation = newTileLocation;
        objectsNonColliding.SetTile(tileLocation.Value, scarecrowTile);
    }

}
