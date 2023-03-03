using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private int stageIndex;
    public void StartLastLevel()
    {
        SceneLoader loader = new SceneLoader();
        loader.LoadLastLevel(stageIndex);
    }
}
