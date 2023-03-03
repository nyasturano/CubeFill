using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private SceneLoader loader = new SceneLoader();
    public void ToMenu()
    {
        loader.LoadMenu();
    }
    public void ToStages()
    {
        loader.LoadStages();
    }
}
