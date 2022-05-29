using UnityEngine;

// 아이템의 속성 (코인)
public enum ItemType { Coin = 50 }

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject  itemEffectPrefab;   // 아이템 획득 이펙트 프리팹

    public void Exit()
    {
        // 아이템이 사라질 때 아이템 획득 이펙트 생성
        Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);

        // 아이템 오브젝트 삭제
        Destroy(gameObject);
    }
}
