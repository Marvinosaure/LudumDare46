
using System.Collections;
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

    [SerializeField]
    bool _doShuffle;

    [SerializeField]
    bool _doNerfTwins;

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

    IEnumerator Start()
    {
        yield return null; // wait for everybody to be initialised

        foreach(Transform child in _spawner.transform)
        {
            child.Find("Marker").gameObject.SetActive(false);
            _spirits.Add(child);
        }
        _spirits[0].transform.Find("Marker").gameObject.SetActive(true);

        // shuffle
        if (_doShuffle)
        {
            for (int i = 0; i < 100; i++)
            {
                int index = Random.Range(1, _spirits.Count - 1);

                var v = _spirits[index];
                _spirits.RemoveAt(index);
                _spirits.Add(v);
            }
        }


        // remove twins
        if (_doNerfTwins)
        {
            int searchIndex = 1;
            while (searchIndex < _spirits.Count)
            {
                Debug.Log($"search index {searchIndex}");
                var first = _spirits[searchIndex];
                var toRemoveIndex = _spirits.FindLastIndex(s => Mathf.Abs(s.transform.position.y - first.transform.position.y) < 1);
                Debug.Log($"toRemoveIndex index {toRemoveIndex}");

                if (toRemoveIndex != -1)
                {
                    var nerf = _spirits[toRemoveIndex];
                    _spirits.RemoveAt(toRemoveIndex);
                    Destroy(nerf.gameObject);
                }
                searchIndex++;
            }
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
            // DontDestroyOnLoad(gameObject);
        }
    }

    public void ChangeSpirit()
    {
        _spirits[currentSpirit].transform.Find("Marker").gameObject.SetActive(false);
        currentSpirit++;

        if(currentSpirit >= _spirits.Count)
        {
            currentSpirit = 0;
        }
        _spirits[currentSpirit].transform.Find("Marker").gameObject.SetActive(true);
        _cameraFollow.Player = _spirits[currentSpirit].gameObject;

    }

    private void OnDestroy()
    {
        SetDualshockLight(new Color());
    }

    private void SetDualshockLight(Color color)
    {
        /* DualShockGamepad dualshock4 = DualShock4GamepadHID.current;
        if (dualshock4 != null)
        {
            dualshock4.SetLightBarColor(color);
        } */
    }
}
