using System;
using UnityEngine;

public class ProgressManager
{
    public static readonly int FirstLevelIndex = 1;
    public static readonly string CurrentLevelKey = "Stage{0}CurrentLevel";

    public static readonly int[] StageLevelsCount = { 3, 0 };

    private int _stageIndex;
    private string _key;

    public ProgressManager(int stageIndex)
    {
        _stageIndex = stageIndex;
        _key = string.Format(CurrentLevelKey, _stageIndex.ToString());
    }

    public int GetCurrentLevel()
    {
        int index = FirstLevelIndex;

        if (PlayerPrefs.HasKey(_key))
        {
            index = PlayerPrefs.GetInt(_key);
            if (index > StageLevelsCount[_stageIndex - 1])
            {
                throw new NullReferenceException("End of the stage with index " + index.ToString());
            }
        }
        else
        {
            SetDefault();
        }

        return index;
    }

    public void SetCurrentLevelToNext()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            if (PlayerPrefs.GetInt(_key) >= StageLevelsCount[_stageIndex - 1])
            {
                PlayerPrefs.SetInt(_key, StageLevelsCount[_stageIndex - 1] + 1);
                throw new NullReferenceException("End of the stage (next) with index " + PlayerPrefs.GetInt(_key).ToString());
            }
            else
            {
                PlayerPrefs.SetInt(_key, PlayerPrefs.GetInt(_key) + 1);
            }
        }
        else
        {
            SetDefault();
        }
    }

    public void ResetCurrentLevel()
    {
        SetDefault();
    }

    private void SetDefault()
    {
        PlayerPrefs.SetInt(_key, FirstLevelIndex);
    }

   
}
