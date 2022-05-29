using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI     textStage;
    [SerializeField]
    private TextMeshProUGUI     textCoinCount;
    [SerializeField]
    private TextMeshProUGUI     textGoalCount;

    public void UpdateTextStage(string stageName)
    {
        textStage.text = stageName;
    }

    public void UpdateCoinCount(int current, int max)
    {
        textCoinCount.text = $"Coin {current}/{max}";
    }

    public void UpdateGoalCount(int current, int max)
    {
        textGoalCount.text = $"Goal {current}/{max}";
    }
}
