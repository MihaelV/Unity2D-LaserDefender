using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 150;
    public GameObject enemyLaser;
    public float shotSpeed = 10f;
    public float shotsPerSecond = 0.5f;

    public int scoreValue = 150;
    private ScoreKeeper scoreKepeer;

    public AudioClip fireSound;
    public AudioClip deathSound;


    private void Start()
    {
        scoreKepeer = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider);
        Laser laser = collider.gameObject.GetComponent<Laser>();        
        if (laser)
        {
            health -= laser.GetDamange();
            laser.Hit();
            if(health <= 0)
            {
                Die();
            }

           
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKepeer.Score(scoreValue);
    }


    void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;
        if (probability > Random.value)
        {
            Fire();
        }
       
    }


    void Fire()
    {
        //Vector3 startPosition = transform.position + new Vector3(0, -1f, 0);
        //GameObject danger = Instantiate(enemyLaser, startPosition, Quaternion.identity) as GameObject;
        // ovo nam vise ne treba jer smo time odmaknuli startnu poziciju rakete koju smo ispucali je smo u unity-u slozili da nam unity neocitava dodire izmedu playera i njegove rakete, enemya i njegove rakete i nase rakete i enemy rakete

        GameObject danger = Instantiate(enemyLaser, transform.position, Quaternion.identity) as GameObject;
        danger.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -shotSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
