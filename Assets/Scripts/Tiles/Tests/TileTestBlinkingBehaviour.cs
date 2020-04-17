using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTestBlinkingBehaviour : MonoBehaviour
{
    public SpriteRenderer sprite;


    Vector3Int tilePosition;
    Tile tile;
    Tilemap tilemap;

    private void Start()
    {
        var tmgo = GameObject.Find("BlinkingTilemap");
        if (tmgo != null) tilemap = tmgo.GetComponent<Tilemap>();
    }

    void Update()
    {
        sprite.color = (Time.time % 1) * Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tilemap != null) tilemap.SetTile(tilePosition, null);
        Destroy(gameObject);
    }

    public void SetTile(Vector3Int pos, Tile t)
    {
        Debug.Log($"SetTile {pos} {t}");
        tilePosition = pos;
        tile = t;
    }
}
