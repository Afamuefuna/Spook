using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public int currentLevel;
    public List<GameObject> levelEnvironments;
    public GameObject levelParent;
    public SpriteRenderer backgroundSpriteRenderer;

    void Start()
    {
        InstantiateLevel();
    }

    public void InstantiateLevel()
    {
        foreach(Transform child in levelParent.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(levelEnvironments[currentLevel], levelParent.transform);

        backgroundSpriteRenderer.sprite = levels[currentLevel].backgroundSprite;
    }
}

[System.Serializable]
public class Level
{
    public int levelNumber;
    public float levelScore;
    public int maxScore;
    public int passScore;
    public bool hasCompleted;
    public bool isLocked;
    public Vector3 levelStartPosition;
    public Sprite backgroundSprite;
}
