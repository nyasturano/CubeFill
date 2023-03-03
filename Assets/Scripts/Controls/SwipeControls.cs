using System.Collections;
using UnityEngine;

public class SwipeControls : PlayerControls
{
    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    [SerializeField] private float _minimumDistance = .2f;
    [SerializeField] private float _maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float _verticalTreshold = .9f;
    [SerializeField, Range(0f, 1f)] private float _horizontalTreshold = .9f;

 
    public SwipeControls()
    {
        OnTouchStarted += ctx => TouchStart(ctx);
        OnTouchEnded += ctx => TouchCancel(ctx);
    }

    public void TouchStart(InputContext context)
    {
        _startPosition = context.Position;
        _startTime = context.Time;
    }

    public void TouchCancel(InputContext context)
    {
        _endPosition = context.Position;
        _endTime = context.Time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(_startPosition, _endPosition) >= _minimumDistance &&
            _endTime - _startTime <= _maximumTime)
        {
            Vector2 direction = (_endPosition - _startPosition).normalized;
            SwipeDetection(direction);
        }
    }

    private void SwipeDetection(Vector2 direction)
    {
        if (Vector2.Dot(direction, Vector2.up) > _verticalTreshold)
        {
            Player.TryMove(Vector2Int.down);
            //Debug.Log("Up");
        }
        else if (Vector2.Dot(direction, Vector2.down) > _verticalTreshold)
        {
            Player.TryMove(Vector2Int.up);
            //Debug.Log("Down");
        }
        else if (Vector2.Dot(direction, Vector2.right) > _horizontalTreshold)
        {
            Player.TryMove(Vector2Int.right);
            //Debug.Log("Right");

        }
        else if (Vector2.Dot(direction, Vector2.left) > _horizontalTreshold)
        {
            Player.TryMove(Vector2Int.left);
            //Debug.Log("Left");
        }

    }
}