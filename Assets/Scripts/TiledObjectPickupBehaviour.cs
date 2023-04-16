using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiledObjectPickupBehaviour : MonoBehaviour, PickUpable
{
    [SerializeField] public TileBase tile;
    [SerializeField] public bool placed;
    [SerializeField] public Tilemap tilemap;

    public void SetTile(TileBase newTile) {
        tile = newTile;
        if (placed) {
            Vector3Int location = tilemap.layoutGrid.WorldToCell(transform.position);
            tilemap.SetTile(location, tile);
        }
    }

    public Sprite GetSprite() {
        if (tile is Tile) {
            return (tile as Tile).sprite;
        } else if (tile is AnimatedTile) {
            return (tile as AnimatedTile).m_AnimatedSprites[0];
        } else {
            return null;
        }
    }

    public void PickUp() {
        Vector3Int location = tilemap.layoutGrid.WorldToCell(transform.position);
        tilemap.SetTile(location, null);
        placed = false;
    }

    public void PutDown(Vector3Int newTileLocation) {
        transform.position = tilemap.layoutGrid.CellToWorld(newTileLocation);
        tilemap.SetTile(newTileLocation, tile);
        placed = true;
    }
}
