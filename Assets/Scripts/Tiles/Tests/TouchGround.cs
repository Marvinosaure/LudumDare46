using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchGround : MonoBehaviour
{
    private Animator anim;
    private IEnumerator coroutine;
    private int count=1;
    public bool IsGameOver { get; set; } = false;
    public bool IsCarried { get; set; } = false;

    public GameObject victory;
    public GameObject failure;
    public Text level;
    public float maxFallSpeed = 1;
    public GameObject Mort;


    private Rigidbody2D body;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        // level.text = "Level " + count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCarried && body.velocity.y < -maxFallSpeed) body.velocity = new Vector2(body.velocity.x, -maxFallSpeed);
        // level.text = "Level " + count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;

        if (other.tag=="ground" && !IsGameOver)
        {
            IsGameOver = true;
            StartCoroutine(Failure());
            anim.SetBool("dead",true);
            anim.SetBool("idle",false);
            anim.SetBool("victory", false);
            float x = transform.position.x;
            float y = transform.position.y;
            float z = 0;
            Vector2 bebe;
            bebe.x = x;
            bebe.y = Mathf.Ceil(y + 0.5f);

            Instantiate(Mort, bebe, Quaternion.identity);
            
            


        }

        if (other.tag=="spirit")
        {
            anim.SetBool("dead",false);
            anim.SetBool("idle",true);
            anim.SetBool("victory",false);
        }
    }

    public void Carry()
    {
        IsCarried = true;
        anim.SetTrigger("carry");
    }

    public void Fire()
    {
        IsCarried = false;
        anim.SetTrigger("fly");
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
            count++;
            anim.SetBool("dead", false);
            anim.SetBool("idle", false);
            anim.SetBool("victory", true); 
        }
    }

    IEnumerator Failure()
    {
        anim.SetTrigger("die");
        yield return new WaitForSeconds(2);
        Instantiate(failure);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Level" + Persistent.CurrentLevel);
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(2);
        Instantiate(victory);
        yield return new WaitForSeconds(2);
        Persistent.CurrentLevel += 1;
        SceneManager.LoadScene("Level" + Persistent.CurrentLevel);
    }
}