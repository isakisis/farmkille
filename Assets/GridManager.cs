using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tilemap ground;
    [SerializeField] Tilemap objectsNonColliding;
    [SerializeField] Tilemap objectsColliding;

    [SerializeField] GameObject crop;

    public Vector3Int WorldToCell(Vector3 position) {
        return objectsNonColliding.layoutGrid.WorldToCell(position);
    }

    public void AddCropAt(Vector3Int location) {
        Vector3 worldLocation = objectsNonColliding.layoutGrid.CellToWorld(location);
        GameObject newObject = Instantiate(crop, worldLocation, Quaternion.identity);
        CropController cropController = newObject.GetComponent<CropController>();
        cropController.objectsNonColliding = objectsNonColliding;
        cropController.gridManager = this;
    }

    public bool IsLocationEmpty(Vector3Int location) {
        return !objectsNonColliding.HasTile(location) && !objectsColliding.HasTile(location);
    }
}
