using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void GoToIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void GoScene(string name)
    {
        if (gameObject.name.Contains("Stage"))
        {
            string stageName = gameObject.name;
            int stageNum = int.Parse(stageName.Replace("Stage", ""));
            Debug.Log(stageNum);
            PlayerPrefs.SetInt("StageIndex", stageNum-1);
        }
        SceneLoader.LoadScene(name);
    }
}
