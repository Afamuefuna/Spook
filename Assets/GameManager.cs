using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentHealth;
    public int maxHealth;
    int score = 0;
    public TMP_Text scoreText, healthText;
    public GameObject pauseMenu;
    public Button pauseButton, resumeButton;
    public UIManager uIManager;
    public LevelManager levelManager;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Debug.Log("Game Paused");
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Debug.Log("Game Resumed");
    }

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();

        UpdateScoreDisplay();
        UpdateHealthDisplay();

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    public void CollectCoin()
    {
        score++;
        UpdateScoreDisplay();
    }

    public void SetScore(int score){
        this.score = score;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void UpdateHealthDisplay(){
        healthText.text = currentHealth.ToString();
    }

    public void RetryLevel(LevelManager levelManager, int levelNumber){
            foreach(GameObject env in levelManager.levelEnvironments){
                env.SetActive(false);
            }
            
            GameManager.Instance.SetScore(0);
            
            levelManager.levelEnvironments[levelNumber - 1].SetActive(true);
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            playerController.rb.bodyType = RigidbodyType2D.Dynamic;
            playerController.transform.position = levelManager.levels[levelNumber - 1].levelStartPosition;
            playerController.restartPoint = levelManager.levels[levelNumber - 1].levelStartPosition;
            levelManager.InstantiateLevel();
            uIManager.Transition(uIManager.levelPlayView);
        ;
    }

    public void CompleteLevel(){
        Debug.Log("Level Completed");

        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.rb.bodyType = RigidbodyType2D.Static;
        
        uIManager.Transition(uIManager.levelCompleteView);
        LevelCompleteView levelCompleteView = uIManager.levelCompleteView.GetComponent<LevelCompleteView>();
        levelCompleteView.scoreText.text = "Score: " + score.ToString();

        levelManager.levels[levelManager.currentLevel].levelScore = score;

        levelManager.levels[levelManager.currentLevel].levelScore = 
        
        levelManager.levels[levelManager.currentLevel].levelScore
        /levelManager.levels[levelManager.currentLevel].maxScore;

        levelManager.levels[levelManager.currentLevel].levelScore =
        Mathf.RoundToInt(levelManager.levels[levelManager.currentLevel].levelScore * 3);

        levelManager.levels[levelManager.currentLevel].passScore = levelManager.levels[levelManager.currentLevel].maxScore/2;

        if(levelManager.levels[levelManager.currentLevel].levelScore >= 1){
            levelCompleteView.resultText.text = "VICTORY";
            levelManager.levels[levelManager.currentLevel].hasCompleted = true;
            
            if (levelManager.currentLevel + 1 < levelManager.levels.Count)
            {
                levelManager.levels[levelManager.currentLevel + 1].isLocked = false;
                levelManager.currentLevel++;
            }
        }else{
            levelCompleteView.resultText.text = "GAME OVER";
            levelManager.levels[levelManager.currentLevel].hasCompleted = false;
        }

        if(levelManager.levels[levelManager.currentLevel].hasCompleted){

            levelCompleteView.nextButton.gameObject.SetActive(true);
                    levelCompleteView.retryButton.gameObject.SetActive(false);
        
                if(levelManager.levels[levelManager.currentLevel].levelScore >= 3){
                    levelCompleteView.stars[0].gameObject.SetActive(true);
                    levelCompleteView.stars[1].gameObject.SetActive(true);
                    levelCompleteView.stars[2].gameObject.SetActive(true);


                }else if(levelManager.levels[levelManager.currentLevel].levelScore == 2){
                    levelCompleteView.stars[0].gameObject.SetActive(true);
                    levelCompleteView.stars[1].gameObject.SetActive(true);
                    levelCompleteView.stars[2].gameObject.SetActive(false);


                    
                }else if(levelManager.levels[levelManager.currentLevel].levelScore == 1){
                    levelCompleteView.stars[0].gameObject.SetActive(true);
                    levelCompleteView.stars[1].gameObject.SetActive(false);
                    levelCompleteView.stars[2].gameObject.SetActive(false);
                }
            }
            else{
                levelCompleteView.stars[0].gameObject.SetActive(false);
                levelCompleteView.stars[1].gameObject.SetActive(false);
                levelCompleteView.stars[2].gameObject.SetActive(false);

                levelCompleteView.nextButton.gameObject.SetActive(false);
                    levelCompleteView.retryButton.gameObject.SetActive(true);
            }
    }
}