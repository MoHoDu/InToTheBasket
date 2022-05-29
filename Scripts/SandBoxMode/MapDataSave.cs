using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using System;

public class MapDataSave : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI     FileName;
    [SerializeField]
    private TMP_InputField      textMaxGoal;
    [SerializeField]
    private Tilemap2D_Edit      tilemap2D;

    private int                 maxGoal;

    private void Awake()
    {
        // StreamingAssets 폴더 정보를 열어온다
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        // GetFile()로 폴더에 있는 모든 파일 정보를 얻어온다 --> stages의 크기로 설정한다 
        // 맵 데이터 .json 파일과 .meta 파일이 있기 때문에 /2를 한 값이 맵 데이터 개수
        if (directory.GetFiles().Length / 2 < 9)
        {
            int StageNum = (directory.GetFiles().Length / 2) + 1;
            FileName.text = "Stage0" + StageNum;
        }
        else
        {
            int StageNum = (directory.GetFiles().Length / 2) + 1;
            FileName.text = "Stage" + StageNum;
        }
    }

    public void Save()
    {
        string maxGoalText = textMaxGoal.text;
        try
        {
            maxGoal = int.Parse(maxGoalText);
        }
        catch (FormatException ie)
        {
            Debug.Log(ie);
            textMaxGoal.text = "Error";
            return;
        }

        // tileMap2D에 저장되어 있는 MapData 정보를 불러온다.
        // 맵 크기, 플레이어 캐릭터 위치, 맵에 존재하는 타일들의 정보
        MapData_Edit mapdata = tilemap2D.GetMapData(maxGoal);

        // inputField UI에 입력된 텍스트 정보를 불러와서 file Name에 저장
        string fileName = FileName.text + ".json";

        // fileName에 ".json" 문장이 없으면 입력해준다..
        // ex) "Stage01" => "Stage01.json"
        //if ( fileName.Contains(".json") == false )
        //{
        //    fileName += ".json";
        //}

        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        // 현재 프로젝트 위치 기준으로 "MapData" 폴더
        fileName = Path.Combine("Assets/StreamingAssets/", fileName);

        // mapData 인스턴스에 있는 내용을 직렬화해서 toJson 변수에 문자열 형태로 저장
        string toJson = JsonConvert.SerializeObject(mapdata, Formatting.Indented);

        // "fileName" 파일에 "toJson" 내용을 저장
        File.WriteAllText(fileName, toJson);
    }

}
