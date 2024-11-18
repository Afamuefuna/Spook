using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : UIView
{
    [SerializeField] Button levelButton, bonusButton, settingsButton;

    public override void Show(){
        base.Show();
    }

    public override void Hide(){
        base.Hide();
    }

    void Start()
    {
        uIManager = FindAnyObjectByType<UIManager>();

        levelButton.onClick.AddListener(() => {
            uIManager.Transition(uIManager.levelView);
        });

        bonusButton.onClick.AddListener(() => {
            uIManager.Transition(uIManager.bonusView);
        });

        settingsButton.onClick.AddListener(() => {
            uIManager.Transition(uIManager.settingsView);
        });
    }
}
