using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public enum ObjectType
    {
        Block,
        ColorTile,
        SpawnTile,
        SimpleTile,
        Transition
    }

    private GameProgress _gameProgress;
    private LevelState _gameState;
    
    // layers
    [SerializeField] private List<Transform> layers;

    // prefabs
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject colorTilePrefab;
    [SerializeField] private GameObject spawnTilePrefab;
    [SerializeField] private GameObject simpleTilePrefab;
    [SerializeField] private GameObject transitionTilePrefab;


    public void Init(LevelState state, GameProgress progress)
    {
        _gameProgress = progress;
        _gameState = state;
    }

    public void Spawn(int objectId, Vector2Int coordinates)
    {
        ObjectType type = (ObjectType)objectId;
        Spawn(type, coordinates);
    }

    public void Spawn(ObjectType objectType, Vector2Int coordinates)
    {
        Vector3 position = CoordinatesToVector(coordinates);
        
        switch (objectType)
        {
            case ObjectType.ColorTile:
                ColorTile tile = InstantiateObject(colorTilePrefab, layers[0], position).GetComponent<ColorTile>();
                _gameProgress.OnLevelStarted += tile.EnableTile; // activates when level starts
                _gameProgress.OnLevelReloaded += tile.DeactivateTile; // deactivates when level reloads
                tile.OnActivated += _gameProgress.ActivateNewBlock; // update progress when activated
                break;

            case ObjectType.Block:
                InstantiateObject(simpleTilePrefab, layers[0], position); // simple tile under the block
                Block block = InstantiateObject(blockPrefab, layers[1], position).GetComponent<Block>();
                _gameState.AddBlock(coordinates, block); // track this block
                break;

            case ObjectType.SpawnTile:
                InstantiateObject(spawnTilePrefab, layers[0], position);
                break;

            case ObjectType.SimpleTile:
                InstantiateObject(simpleTilePrefab, layers[0], position);
                break;

            case ObjectType.Transition:
                InstantiateObject(transitionTilePrefab, layers[0], position);
                break;
        }

    }
    
    public Player SpawnPlayer()
    {
        GameObject playerObj = InstantiateObject(player, layers[1], Vector3.zero);
        return playerObj.GetComponent<Player>();
    }

    private GameObject InstantiateObject(GameObject prefab, Transform parent, Vector3 position)
    {
        GameObject gameObject = Instantiate(prefab, parent, false);
        gameObject.transform.localPosition = position;
        return gameObject;
    }
    private Vector3 CoordinatesToVector(Vector2Int coordinates)
    {
        return new Vector3(coordinates.x, 0f, coordinates.y);
    }
}
