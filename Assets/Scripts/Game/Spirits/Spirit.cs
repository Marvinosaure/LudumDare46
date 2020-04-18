using UnityEngine;

[CreateAssetMenu(fileName = "New Spirit", menuName = "Spirit")]
public class Spirit : ScriptableObject
{
    public enum Type
    {
        small,
        medium,
        Large
    }

    public Type type;
    public float _speed;
    public float _jumpForce;
    public float _throwForce;
}