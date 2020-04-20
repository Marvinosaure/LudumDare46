using UnityEngine;

public class SpiritData : MonoBehaviour
{
    public Spirit spirit;

    [SerializeField]
    private float _groundCheckRadius;

    [SerializeField]
    private LayerMask _collisionLayers;

    [SerializeField]
    private Transform _groundCheck;
    public bool _isGrounded;

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _collisionLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
