using UnityEngine;

public abstract class PlayerControls : MonoBehaviour
{

    public delegate void OnTouchStartedEventHandler(InputContext ctx);
    public delegate void OnTouchEndedEventHandler(InputContext ctx);
    public delegate void OnTouchDeltaEventHandler(InputContext ctx);

    public event OnTouchStartedEventHandler OnTouchStarted;
    public event OnTouchEndedEventHandler OnTouchEnded;
    public event OnTouchDeltaEventHandler OnTouchDelta;

    protected Player Player;

    public void BindPlayer(Player player)
    {
        Player = player;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchStarted?.Invoke(new InputContext(Time.time, touch.position));
                    break;
                case TouchPhase.Moved:
                    OnTouchDelta?.Invoke(new InputContext(Time.time, touch.position));
                    break;
                case TouchPhase.Ended:
                    OnTouchEnded?.Invoke(new InputContext(Time.time, touch.position));
                    break;
            }
        }
    }

    public class InputContext
    {
        public InputContext(float time, Vector2 position)
        {
            Time = time;
            Position = position;
        }

        public float Time { get; private set; }
        public Vector2 Position { get; private set; }
    }
}
