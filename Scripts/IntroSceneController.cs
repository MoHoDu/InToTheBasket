using UnityEngine;
using System.IO;

public class IntroSceneController : MonoBehaviour
{
    [SerializeField]
    private SelectTwinkleEffect     selectEffect;

    private void Awake()
    {
        // 현재 게임 윈도우가 포커스 되어 있지 않아도 게임이 실행되도록 설정
        Application.runInBackground = true;

        // 게임을 새로 시작했을 때 항상 스테이지가 1부터 시작하도록 설정
        PlayerPrefs.SetInt("StageIndex", 0);

        // StreamingAssets 폴더 정보를 열어온다
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        // GetFile()로 폴더에 있는 모든 파일 정보를 얻어온다
        // 맵 데이터 .json 파일과 .meta 파일이 있기 때문에 /2를 한 값이 맵 데이터 개수
        StageController.maxStageCount = directory.GetFiles().Length / 2;
    }

    private void Update()
    {
        //    // 아무 키나 누르면
        //    if (Input.anyKeyDown)
        //    {
        //        // "Stage" 씬으로 씬 변경
        //        SceneLoader.LoadScene("Stage");
        //    }

        // 스페이스바 키를 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 0번 메뉴를 선택하면 
            if (selectEffect.selectNum == 0)
            {
                SceneLoader.LoadScene("Stage");
            }
            // 1번 메뉴를 선택하면 
            else if (selectEffect.selectNum == 1)
            {
                SceneLoader.LoadScene("Load");
            }
            // 2번 메뉴를 선택하면 
            else if (selectEffect.selectNum == 2)
            {
                SceneLoader.LoadScene();
            }
            // 3번 메뉴를 선택하면 
            else if (selectEffect.selectNum == 3)
            {
                SceneLoader.LoadScene("SandBox");
            }
            // 마지막 메뉴를 선택하면 
            else
            {
                Application.Quit();
            }
        }
    }
}
