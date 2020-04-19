using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform levelGrid;
    [SerializeField] int levelCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int level = 1; level <= levelCount; level++)
        {
            var go = Instantiate(buttonPrefab);
            go.transform.SetParent(levelGrid);
            go.GetComponent<LevelSelectionButton>().Level = level;
            go.transform.localScale = Vector3.one;
        }
    }
}
