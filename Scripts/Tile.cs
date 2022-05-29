using UnityEngine;

// 타일의 속성 (빈 공간, 기본, 부서질 수 있는, 폭탄, 점프, 왼쪽 직진, 오른쪽 직진, 순간이동, 개수 파악용)
public enum TileType { Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, Basket, BackBoard, Bar, onlyBar, EnemyBasket, LastIndex }

// 플레이어와 타일의 충돌 방향 (플레이어 기준)
public enum CollisionDirection { Up = 0, Down }

// 추상 클래스로 정의 
public abstract class Tile : MonoBehaviour
{
    /*[SerializeField]
    private Sprite[]        images;             // 타일에 적용될 수 있는 이미지 배열
    private SpriteRenderer  spriteRenderer;     // 타일 이미지 변경을 위한 SpriteRenderer
    private TileType        tileType;           // 현재 타일의 속성

    public void Setup(TileType tileType)
    {
        spriteRenderer  = GetComponent<SpriteRenderer>();
        TileType        = tileType;
    }

    // TileType 프로퍼티 
    public TileType TileType
    {
        set
        {
            tileType                = value;
            spriteRenderer.sprite   = images[(int)tileType - 1];
        }
        get => tileType;
    }*/

    protected Movement2D movement2D;    // 타일과 부딪힌 플레이어를 조작하기 위한 Movement2D

    // 가상 메소드 Setup
    public virtual void Setup(Movement2D movement2D)
    {
        this.movement2D = movement2D;
    }

    // 플레이어가 타일과 부딪히면 실행되는 추상 메소드 
    public abstract void Collision(CollisionDirection direction);
}
