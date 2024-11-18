
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   public UIView levelView, menuView, bonusView, settingsView, levelPlayView, levelCompleteView;
   public UIView currentView;

   public void Transition(UIView uIView)
   {
      if(currentView != null){
         currentView.Hide();
      }
      uIView.Show();
      currentView = uIView;
   }

   public void Start(){
      Transition(menuView);
   }
}
