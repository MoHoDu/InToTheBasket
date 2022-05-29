using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera      mainCamera;                     // 메인 카메라
    private TileType_Edit    currentType = TileType_Edit.Empty;   // 마우스 클릭된 위치의 타일을 currentType 속성으로 변경
    private Tile_Edit        playerTile = null;              // 플레이어 타일 정보

    [SerializeField]
    private CameraController_Edit    cameraController;       // 카메라 위치, 줌 제어를 위한 CameraController
    private Vector2             previousMousePosition;  // 직전 프레임의 마우스 위치
    private Vector2             currentMousePosition;   // 현재 프레임의 마우스 위치

    [SerializeField]
    private Tilemap2D_Edit tilemap2D;

    [SerializeField]
    private Sprite[] imagesBase;
    [SerializeField]
    private Sprite[] imagesBroke;

    private void Update()
    {
        // 현재 마우스가 UI 위에 있을 때 IsPointerOverGameObject()가 true로 반환
        // 즉, 마우스가 UI 위에 있을 때는 Update() 내용이 실행되지 않는다
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        // 카메라 이동, Zoom In/Out
        UpdateCamera();

        RaycastHit hit;
        // 마우스 왼쪽 버튼을 누르고 있을 때
        if ( Input.GetMouseButton(0) )
        {
            // 카메라로부터 현재 마우스 위치로 뻗어나가는 광선 생성
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // 광선에 부딪힌 오브젝트가 존재하면 hit에 저장
            if ( Physics.Raycast(ray, out hit, Mathf.Infinity) )
            {
                // hit 오브젝트의 Tile 컴포넌트 정보를 불러와 tile 변수에 저장
                // 이 때에 hit 오브젝트에 Tile 컴포넌트가 없으면 null 변환
                Tile_Edit tile = hit.transform.GetComponent<Tile_Edit>();
                PosTile posTile = tile.tilePosition;
                if ( tile != null )
                {
                    // 플레이어 타일은 맵에 1개만 배치할 수 있기 때문에
                    // 이전에 배치된 플레이어 타일이 있으면 Empty 속성으로 설정
                    if ( currentType == TileType_Edit.Player )
                    {
                        if ( playerTile != null )
                        {
                            playerTile.TileType_Edit = TileType_Edit.Empty;
                        }
                        playerTile = tile;
                    }

                    if ( currentType == TileType_Edit.Base )
                    {
                        int leftNum     = posTile.PosX > 0 ?
                                            posTile.PosY * posTile.Width + posTile.PosX - 1 :
                                            -1;

                        int rightNum    = posTile.PosX + 1 < posTile.Width ?
                                            posTile.PosY * posTile.Width + posTile.PosX + 1 :
                                            -1;
                        Debug.Log(posTile.PosX + "\n" + leftNum + "\n" + rightNum);

                        Tile_Edit LeftTile  = leftNum != -1 ?
                                                tilemap2D.transform.GetChild(leftNum).GetComponent<Tile_Edit>() :
                                                null;

                        Tile_Edit RightTile = rightNum != -1 ?
                                                tilemap2D.transform.GetChild(rightNum).GetComponent<Tile_Edit>() :
                                                null;
                        Debug.Log(LeftTile + "\n" + RightTile);

                        int a = 0;
                        if (LeftTile != null && LeftTile.TileType_Edit == currentType)
                        {
                            //tile.GetComponent<TileBase>().existLeft = true;
                            //LeftTile.GetComponent<TileBroke>().existRight = true;
                            SpriteRenderer LftSprite = tilemap2D.transform.GetChild(leftNum).GetComponent<SpriteRenderer>();
                            LftSprite.sprite = LftSprite.sprite == imagesBase[2] || LftSprite.sprite == imagesBase[3] ?
                                                imagesBase[3] :
                                                imagesBase[1];
                            a += 2;
                        }

                        if (RightTile != null && RightTile.TileType_Edit == currentType)
                        {
                            //tile.GetComponent<TileBroke>().existRight = true;
                            //RightTile.GetComponent<TileBroke>().existLeft = true;
                            SpriteRenderer RitSprite = tilemap2D.transform.GetChild(rightNum).GetComponent<SpriteRenderer>();
                            RitSprite.sprite = RitSprite.sprite == imagesBase[1] || RitSprite.sprite == imagesBase[3] ?
                                                imagesBase[3] :
                                                imagesBase[2];
                            a += 1;
                        }

                        // 부딪힌 오브젝트를 tileType 속성으로 변경 ( 타일, 아이템, 플레이어 캐릭터 )
                        tile.TileType_Edit = currentType;
                        hit.transform.GetComponent<SpriteRenderer>().sprite = imagesBase[a];
                        return;
                    }

                    if (currentType == TileType_Edit.Broke)
                    {
                        int leftNum = posTile.PosX > 0 ?
                                            posTile.PosY * posTile.Width + posTile.PosX - 1 :
                                            -1;

                        int rightNum = posTile.PosX + 1 < posTile.Width ?
                                            posTile.PosY * posTile.Width + posTile.PosX + 1 :
                                            -1;

                        Tile_Edit LeftTile = leftNum != -1 ?
                                                tilemap2D.transform.GetChild(leftNum).GetComponent<Tile_Edit>() :
                                                null;

                        Tile_Edit RightTile = rightNum != -1 ?
                                                tilemap2D.transform.GetChild(rightNum).GetComponent<Tile_Edit>() :
                                                null;

                        int a = 0;
                        if (LeftTile != null && LeftTile.TileType_Edit == currentType)
                        {
                            //tile.GetComponent<TileBase>().existLeft = true;
                            //LeftTile.GetComponent<TileBroke>().existRight = true;
                            SpriteRenderer LftSprite = tilemap2D.transform.GetChild(leftNum).GetComponent<SpriteRenderer>();
                            LftSprite.sprite = LftSprite.sprite == imagesBroke[2] || LftSprite.sprite == imagesBroke[3] ?
                                                imagesBroke[3] :
                                                imagesBroke[1];
                            a += 2;
                        }

                        if (RightTile != null && RightTile.TileType_Edit == currentType)
                        {
                            //tile.GetComponent<TileBroke>().existRight = true;
                            //RightTile.GetComponent<TileBroke>().existLeft = true;
                            SpriteRenderer RitSprite = tilemap2D.transform.GetChild(rightNum).GetComponent<SpriteRenderer>();
                            RitSprite.sprite = RitSprite.sprite == imagesBroke[1] || RitSprite.sprite == imagesBroke[3] ?
                                                imagesBroke[3] :
                                                imagesBroke[2];
                            a += 1;
                        }

                        // 부딪힌 오브젝트를 tileType 속성으로 변경 ( 타일, 아이템, 플레이어 캐릭터 )
                        tile.TileType_Edit = currentType;
                        hit.transform.GetComponent<SpriteRenderer>().sprite = imagesBroke[a];
                        return;
                    }

                    if ( currentType == TileType_Edit.Basket )
                    {
                        if (posTile.PosX + 1 >= posTile.Width)
                        {
                            tile.TileType_Edit = currentType;
                            return;
                        }

                        int n1 = (posTile.PosY - 1) * posTile.Width + posTile.PosX + 1;
                        int n2 = posTile.PosY       * posTile.Width + posTile.PosX + 1;
                        int n3 = (posTile.PosY + 1) * posTile.Width + posTile.PosX + 1;

                        int childNum = tilemap2D.transform.childCount;

                        Tile_Edit BackBoardTile = tilemap2D.transform.GetChild(n1).GetComponent<Tile_Edit>();
                        Tile_Edit BarTile = tilemap2D.transform.GetChild(n2).GetComponent<Tile_Edit>();

                        BackBoardTile.TileType_Edit = TileType_Edit.BackBoard;
                        BarTile.TileType_Edit = TileType_Edit.Bar;

                        if (n3 <= childNum)
                        {
                            Tile_Edit BarTile2 = tilemap2D.transform.GetChild(n3).GetComponent<Tile_Edit>();
                            BarTile2.TileType_Edit = TileType_Edit.onlyBar;
                        }
                    }

                    // 부딪힌 오브젝트를 tileType 속성으로 변경 ( 타일, 아이템, 플레이어 캐릭터 )
                    tile.TileType_Edit = currentType;
                }
            }
        }
    }

    /// <summary>
    /// 타일, 아이템, 플레이어 캐릭터 버튼을 눌러 tileType을 변경
    /// </summary>
    public void SetTileType(int tileType)
    {
        currentType = (TileType_Edit)tileType;
    }

    public void UpdateCamera()
    {
        // 키보드를 이용한 카메라 이동
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        cameraController.SetPosition(x, y);

        // 마우스 휠 버튼을 이용한 카메라 이동
        if (Input.GetMouseButtonDown(2))
        {
            currentMousePosition = previousMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            currentMousePosition = Input.mousePosition;
            if (previousMousePosition != currentMousePosition)
            {
                Vector2 move = (previousMousePosition - currentMousePosition) * 0.5f;
                cameraController.SetPosition(move.x, move.y);
            }
        }
        previousMousePosition = currentMousePosition;

        // 마우스 휠을 이용한 카메라 Zoom In/Out
        float distance = Input.GetAxisRaw("Mouse ScrollWheel");
        cameraController.SetOrthographicSize(-distance);
    }

}
