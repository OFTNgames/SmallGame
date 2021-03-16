using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public List<Level> levelsList = new List<Level>();
    [SerializeField] private int _numberOfLevels;
    void Start()
    {
        for (int i = 1; i <= _numberOfLevels; i++)
        {
            levelsList.Add(new Level(i));
            
            if (levelsList[i].id == 1)
                levelsList[i].unlocked = true;
            else
                levelsList[i].unlocked = PlayerPrefs.GetInt(levelsList[i].id.ToString() + "unlocked") != 0;
            
            levelsList[i].complete = PlayerPrefs.GetInt(levelsList[i].id.ToString() + "complete") != 0;
            levelsList[i].tracking = PlayerPrefs.GetInt(levelsList[i].id.ToString() + "tracking") != 0;
        }
    }
}
