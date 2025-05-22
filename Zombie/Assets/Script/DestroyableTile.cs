using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyableTile : MonoBehaviour
{
    public LayerMask layerToReg;
    public Tilemap tilemap;
    public Dictionary<Vector3, int> tilesStats = new();

    public int defaultHealth;

    public void Start()
    {
        tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            tilesStats.Add(pos, defaultHealth);
        }
    }

    public void OnCollisonEnter2D(Collision2D other)
    {
        if ((layerToReg & (1 << other.gameObject.layer)) != 0)
        {
            Vector3Int hitPoint = tilemap.WorldToCell(other.GetContact(0).point);
            if (tilesStats.ContainsKey(hitPoint))
            {
                tilesStats[hitPoint] -= 10;
                if (tilesStats[hitPoint] <= 0)
                {
                    tilemap.SetTile(hitPoint, null);
                }
            }
        }
    }
}
