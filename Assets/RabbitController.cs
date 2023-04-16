using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
  [SerializeField] public GridManager gridManager;
  [SerializeField] public Tilemap objectsNonColliding;

  [SerializeField] TileBase crop0;
  [SerializeField] TileBase crop1;
  [SerializeField] TileBase crop2;

  Vector3Int tileLocation;
  float time = 0;
  float growSpeed = 6;

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
      float seedlingTime = growSpeed * 1;
      float ripeTime = growSpeed * 2;
      float rotTime = growSpeed * 3;

      if (newTime > rotTime && time <= rotTime) {
          objectsNonColliding.SetTile(tileLocation, null);
          Destroy(gameObject);
      } else if (newTime > ripeTime && time <= ripeTime) {
          objectsNonColliding.SetTile(tileLocation, crop2);
      } else if (newTime > seedlingTime && time <= seedlingTime) {
          objectsNonColliding.SetTile(tileLocation, crop1);
      }

      time = newTime;
  }
}
