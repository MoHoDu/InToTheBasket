using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragList : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
{
    public ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = transform.parent.parent.parent.GetComponent<ScrollRect>();
    }
    public void OnBeginDrag(PointerEventData e)
    {
        scrollRect.OnBeginDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        scrollRect.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        scrollRect.OnDrag(e);
    }

    public void OnScroll(PointerEventData eventData)
    {
        scrollRect.OnScroll(eventData);
    }
}
