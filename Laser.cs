using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // if laser go grater then 10 destroy
        if (transform.position.y >= 10 )
        {
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject); // destroy the parent object for triple shots 
        }
            Destroy(gameObject); // destroy only one laser
        }
    }
}
