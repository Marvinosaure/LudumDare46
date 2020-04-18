using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    public GameObject Player
    {
        get { return _player; }
        set { InitPlayer(value); }
    }

    [SerializeField]
    private float _timeOffset;

    [SerializeField]
    private Vector3 _posOffset;

    private Vector3 _velocity;

    private void Start()
    {
        _player.AddComponent<Character>();
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _player.transform.position + _posOffset, ref _velocity, _timeOffset);
    }

    private void InitPlayer(GameObject player)
    {
        Destroy(_player.GetComponent<Character>());


        _player = player;
        _player.AddComponent<Character>();
    }
}
