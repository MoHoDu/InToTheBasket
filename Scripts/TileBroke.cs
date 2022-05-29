using UnityEngine;

public class TileBroke : Tile
{
    // onlyOne = 0, rightOne = 1, leftOne = 2, both = 3
    public bool existLeft = false;
    public bool existRight = false;

    [SerializeField]
    private GameObject  tileBrokeEffect;

    [SerializeField]
    private Sprite[] sprites;

    private void Update()
    {
        int whatType = existLeft == false && existRight == false ?
                        0 : existLeft && existRight ?
                        3 : existLeft ?
                        2 : 1;

        this.GetComponent<SpriteRenderer>().sprite = sprites[whatType];
    }

    public override void Collision(CollisionDirection direction)
    {
        // 타일이 부서지는 효과를 재생하는 파티클 프리팹 생성
        Instantiate(tileBrokeEffect, transform.position, Quaternion.identity);

        // 플레이어의 아래쪽과 타일이 부딪히면 플레이어 점프
        if ( direction == CollisionDirection.Down)
        {
            movement2D.JumpTo(); 
        }

        // 타일 삭제
        Destroy(gameObject);
    }
}
