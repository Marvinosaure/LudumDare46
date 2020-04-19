using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null) Destroy(collision.gameObject); //collision.gameObject.transform.position = new Vector3(-2, 1, 0);
    }
}
