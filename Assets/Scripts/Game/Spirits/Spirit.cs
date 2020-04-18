using UnityEngine;

[CreateAssetMenu(fileName = "New Spirit", menuName = "Spirit")]
public class Spirit : ScriptableObject
{
    public enum Type
    {
        type1,
        type2,
        type3,
        type4
    }

    public Type type;
    public float _speed;
    public float _jumpForce;
}