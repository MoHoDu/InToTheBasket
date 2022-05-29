using System.Collections.Generic;
using UnityEngine;

public class Tilemap2D : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private StageUI         stageUI;

    [Header("Tile")]
    [SerializeField]
    //private GameObject      tilePrefab;
    private GameObject[]    tilePrefabs;        // 타일의 속성에 따라 프리팹도 별도 생성 / 배열에 저장
    [SerializeField]
    private Movement2D      movement2D;         // 플레이어와 타일이 부딪혔을 때 플레이어 제어를 위한 Movement2D

    [Header("Item")]
    [SerializeField]
    private GameObject      itemPrefab;

    private int             maxCoinCount = 0;       // 현재 스테이지에 존재하는 최대 코인 개수
    private int             currentCoinCount = 0;   // 현재 스테이지에 존재하는 현재 코인 개수
    private int             maxGoalCount = 0;       // 현재 스테이지에서 목표로 하는 골 개수
    private int             currentGoalCount = 0;   // 현재 스테이지에서 득점한 골 개수 

    private List<TileBlink> blinkTiles;             // 현재 스테이지에 존재하는 순간이동 타일 리스트 

    public void GenerateTilemap(MapData mapData)
    {
        blinkTiles = new List<TileBlink>();

        int width   = mapData.mapSize.x;
        int height  = mapData.mapSize.y;

        for (int y = 0; y < height; ++ y)
        {
            for (int x = 0; x < width; ++x)
            {
                // 격자 형태로 배치된 타일들을 왼쪽 상단부터 순차적으로 번호를 부여
                // 0, 1, 2, 3, 4, 5,
                // 6, 7, ...
                int index = y * width + x;

                // 타일의 속성이 "Empty"이면 아무것도 생성하지 않고 비워둔다
                if (mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                // 생성되는 타일맵의 중앙이 (0, 0, 0)인 위치
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // 현재 index의 맵 정보가 TileType.Empty(0)보다 크고, TileType.LastIndex(8)보다 작으면
                if (mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    if (mapData.mapData[index] == (int)TileType.Base)
                    {
                        bool left = false;
                        bool right = false;
                        if (x != 0 && mapData.mapData[index - 1] == (int)TileType.Base)
                            left = true;
                        if (x != width - 1 && mapData.mapData[index + 1] == (int)TileType.Base)
                            right = true;
                        // 타일 생성
                        SpawnTile((TileType)mapData.mapData[index], position, left, right);
                    }
                    else if (mapData.mapData[index] == (int)TileType.Broke)
                    {
                        bool left = false;
                        bool right = false;
                        if (x != 0 && mapData.mapData[index - 1] == (int)TileType.Broke)
                            left = true;
                        if (x != width - 1 && mapData.mapData[index + 1] == (int)TileType.Broke)
                            right = true;
                        // 타일 생성
                        SpawnTile((TileType)mapData.mapData[index], position, left, right);
                    }
                    else
                    {
                        // 타일 생성
                        SpawnTile((TileType)mapData.mapData[index], position);
                    }
                }

                // 현재 index의 맵 정보가 ItemType.Coin(10)이면
                else if (mapData.mapData[index] == (int)ItemType.Coin)
                {
                    // 아이템 생성
                    SpawnItem(position);
                }
            }
        }

        maxGoalCount = mapData.maxGoalCount;
        currentCoinCount = maxCoinCount;
        // 현재 코인의 개수가 바뀔 때마다 UI 출력 정보 갱신
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);
        stageUI.UpdateGoalCount(currentGoalCount, maxGoalCount);

        // 순간이동 타일은 다른 순간이동 타일로 이동할 수 있기 때문에
        // 맵에 배치되어 있는 모든 순간이동 타일의 정보를 가지고 있어야 한다 
        foreach ( TileBlink tile in blinkTiles )
        {
            tile.SetBlinkTiles(blinkTiles);
        }
    }

    private void SpawnTile(TileType tileType, Vector3 position, bool left = false, bool right = false)
    {
        //GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);
        GameObject clone = Instantiate(tilePrefabs[(int)tileType - 1], position, Quaternion.identity);

        clone.name = "Tile";                    // Tile 오브젝트의 이름을 "Tile"로 설정
        clone.transform.SetParent(transform);   // Tilemap2D 오브젝트를 Tile 오브젝트의 부모로 설정

        Tile tile = clone.GetComponent<Tile>(); // 방금 생성한 타일(clone) 오브젝트의 Tile.Setup() 메소드 호출
        //tile.Setup(tileType);
        tile.Setup(movement2D);

        if ( tileType == TileType.Blink )
        {
            // 현재 맵에 존재하는 순간이동 타일만 따로 리스트에 보관
            blinkTiles.Add(clone.GetComponent<TileBlink>());
        }

        if ( tileType == TileType.Base )
        {
            // 왼쪽과 오른쪽 타일이 같은 타입일 시에 해당 타일에 각각 bool타입의 변수를 수정
            clone.GetComponent<TileBase>().existLeft = left;
            clone.GetComponent<TileBase>().existRight = right;
        }

        if ( tileType == TileType.Broke )
        {
            // 왼쪽과 오른쪽 타일이 같은 타입일 시에 해당 타일에 각각 bool타입의 변수를 수정
            clone.GetComponent<TileBroke>().existLeft = left;
            clone.GetComponent<TileBroke>().existRight = right;
        }
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item";                    // Tile 오브젝트의 이름을 "Item"으로 설정
        clone.transform.SetParent(transform);   // Tilemap2D 오브젝트를 Tile 오브젝트의 부모로 설정

        // 현재 아이템은 코인 밖에 없기 때문에 생성한 아이템의 개수 = 코인 개수
        maxCoinCount ++;
    }

    public void GetCoin(GameObject coin)
    {
        currentCoinCount --;
        // 현재 코인의 개수가 바뀔 때마다 UI 출력 정보 갱신
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        // 코인 아이템이 사라질 때 호출하는 Item.Exit() 메소드 호출
        coin.GetComponent<Item>().Exit();

        // 현재 스테이지에 코인 개수가 0이면
        if ( currentCoinCount == 0 && currentGoalCount == maxGoalCount)
        {
            // 게임 클리어
            stageController.GameClear();
        }
    }

    public void Goal()
    {
        currentGoalCount++;
        stageUI.UpdateGoalCount(currentGoalCount, maxGoalCount);

        if (currentCoinCount == 0 && currentGoalCount >= maxGoalCount)
        {
            // 게임 클리어
            stageController.GameClear();
        }
    }
}
