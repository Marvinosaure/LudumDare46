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
    private bool isGameOver = false;

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
        if (other.tag=="ground" && !isGameOver)
        {

            isGameOver = true;
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

        if (other.tag == "cradle" && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(Victory());
            count++;
            anim.SetBool("die", false);
            anim.SetBool("idle", false);
            anim.SetBool("victorydanse", true);
        }
    }

    IEnumerator Failure()
    {
        yield return new WaitForSeconds(2);
        Instantiate(failure);
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(2);
        Instantiate(victory);
    }
}