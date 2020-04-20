using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectionButton : MonoBehaviour
{
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Persistent.CurrentLevel = level;
                SceneManager.LoadScene("Level" + Level);
                SoundsManager.instance.AmbientPlay();
            });
            transform.Find("Text").GetComponent<Text>().text = value.ToString();
        }
    }
}
