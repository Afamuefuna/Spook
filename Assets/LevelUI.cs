using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public TMP_Text levelNumberText;
    public Image[] stars;
    public Button levelButton;
    public LevelManager levelManager;
    public int levelNumber;
    public UIManager uIManager;

    public void Start(){
        levelButton.onClick.AddListener(() => {
            Debug.Log("Level Button Clicked");

            OnClick(levelManager, levelNumber);
        });
    }

    public void OnClick(LevelManager levelManager, int levelNumber){
            GameManager.Instance.RetryLevel(levelManager, levelNumber);
    }
}
