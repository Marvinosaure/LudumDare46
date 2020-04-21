using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    [SerializeField] GameObject victory;
    private void OnTriggerEnter2D(Collider2D other)
    {

        other.gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(other.gameObject.transform.parent.GetComponent<Character>());

        //gameObject.GetComponent<Animator>().SetBool("Final", true);
        gameObject.transform.Find("WithBaby").GetComponent<Animator>().SetBool("Final", true);

        StartCoroutine(Victory());
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(5);
        Instantiate(victory);
        yield return new WaitForSeconds(4);
        Persistent.CurrentLevel = 1;
        SceneManager.LoadScene("LevelSelection");
    }
}
