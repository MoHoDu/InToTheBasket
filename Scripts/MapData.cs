using UnityEngine;

[System.Serializable] // json 데이터 불러오기를 위한 직렬화 클래스  
public class MapData
{
    public Vector2Int   mapSize;
    public int[]        mapData;

    public Vector2Int   playerPosition;

    public int          maxGoalCount;
}
