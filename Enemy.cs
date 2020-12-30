using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Player _player; 

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bootom os screen respawn a new random x position 
        if (transform.position.y < -5f )
        {
            float xPos = Random.Range(8.0f, -8.0f); // the enemy will apper in diffrent location after he goes dowm
            transform.position = new Vector3(xPos, 7, 0);
        }
    }

    // if other is player 
    // damage the player and destroy us
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }

        // if other is laser
        //destory laser and then us
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10); // this the point int, inthe player script
            }
            
            Destroy(this.gameObject);
        }
    }



}
