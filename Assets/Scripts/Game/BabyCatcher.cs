using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyCatcher : MonoBehaviour
{
    [SerializeField] Transform positionCarry;
    [SerializeField] float angleCarry;

    private GameObject baby;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"BABY CATCHER ENTER {collision.gameObject} {collision.tag}");
        if(collision.tag == "baby")
        {
            baby = collision.gameObject;
            collision.gameObject.transform.SetParent(positionCarry);
            collision.gameObject.transform.localPosition = Vector3.zero;
            collision.gameObject.transform.localRotation = Quaternion.Euler(0, 0, angleCarry);
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            // For testing
            StartCoroutine(AutoRelease());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"BABY CATCHER EXIT {collision.gameObject}");
    }

    public void Release(Vector2 velocity)
    {
        baby.transform.SetParent(null);
        baby.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        baby.GetComponent<Rigidbody2D>().velocity = velocity;
        baby = null;
    }

    IEnumerator AutoRelease()
    {
        yield return new WaitForSeconds(2);

        Release(new Vector2(0, 5));
    }
}
