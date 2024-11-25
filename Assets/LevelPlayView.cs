using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlayView : UIView
{
    public override void Show()
    {
        base.Show();

        AudioManager.Instance.PlayMusic(BackgroundMusic.PLAY_MUSIC);
    }
}
