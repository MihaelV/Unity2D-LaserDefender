using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;

    public float width = 10f;
    public float height = 5f;
    private bool movingRight = false;
    public float speed = 10f;

    private float xmax;
    private float xmin;

    public float SpawnDelay = 0.5f;
    


	// Use this for initialization
	void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightEdge.x;
        xmin = leftEdge.x;


        //SpawnEnemies();
        SpawnUntilFull();
	}

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject; // instanciramo nove neprijatelje koji ce se pojaviti na poziciji roditelja
            enemy.transform.parent = child; // služi za stvaranje formacije neprijatelja unutar njhovog roditelja koji je ubiti Gizmo kojeg smo poslozili kao formaciju
        }
    }


    void SpawnUntilFull() // ova funkcija spawna enemy-e jedan po jedan na prvoj slobodnoj poziciji 
    {
        Transform FreePosition = nextFreePosition();
        //provjera ako postoji prazno mjesto
        if (FreePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, FreePosition.transform.position, Quaternion.identity) as GameObject; // instanciramo samo jednog neprijatelja ako postoji prazno mijesto
            enemy.transform.parent = FreePosition; // služi za stvaranje formacije neprijatelja unutar njhovog roditelja koji je ubiti Gizmo kojeg smo poslozili kao formaciju
        }

        if (nextFreePosition())
        {
            Invoke("SpawnUntilFull", SpawnDelay); //kašnjenje prije stvaranja svakog enemy-a
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width,height));
    }


    // Update is called once per frame
    void Update(){
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;            
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if(leftEdgeOfFormation < xmin ) 
        {
            movingRight = true;
        }
        else if(rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            Debug.Log("Empty Formation");
            //SpawnEnemies();
            //spawn new enemies

            SpawnUntilFull(); 


        }
    }

    bool AllMembersDead()
    {
        
        foreach(Transform childPositionGameObject in transform) // kroz transform dobivamo djecu
        {
            if(childPositionGameObject.childCount > 0) // brojimo koliko ima djece (neprijatelja)
            {
                return false; //every member is not dead
            }
        }      
        return true;
        //every member is dead     
        
    }

    Transform nextFreePosition()
    {
        
        foreach(Transform empty in transform)
        {
            if(empty.childCount == 0)
            {
                return empty;
            }
        }
        return null;
    }

   
}
