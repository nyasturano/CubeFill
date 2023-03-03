using TMPro;
using UnityEngine;

public class ProgressText : MonoBehaviour
{
    [SerializeField] private TMP_Text progressText;

    public void UpdateText(int currentScore, int targetScore)
    {
        progressText.text = currentScore.ToString() + "/" + targetScore.ToString();
    }
}
