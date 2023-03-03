using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartStage : MonoBehaviour
{
    [SerializeField] private int stageIndex;

    public void Restart()
    {
        ProgressManager progress = new ProgressManager(stageIndex);
        progress.ResetCurrentLevel();
    }
}
