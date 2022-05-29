using UnityEngine;

public enum TileType_Edit // 열거형
{
    Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink,
                Basket, BackBoard, Bar, onlyBar, EnemyBasket,               // 타입
    ItemCoin = 50,                                                          // 아이템
    Player = 100,                                                           // 플레이어
}

public struct PosTile
{
    public int PosX;
    public int PosY;
    public int Width;
}

public class Tile_Edit : MonoBehaviour
{
    [SerializeField]
    private Sprite[]        tileImages;      // 타일 이미지 배열
    [SerializeField]
    private Sprite[]        itemImages;      // 아이템 이미지 배열
    [SerializeField]
    private Sprite          playerImage;    // 플레이어 이미지

    private TileType_Edit   tileTypeEdit;

    public  PosTile         tilePosition;

    private SpriteRenderer  spriteRenderer;

    public void Setup(TileType_Edit tileType_Edit, int PosX, int PosY, int width)
    {
        spriteRenderer      = GetComponent<SpriteRenderer>();
        TileType_Edit       = tileType_Edit;
        tilePosition.PosX   = PosX;
        tilePosition.PosY   = PosY;
        tilePosition.Width  = width;
    }

    // TileType 프로퍼티 
    public TileType_Edit TileType_Edit
    {
        set
        {
            tileTypeEdit = value;

            // 타일 (Empty, Base, Broke, Jump, StraightLeft, StraightRight, Blink....)
            if ((int)tileTypeEdit < (int)TileType_Edit.ItemCoin)
            {
                spriteRenderer.sprite = tileImages[(int)tileTypeEdit];
            }
            // 아이템 (Coin)
            else if ( (int)tileTypeEdit < (int)TileType_Edit.Player )
            {
                spriteRenderer.sprite = itemImages[(int)tileTypeEdit - (int)TileType_Edit.ItemCoin];
            }
            // 플레이어 캐릭터 ( 맵 에디터에 보여주기 위해 설정하였으며,
            // 저장할 땐 위치 정보를 저장하고 플레이어 위치의 타일은 Empty로 설정)
            else if ( (int)tileTypeEdit == (int)TileType_Edit.Player )
            {
                spriteRenderer.sprite = playerImage;
            }
        }
        get => tileTypeEdit;
    }

}
