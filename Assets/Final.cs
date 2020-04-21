using UnityEngine;

public class Final : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        Destroy(other.gameObject);

        //gameObject.GetComponent<Animator>().SetBool("Final", true);
        gameObject.transform.Find("WithBaby").GetComponent<Animator>().SetBool("Final", true);
    }
}
