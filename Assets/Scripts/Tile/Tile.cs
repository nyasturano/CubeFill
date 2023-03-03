using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] private bool _activatedOnce;
    [SerializeField] private bool _canBeDeactivated;

    private bool _isActivated;

    public delegate void OnActivatedEventHandler();
    public event OnActivatedEventHandler OnActivated;

    public delegate void OnDeactivatedEventHandler();
    public event OnDeactivatedEventHandler OnDeactivated;

    protected virtual void Awake()
    {
        OnActivated += Activate;
        OnDeactivated += Deactivate;
    }

    public abstract void Activate();
    public abstract void Deactivate();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isActivated || !_activatedOnce)
            {
                OnActivated?.Invoke();
                _isActivated = true;
            }
        }
    }

    public void DeactivateTile()
    {
        if (_isActivated && _canBeDeactivated)
        {
            OnDeactivated?.Invoke();
            _isActivated = false;
        }
    }
}
