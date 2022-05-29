using UnityEngine;
using TMPro;
using System.IO;

public class StageInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI stageName;
    [SerializeField]
    private TextMeshProUGUI stageDate;

    private void Update()
    {
        if (gameObject.name.Contains("Clone"))
        {
            return;
        }
        else
        {
            reName();
        }
    }

    public void reName()
    {
        stageName.text = gameObject.name;

        string fileName = Path.Combine(Application.streamingAssetsPath, stageName.text + ".json");
        var info = new FileInfo(fileName);

        stageDate.text = "" + info.LastAccessTime;
    }
}
