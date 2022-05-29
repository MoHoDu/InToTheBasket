using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollView;
    [SerializeField]
    private GameObject stagePrefab;                 // 스테이지 정보 프리팹
    [SerializeField]
    private ScrollRectPosition scrollRectPosition;  // 스크롤 사에 지정 스테이지 버튼 정보 가져오기 위한 변수 

    private GameObject[] stages;                    // 스테이지 정보 기입 

    private void Awake()
    {
        // StreamingAssets 폴더 정보를 열어온다
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        // GetFile()로 폴더에 있는 모든 파일 정보를 얻어온다 --> stages의 크기로 설정한다 
        // 맵 데이터 .json 파일과 .meta 파일이 있기 때문에 /2를 한 값이 맵 데이터 개수
        stages = new GameObject[directory.GetFiles().Length / 2];
        for (int i = 0; i < StageController.maxStageCount; i++)
        {
            RectTransform viewPortObject = scrollView.viewport;
            GameObject clone = Instantiate(stagePrefab, viewPortObject.GetChild(0).transform);
            clone.name = i + 1 < 10 ? $"Stage0{i+1}" : $"Stage{i+1}";

            stages[i] = clone;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            int stageNum = scrollRectPosition.selectedNum;
            PlayerPrefs.SetInt("StageIndex", stageNum);
            SceneLoader.LoadScene("Stage");
        }
    }
}
