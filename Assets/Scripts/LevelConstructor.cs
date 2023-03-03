using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelConstructor : MonoBehaviour
{
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private ProgressText progressText;
    [SerializeField] private Button reloadButton;
    [SerializeField] private int startDelay;

    [SerializeField] private PlayerControls _controls;
    private Player _player;
    private LevelState _levelState;
    private GameProgress _gameProgress;

    private LevelData _levelData;

    private int _sideTilesCount = 3;

    private async void Awake()
    {
        // œ–Œ¬≈–»“‹ Õ¿ NULL_DATA
        await LoadSource(string.Format("stg_{0}_lvl_{1}",
                LevelTransferedData.StageIndex.ToString(),
                LevelTransferedData.LevelIndex.ToString()));

        Build();

        await Task.Delay(startDelay);
        
        InitializePlayer();
    }

    private async Task LoadSource(string path)
    {
        AssetLoader<TextAsset> loader = new AssetLoader<TextAsset>();
        TextAsset asset = await loader.LoadAsync(path);
        if (asset == null) // level doesn't exist
        {
            SceneLoader sceneLoader = new SceneLoader();
            sceneLoader.LoadStages();
        }
        else
        {
            _levelData = new LevelData(asset.ToString());
        }
        loader.Release();
    }

    private void SetCamera()
    {
        Camera.main.transform.localPosition =
            new Vector3(_levelData.FieldWidth / 2f - .5f, 0f, .88f - _levelData.FieldHeight / 2f);
        Camera.main.orthographicSize = (_levelData.FieldWidth + (_sideTilesCount - 1) * 2) / (2 * Camera.main.aspect);
    }

    private void Build()
    {
        SetCamera();

        // calculate the additional tiles count
        //_sideTilesCount = (int)Mathf.Ceil((MAX_FIELD_WIDTH - _levelData.FieldWidth) / 2f);

        _gameProgress = new GameProgress(_levelData);
        _levelState = new LevelState(_levelData, _sideTilesCount);

        spawner.Init(_levelState, _gameProgress);

        reloadButton.onClick.AddListener(_gameProgress.Reload);

        _gameProgress.OnProgressChanged += progressText.UpdateText;
        _gameProgress.ActivateNewBlock();

        // spawn enviroment        
        for (int i = 0; i < _levelData.FieldHeight; i++)
        {
            for (int j = 0; j < _levelData.FieldWidth; j++)
            {
                spawner.Spawn(_levelData.Field[i, j], new Vector2Int(j, i));
            }
        }

        BuildEntry();
        _levelState.OpenEntry();
        _gameProgress.OnLevelStarted += _levelState.CloseEntry;
    }
  

    // creating entry additional tiles
    public void BuildEntry()
    {
        for (int k = 1; k <= _sideTilesCount + 1; k++)
        {
            spawner.Spawn(ObjectSpawner.ObjectType.SimpleTile, new Vector2Int(-k, _levelData.StartPosition.y));
        }
    }

    // creating exit additional tiles
    public void BuildExit(Vector2Int exitPosition)
    {
        for (int k = 0; k < _sideTilesCount; k++)
        {
            spawner.Spawn(ObjectSpawner.ObjectType.SimpleTile, new Vector2Int(_levelData.FieldWidth + k, exitPosition.y));
        }

        spawner.Spawn(ObjectSpawner.ObjectType.Transition, new Vector2Int(_levelData.FieldWidth + _sideTilesCount, exitPosition.y));   
    }

    private void InitializePlayer()
    {
        _player = spawner.SpawnPlayer();

        // level starts when player reaches its start position
        _player.OnMoveEnded += StartLevelRoutine;

        _gameProgress.OnLevelReloaded += _player.ReturnToStart;

        _gameProgress.OnLevelFinished += () =>
        {
            BuildExit(_player.Coordinates);
            _levelState.OpenExit(_player.Coordinates);
        };

        _player.InitWithPosition(_levelState.CalculatePath, new Vector2Int(-_sideTilesCount - 1, _levelData.StartPosition.y), _levelData.StartPosition);

        _controls.BindPlayer(_player);
    }

    public void StartLevelRoutine()
    {
        _player.OnMoveEnded -= StartLevelRoutine;
        _gameProgress.StartLevel();
    }
}