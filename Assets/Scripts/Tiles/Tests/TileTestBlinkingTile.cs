
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TileTestBlinkingTile : Tile
{
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        Debug.Log($"StartUp {position} {tilemap} {go.GetComponent<BlinkingTile>()}");

        go.GetComponent<BlinkingTile>().SetTile(position, this);

        return true;
    }

    public override void RefreshTile(Vector3Int location, ITilemap tilemap)
    {
        Debug.Log($"Refresh Tile {location} {tilemap}");
       
    }

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        Debug.Log($"GetTileData");
        tileData.sprite = this.sprite;
        tileData.color = this.color;
        tileData.transform = this.transform;
        tileData.gameObject = this.gameObject;
        tileData.flags = this.flags;

        tileData.colliderType = this.colliderType;
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/TileTestBlinkingTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Blinking Tile", "New Blinking Tile", "Asset", "Save Blinking Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TileTestBlinkingTile>(), path);
    }
#endif
}