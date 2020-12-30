using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Player _player;
    private Animator _anim;
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL");

        }
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animation is Null");
        }
        
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f) ;
        }
    }



}
