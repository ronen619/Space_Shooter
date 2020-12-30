using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text _restartLevelText;
    [SerializeField] private Text _GameOverText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImg;

    private GameManager _gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _GameOverText.gameObject.SetActive(false); // turn off the object until i call him
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
       
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void PlayerLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartLevelText.gameObject.SetActive(true);
        _GameOverText.gameObject.SetActive(true); // bring the game over text
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
       
    }
    
}

