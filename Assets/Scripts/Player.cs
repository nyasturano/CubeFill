using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region EVENTS
    public delegate void OnMoveEndedEventHandler();
    public event OnMoveEndedEventHandler OnMoveEnded;
    #endregion

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float speed;
    
    private Vector3 _targetPosition;

    private Vector2Int _startPosition;
    
    public Vector2Int LastDirection { get; private set; }
    public Vector2Int Coordinates { get; private set; }
    
    public bool IsMoving { get; private set; }

    private Func<Vector2Int, Vector2Int, int> CalculatePath;

    public void InitWithPosition(Func<Vector2Int, Vector2Int, int> calculatePath, 
        Vector2Int spawnPosition, Vector2Int startPosition)
    {
        CalculatePath = calculatePath;
        _startPosition = startPosition;
        
        ForceMove(spawnPosition);
        SetTarget(startPosition);
    
    }

    public void InitWithDirection(Func<Vector2Int, Vector2Int, int> calculatePath, 
        Vector2Int spawnPosition, Vector2Int direction)
    {
        CalculatePath = calculatePath;
        _startPosition = spawnPosition;
        
        ForceMove(spawnPosition);
        TryMove(direction);
    }

    private void ForceMove(Vector2Int target)
    {
        transform.localPosition = new Vector3(target.x, 0f, target.y);
        _targetPosition = transform.localPosition;
        Coordinates = target;
    }

    private void SetTarget(Vector2Int target)
    {
        CalculateDirection(target);

        _targetPosition = new Vector3(target.x, 0f, target.y);
        Coordinates = target;
    }
    
    private void SetTargetByOffset(Vector2Int offset)
    {
        _targetPosition = transform.localPosition + new Vector3(offset.x, 0f, offset.y);
        Coordinates += offset;
    }

    private void CalculateDirection(Vector2Int target)
    {
        Vector2 direction = target - Coordinates;
        direction.Normalize();
        LastDirection = new Vector2Int((int)direction.x, (int)direction.y);
    }

    public void ReturnToStart()
    {
        ForceMove(_startPosition);
    }

    public void TryMove(Vector2Int direction)
    {
        if (!IsMoving)
        {
            LastDirection = direction;
            SetTargetByOffset(direction * CalculatePath(Coordinates, direction));
        }
    }

    private void Update()
    {
        if (transform.localPosition != _targetPosition)
        {
            IsMoving = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, speed * Time.deltaTime);
        }
        else
        {
            if (IsMoving)
            {
                OnMoveEnded?.Invoke();
                particle.Play();
            }
            IsMoving = false;
        }
    }
}
