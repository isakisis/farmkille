using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tilemap ground;
    [SerializeField] Tilemap objectsNonColliding;
    [SerializeField] Tilemap objectsColliding;
    [SerializeField] Tilemap barn;

    [SerializeField] GameObject crop;
    [SerializeField] GameObject scarecrow;
    [SerializeField] GameObject seedBag;

    [SerializeField] GameObject scarecrowSpawnLocation;
    [SerializeField] GameObject seedbagSpawnLocation;

    Dictionary<Vector3Int, GameObject> locationToEntity;

    void Start()
    {
        AddScarecrowAt(WorldToCell(scarecrowSpawnLocation.transform.position));
        AddSeedBagAt(WorldToCell(seedbagSpawnLocation.transform.position));
    }

    GridManager() {
        locationToEntity = new Dictionary<Vector3Int, GameObject>();
    }


    public Vector3Int WorldToCell(Vector3 position) {
        return objectsNonColliding.layoutGrid.WorldToCell(position);
    }

    public void AddCropAt(Vector3Int location) {
        Vector3 worldLocation = objectsNonColliding.layoutGrid.CellToWorld(location);
        GameObject newObject = Instantiate(crop, worldLocation, Quaternion.identity);
        CropController cropController = newObject.GetComponent<CropController>();
        cropController.objectsNonColliding = objectsNonColliding;
        cropController.gridManager = this;

        locationToEntity.Add(location, newObject);
    }

    public void AddSeedBagAt(Vector3Int location) {
        Vector3 worldLocation = objectsNonColliding.layoutGrid.CellToWorld(location);
        GameObject newObject = Instantiate(seedBag, worldLocation, Quaternion.identity);
        SeedBagController seedBagController = newObject.GetComponent<SeedBagController>();
        seedBagController.objectsNonColliding = objectsNonColliding;
        seedBagController.gridManager = this;

        locationToEntity.Add(location, newObject);
    }

    public void AddScarecrowAt(Vector3Int location)
    {
        Vector3 worldLocation = objectsNonColliding.layoutGrid.CellToWorld(location);
        GameObject newObject = Instantiate(scarecrow, worldLocation, Quaternion.identity);
        ScarecrowController scarecrowController = newObject.GetComponent<ScarecrowController>();
        scarecrowController.objectsNonColliding = objectsNonColliding;
        scarecrowController.gridManager = this;

        locationToEntity.Add(location, newObject);
    }

    public GameObject PickUp(Vector3Int location) {
        if (locationToEntity.ContainsKey(location)) {
            GameObject someObject = locationToEntity[location];
            PickUpBehaviour pickUpBehaviour = someObject.GetComponent<PickUpBehaviour>();
            if (pickUpBehaviour) {
                locationToEntity.Remove(location);
                pickUpBehaviour.PickUp();
                return someObject;
            }
        }

        return null;
    }

    public bool PutDown(Vector3Int location, GameObject someObject) {
        if (!locationToEntity.ContainsKey(location)) {
            PickUpBehaviour pickUpBehaviour = someObject.GetComponent<PickUpBehaviour>();
            if (pickUpBehaviour) {
                locationToEntity.Add(location, someObject);
                pickUpBehaviour.PutDown(location);
                return someObject;
            }
        }

        return false;
    }

    public bool IsLocationEmpty(Vector3Int location) {
        return !objectsNonColliding.HasTile(location) && !objectsColliding.HasTile(location);
    }

    public bool isLocationBarn(Vector3Int location) {
        return barn.HasTile(location);

    }
}
