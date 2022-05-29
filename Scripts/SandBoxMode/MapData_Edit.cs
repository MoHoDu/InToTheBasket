using UnityEngine;

[System.Serializable]
public class MapData_Edit
{
    public Vector2Int   mapSize;
    public int[]        mapData;

    public Vector2Int   playerPosition;

    public int maxGoalCount;
}
