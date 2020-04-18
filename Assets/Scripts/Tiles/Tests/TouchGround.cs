using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchGround : MonoBehaviour
{
    private Animator anim;
    private IEnumerator coroutine;
    private int count=1;
    public bool IsGameOver { get; set; } = false;

    public GameObject victory;
    public GameObject failure;
    public Text level;
    
    // Start is called before the first frame update
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        // level.text = "Level " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // level.text = "Level " + count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;

        Debug.Log($"BABY TRIGGER {other.tag} {other.name}");
        if (other.tag=="ground" && !IsGameOver)
        {

            IsGameOver = true;
            StartCoroutine(Failure());
            anim.SetBool("die",true);
            anim.SetBool("idle",false);
            anim.SetBool("victorydanse",false);
        }

        if (other.tag=="spirit")
        {
            anim.SetBool("die",false);
            anim.SetBool("idle",true);
            anim.SetBool("victorydanse",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"BABY TRIGGER {collision.tag} {collision.name}");
        if (collision.tag == "cradle" && !IsGameOver)
        {
            gameObject.transform.parent.parent.GetComponentInChildren<BabyCatcher>().Release(Vector2.zero);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.position = collision.transform.position + new Vector3(0, 0.5f, 0);

            // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            IsGameOver = true;
            StartCoroutine(Victory());
            /* count++;
            anim.SetBool("die", false);
            anim.SetBool("idle", false);
            anim.SetBool("victorydanse", true); */
        }
    }

    IEnumerator Failure()
    {
        yield return new WaitForSeconds(2);
        Instantiate(failure);
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1);
        Instantiate(victory);
    }
}