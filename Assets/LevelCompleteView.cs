using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteView : UIView
{
    public Image[] stars;
    public TMP_Text scoreText;
    public TMP_Text resultText;
    public Button nextButton;
    public Button retryButton;
    public Button mainMenuButton;
    public LevelManager levelManager;
    
    public override void Show()
    {
        base.Show();
    }

    public override void Hide(){
        base.Hide();
    }

    void Start(){
        uIManager = FindAnyObjectByType<UIManager>();
        mainMenuButton.onClick.AddListener(() => {
            uIManager.Transition(uIManager.menuView);
        });

        retryButton.onClick.AddListener(() => {
            GameManager.Instance.RetryLevel(levelManager, levelManager.currentLevel + 1);
        });
    }

}
