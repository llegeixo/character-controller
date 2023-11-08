using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    
    int vida = 5;

    // Start is called before the first frame update
    public void TakeDamage(int damage)
    {
        vida -= damage;

        if(vida <=0)
        {
            Destroy(this.gameObject);
        }
    }

}
