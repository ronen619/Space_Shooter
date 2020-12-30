using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _Score;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float _speedBoost = 2.0f;
    [SerializeField] private float _fireRate = 0.5f;

    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private UiManager _uiManager;

    public GameObject _laserPrefab;
    public GameObject _tripleShotPrefab;
    public GameObject _ShieldPrefab;
  

    [SerializeField] public bool _isTripleShotActive = false;
    [SerializeField] public bool _isSpeedBoostActive = false;
    [SerializeField] public bool _isShieldActive = false;
    [SerializeField] public bool _isPlayerDie = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The SpawnMananger is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
     
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0); // the new vector for better script

            transform.Translate(direction * speed * Time.deltaTime);

        // get to y = 0 he cant go up
        // eles if go down to -3.38f he cant fo downer
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.38f)
        {
            transform.position = new Vector3(transform.position.x, -3.38f, 0);

            // transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.38f, 0), 0);
        }

        // position x is 11
        // x pos = -11
        // eles if player on the x is less then -11 
        // x pos = 11

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        Vector3 offset = new Vector3(0, 1.05f, 0); // the space between the laser and player

        _canFire = Time.time + _fireRate; // Time.time = 1 and fire rate is = 0.5 then he can fire
        

        // if space key press and bool is true fire TripleShot
        // else fire 1 laser
        if ( _isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }

    }
    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _ShieldPrefab.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.PlayerLives(_lives);

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        
        }
    }
    

     public void TripleShotActive()
    {
            _isTripleShotActive = true;
        
            StartCoroutine(TripleShotPowerDownRoutine());
        
    }    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);

        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        speed *= _speedBoost;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedBoostActive = false;            
        speed /= _speedBoost;
    }

    public void ShieldActive()
    {

        _isShieldActive = true;
        _ShieldPrefab.SetActive(true);
        
    }
    public void AddScore(int points)
    {
        _Score += points;
        // commuinicate with UI to update score
        _uiManager.UpdateScore(_Score);
    }

    public void PlayerDie()
    {
        _isPlayerDie = true;
    }
}

