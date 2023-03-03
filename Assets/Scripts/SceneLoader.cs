using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly static int MenuSceneIndex = 0;
    private readonly static int StagesSceneIndex = 1;
    private readonly static int LevelSceneIndex = 2;

    public void LoadNextLevel()
    {
        ProgressManager progress = new ProgressManager(LevelTransferedData.StageIndex);
        try
        {
            progress.SetCurrentLevelToNext();
            LevelTransferedData.LevelIndex++;
            LoadLevel();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.Message);
            LoadStages();
        }
    }

    public void LoadLastLevel(int stageIndex)
    {
        ProgressManager progress = new ProgressManager(stageIndex);
        try
        {
            LevelTransferedData.StageIndex = stageIndex;
            LevelTransferedData.LevelIndex = progress.GetCurrentLevel();
            LoadLevel();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelSceneIndex);
    }
    public void LoadStages()
    {
        SceneManager.LoadScene(StagesSceneIndex);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(MenuSceneIndex);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
