using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float padding = 1f;
    float xmin;
    float xmax;
    public float laserSpeed;

    public GameObject laser;

    public float firingRate = 0.2f;


    public float health = 250f;


    public AudioClip fireSound;
    private Zivot zivot;
    private int ziv = 1;


    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z; // udaljenost kamere od objekta
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distance));
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;

        zivot = GameObject.Find("Zivot").GetComponent<Zivot>();
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);                    
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

      
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("left");
            //transform.position += new Vector3(-speed * Time.deltaTime,0,0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            print("right");
            //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // ograniči kretanje playera na scenu
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y,transform.position.z);
    }


    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 0.7f, 0); // udaljavamo pocetak rakete po y osi za 1, jer ako bi pocela raketa ici s iste pozicije kao i player unity bi registirao kao da nas je raketa pogodila i svaki put kad bi zapucali uzeo bi nam health
        GameObject shothim = Instantiate(laser, startPosition, Quaternion.identity);
        shothim.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        //Debug.Log(collider);
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser)
        {
            health -= laser.GetDamange();
            laser.Hit();
            
            if (health <= 0)
            {
                Die();
            }
            zivot.BrojiloZivota(ziv);
            
        }
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win Screen");
        Destroy(gameObject);
    }

    

}
