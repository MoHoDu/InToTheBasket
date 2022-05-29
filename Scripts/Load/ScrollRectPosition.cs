using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollRectPosition : MonoBehaviour
{
    RectTransform scrollRectTransform;      // scrollView 오브젝트
    RectTransform contentPanel;             // scrollView 오브젝트 속 content 오브젝트 
    RectTransform selectedRectTransform;    // 선택된 오브젝트 
    GameObject lastSelected;                // 이전 선택 오브젝트 

    GameObject[] buttons;                   // 스테이지 버튼들 
    public int selectedNum = 0;             // 선택된 스테이지 넘버 -1 

    void Start()
    {
        // 각각 오브젝트들 변수에 저장 
        scrollRectTransform = GetComponent<RectTransform>();
        contentPanel = GetComponent<ScrollRect>().content;
        // buttons 리스트 크기 지정 
        int buttonsNum = contentPanel.transform.childCount;
        buttons = new GameObject[buttonsNum];

        // buttons 리스트에 각각의 버튼들 저장 
        for (int i = 0; i < contentPanel.transform.childCount; i++)
        {
            GameObject index = contentPanel.GetChild(i).gameObject;
            buttons[i] = index;
        }
    }
    void Update()
    {
        // 만약 아래 방향키를 누르면 selectedNum ++ 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 리스트 크기 이상으로 벗어나지 않는 정도로만 
            if (selectedNum != buttons.Length - 1)
            {
                selectedNum++;
            }
        }

        // 만약 위 방향키를 누르면 selectedNum --
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 0번 아래 숫자를 가진 스테이지는 없으므로 
            if (selectedNum > 0)
            {
                selectedNum--;
            }
        }

        //GameObject selected = EventSystem.current.currentSelectedGameObject;
        // 선택된 오브젝트 지정 
        GameObject selected = buttons[selectedNum];

        // 선택된 오브젝트가 없으면 패스 
        if (selected == null)
        {
            return;
        }
        // 선택된 오브젝트의 부모 오브젝트가 contentPanel이 아닌 경우도 패스 
        if (selected.transform.parent != contentPanel.transform)
        {
            return;
        }
        // 저번 선택과 같아도 패스 
        if (selected == lastSelected)
        {
            return;
        }

        if (lastSelected != null)
        {
            Color lastColor = new Color(0, 0, 0, 100f/255f);
            lastSelected.GetComponent<Image>().color = lastColor;
        }

        Color color = new Color(1, 1, 1, 100f/255f);
        selected.GetComponent<Image>().color = color;


        //Debug.Log(selected.name);
        // 선택된 오브젝트의 크기를 가져오기 위해서 변수 생성 
        selectedRectTransform = selected.GetComponent<RectTransform>();

        // 선택된 오브젝트의 Y값 지정
        // abs는 절대값 / anchoredPosition은 오브젝트가 위/왼쪽으로 배치된 경우 (Screen.Width/2, -Screen.Height/2)
        // 즉 Inspector에 나와있는 PosY값의 절대값 + 해당 오브젝트의 높이 
        float selectedPositionY = Mathf.Abs(selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;

        //float scrollViewMinY = contentPanel.anchoredPosition.y;

        //float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;

        //Debug.Log("선택된 오브젝트 Y : " + selectedPositionY);
        //Debug.Log("스크롤뷰 maxY : " + scrollViewMaxY);
        //Debug.Log("스크롤뷰 minY : " + scrollViewMinY);

        // 첫번째 스테이지 버튼을 지정한 경우 PosY를 0으로 지정 
        if (selectedNum == 0)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, 0.0f);
        }
        // 마지막 스테이지 버튼을 지정한 경우 PosY를 contentPanel의 높이로 지정 
        else if (selectedNum == buttons.Length - 1)
        {
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, contentPanel.rect.height);
        }
        // 그 외에는 선택된 오브젝트의 절대값 PosY + 해당 오브젝트 높이 - 스크롤뷰의 높이로 contentPanel PosY 지정 
        else
        {
            float newY = selectedPositionY - scrollRectTransform.rect.height;
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        }

        //else if (selectedPositionY > scrollViewMaxY)
        //{
        //    Debug.Log("On");
        //    float newY = selectedPositionY - scrollRectTransform.rect.height;
        //    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        //}

        //else if (Mathf.Abs(selectedRectTransform.anchoredPosition.y) < scrollViewMinY)
        //{
        //    float newY = selectedPositionY - scrollRectTransform.rect.height;
        //    contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, newY);
        //    //Mathf.Abs(selectedRectTransform.anchoredPosition.y
        //}

        // 이번 지정 오브젝트를 이전 지정 오브젝트로 저장 
        lastSelected = selected;
    }
}