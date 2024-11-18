using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : UIView
{
     public Button closeButton;
     public List<LevelUI> levelUIs;
     public LevelManager levelManager;

    public override void Show()
    {
        base.Show();

        for (int i = 0; i < levelUIs.Count; i++){
            levelUIs[i].levelButton.interactable = false;
            levelUIs[i].levelNumberText.text = levelManager.levels[i].levelNumber.ToString();
            levelUIs[i].levelManager = levelManager;
            levelUIs[i].levelNumber = levelManager.levels[i].levelNumber;
            levelUIs[i].uIManager = uIManager;

            if(i == 0){
                levelUIs[i].levelButton.interactable = true;
            }
            if(levelManager.levels[i].hasCompleted){
                levelUIs[i].levelButton.interactable = true;

                if(levelManager.levels[i].levelScore == 3){
                    levelUIs[i].stars[0].gameObject.SetActive(true);
                    levelUIs[i].stars[1].gameObject.SetActive(true);
                    levelUIs[i].stars[2].gameObject.SetActive(true);
                }else if(levelManager.levels[i].levelScore == 2){
                    levelUIs[i].stars[0].gameObject.SetActive(true);
                    levelUIs[i].stars[1].gameObject.SetActive(true);
                    levelUIs[i].stars[2].gameObject.SetActive(false);
                }else if(levelManager.levels[i].levelScore == 1){
                    levelUIs[i].stars[0].gameObject.SetActive(true);
                    levelUIs[i].stars[1].gameObject.SetActive(false);
                    levelUIs[i].stars[2].gameObject.SetActive(false);
                }
            }else{
                levelUIs[i].stars[0].gameObject.SetActive(false);
                levelUIs[i].stars[1].gameObject.SetActive(false);
                levelUIs[i].stars[2].gameObject.SetActive(false);
            }

            if(levelManager.currentLevel == levelManager.levels[i].levelNumber){
                levelUIs[i].levelButton.interactable = true;
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    void Start()
    {
        uIManager = FindAnyObjectByType<UIManager>();

        closeButton.onClick.AddListener(()=>
        {
            uIManager.Transition(uIManager.menuView);
        });
    }
}
