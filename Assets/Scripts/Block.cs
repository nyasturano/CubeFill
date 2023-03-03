using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _raised = true;

    public void MoveDown()
    {
        if (_raised)
        {
            animator.SetTrigger("Down");
            _raised = false;
        }
    }

    public void MoveUp()
    {
        if (!_raised)
        {
            animator.SetTrigger("Up");
            _raised = true;
        }
    }
}
