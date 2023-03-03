using System.Threading.Tasks;
using UnityEngine;

public class LevelTransitionTile : Tile
{
    public override void Activate()
    {
        SceneLoader loader = new SceneLoader();
        loader.LoadNextLevel();
    }

    public override void Deactivate() { }
}
