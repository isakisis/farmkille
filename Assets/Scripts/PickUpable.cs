using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PickUpable {
    void PickUp();
    void PutDown(Vector3Int location);
    Sprite GetSprite();
}
