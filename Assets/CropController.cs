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

    Vector3Int tileLocation;
    float time = 0;

    public CropController() {
    }

    // Start is called before the first frame update
    void Start()
    {
        tileLocation = gridManager.WorldToCell(transform.position);
        objectsNonColliding.SetTile(tileLocation, crop0);
    }

    // Update is called once per frame
    void Update()
    {
        float newTime = time + Time.deltaTime; 

        if (newTime > 9 && time <= 9) {
            objectsNonColliding.SetTile(tileLocation, null);
            Destroy(gameObject);
        } else if (newTime > 6 && time <= 6) {
            objectsNonColliding.SetTile(tileLocation, crop2);
        } else if (newTime > 3 && time <= 3) {
            objectsNonColliding.SetTile(tileLocation, crop1);
        }

        time = newTime;
    }
}
