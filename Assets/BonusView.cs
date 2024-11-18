using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BonusView : UIView
{
    public Button closeButton;

    public override void Show()
    {
        base.Show();
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
