using UnityEngine;

public class GameProgress
{
    public GameProgress(LevelData data)
    {
        _activatedBlocksCount = 0;

        for (int i = 0; i < data.FieldHeight; i++)
        {
            for (int j = 0; j < data.FieldWidth; j++)
            {
                if (data.Field[i, j] != 0)
                {
                    _blocksCount++;
                }
            }
        }
    }

    #region EVENTS
    public delegate void OnLevelStartedEventHandler();
    public  event OnLevelStartedEventHandler OnLevelStarted;

    public delegate void OnLevelReloadedEventHandler();
    public event OnLevelReloadedEventHandler OnLevelReloaded;

    public delegate void OnLevelFinishedEventHandler();
    public  event OnLevelFinishedEventHandler OnLevelFinished;

    public delegate void OnProgressChangedEventHandler(int currentScore, int targetScore);
    public event OnProgressChangedEventHandler OnProgressChanged;
    #endregion

    private int _activatedBlocksCount;
    private int _blocksCount;

    public void ActivateNewBlock()
    {
        _activatedBlocksCount++;
        OnProgressChanged?.Invoke(_activatedBlocksCount, _blocksCount);

        if (_activatedBlocksCount == _blocksCount)
        {
            FinishLevel();
        }
    }
   
    public void StartLevel()
    {
        OnLevelStarted?.Invoke();
    }

    public void Reload()
    {
        _activatedBlocksCount = 1;
        OnProgressChanged?.Invoke(_activatedBlocksCount, _blocksCount);
        OnLevelReloaded?.Invoke();
    }

    private void FinishLevel()
    {
        OnLevelFinished?.Invoke();
    }
}
