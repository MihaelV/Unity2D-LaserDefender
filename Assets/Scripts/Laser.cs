using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float damange = 100f;


    public float GetDamange()
    {
         return damange;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
