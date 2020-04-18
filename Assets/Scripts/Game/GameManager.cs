using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject _camera;
    private CameraFollow _cameraFollow;
    
    [SerializeField]
    private GameObject _spawner;

    private List<Transform> _spirits = new List<Transform>();
    public List<Transform> Spirits
    {
        get { return _spirits; }
    }

    private int currentSpirit = 0;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        foreach(Transform child in _spawner.transform)
        {
            _spirits.Add(child);
        }

        _cameraFollow = _camera.GetComponent<CameraFollow>();
    }

    private void Singleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeSpirit()
    {
        currentSpirit++;

        if(currentSpirit >= _spirits.Count)
        {
            currentSpirit = 0;
        }

        _cameraFollow.Player = _spirits[currentSpirit].gameObject;
    }
}
