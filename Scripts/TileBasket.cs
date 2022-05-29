using UnityEngine;

public class TileBasket : Tile
{
    [SerializeField]
    private GameObject  tileGoalEffect;

    private Tilemap2D   tilemap2D;

    private StageController stageInfo;

    public override void Collision(CollisionDirection direction)
    {
        // 플레이어의 아래쪽과 타일이 부딪히면 골 처리 
        if (direction == CollisionDirection.Down)
        {
            // 골인 효과를 재생하는 파티클 프리팹 생성
            Instantiate(tileGoalEffect, transform.position, Quaternion.identity);
            //Debug.Log("Goal");

            stageInfo = GameObject.Find("StageController").GetComponent<StageController>();
            tilemap2D = GameObject.Find("Tilemap2D").GetComponent<Tilemap2D>();
            tilemap2D.Goal();
            Vector2Int position = stageInfo.playerPosition;

            movement2D.transform.position = new Vector3(position.x, position.y);

            //movement2D.JumpTo();
        }


    }
}
