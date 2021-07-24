using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //configuration
    [Range(0.1f, 10f) ] [SerializeField] float gameSpeed= 1f;
    [SerializeField] int pointsPerBlockDestroyed = 27;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] private bool isAutoPlayEnabled;

    
    //state variables
    [SerializeField] int currentScore = 0;

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currentScore.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        
    }

    

    
    public void addToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text=currentScore.ToString();
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }

    
}
