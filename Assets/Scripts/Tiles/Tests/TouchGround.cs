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
            SoundsManager.instance.BabyScratchPlay();
            IsGameOver = true;
            StartCoroutine(Failure());
            // anim.SetBool("dead",true);
            float x = transform.position.x;
            float y = transform.position.y;

            Vector2 bebe;

            var cp = collision.GetContact(0).point;
            var v = new Vector2(x - cp.x, y - cp.y);
            var angle = Mathf.Atan2(v.y, v.x);
            var explosionAngle = 0;
            bebe.y = Mathf.Ceil(y + 0.5f);
            bebe.x = x;

            if (Mathf.Rad2Deg * angle >= -45 && Mathf.Rad2Deg * angle < 45)
            {
                explosionAngle = -90;
                bebe.y = y;
                bebe.x = Mathf.Ceil(x + 0.5f);
            }

            if (Mathf.Rad2Deg * angle < -135 || Mathf.Rad2Deg * angle > 135)
            {
                explosionAngle = 90;
                bebe.y = y;
                bebe.x = Mathf.Floor(x - 0.5f);
            }

            if (Mathf.Rad2Deg * angle < -45 && Mathf.Rad2Deg * angle >= -135)
            {
                explosionAngle = 180;
                bebe.y = Mathf.Floor(y - 0.5f);
                bebe.x = x;
            }


            Debug.Log($"XP {angle} {Mathf.Rad2Deg * angle} {explosionAngle} {v}");
            Instantiate(Mort, bebe, Quaternion.Euler(0, 0, explosionAngle));
        }

        if (other.tag=="spirit")
        {
            // anim.SetBool("dead",false);
            // anim.SetBool("idle",true);
            // anim.SetBool("victory",false);
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
            collision.gameObject.GetComponent<Cradle>().SetBaby(true);
            

            if (gameObject.transform.parent != null)
            {
                gameObject.transform.parent.parent.GetComponentInChildren<BabyCatcher>().Release(Vector2.zero);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }

            Carry();
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.position = collision.transform.position + new Vector3(0, 0.5f, 0);

            // gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            IsGameOver = true;
            StartCoroutine(Victory());
            count++;
            // anim.SetBool("dead", false);
            // anim.SetBool("idle", false);
            // anim.SetBool("victory", true); 
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