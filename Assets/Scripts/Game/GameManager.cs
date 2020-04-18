
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

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
    private static Color _dualShockColor = Color.yellow;

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
        SetDualshockLight(_dualShockColor);
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

    private void OnDestroy()
    {
        SetDualshockLight(new Color());
    }

    private void SetDualshockLight(Color color)
    {
        DualShockGamepad dualshock4 = DualShock4GamepadHID.current;
        if (dualshock4 != null)
        {
            dualshock4.SetLightBarColor(color);
        }
    }
}
