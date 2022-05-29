using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectTwinkleEffect : MonoBehaviour
{
    [SerializeField]
    private float           fadeTime;   // 페이드 되는 시간
    [SerializeField]
    private Image[]         ObjectFade;   // 페이드 효과에 사용되는 TMPro

    public int             selectNum = 0;
    public Image[] ObjectFades => ObjectFade;

    private void Awake()
    {
        // Fade In<->Out을 반복해서 반짝이는 효과 재생
        StartCoroutine(Twinkle(ObjectFade[selectNum]));
    }

    private void Update()
    {
        // 아래 키를 눌러서 아래 메뉴로 선택을 옮기면 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectNum != ObjectFade.Length - 1)
            {
                selectNum += 1;
                StopAllCoroutines();
                Color color = ObjectFade[selectNum - 1].color;
                color.a = 1;
                ObjectFade[selectNum - 1].color = color;
                StartCoroutine(Twinkle(ObjectFade[selectNum]));
            }
        }
        // 위 키를 눌러서 윗 메뉴로 선택을 옮기면 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectNum != 0)
            {
                selectNum -= 1;
                StopAllCoroutines();
                Color color = ObjectFade[selectNum + 1].color;
                color.a = 1;
                ObjectFade[selectNum + 1].color = color;
                StartCoroutine(Twinkle(ObjectFade[selectNum]));
            }
        }
    }

    private IEnumerator Twinkle(Image objectFade)
    {
        while ( true )
        {
            yield return StartCoroutine(Fade(1, 0.2f, objectFade));    // Fade In

            yield return StartCoroutine(Fade(0.2f, 1, objectFade));    // Fade Out
        }
    }

    private IEnumerator Fade(float start, float end, Image objectFade)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color     = objectFade.color;
            color.a         = Mathf.Lerp(start, end, percent);
            objectFade.color  = color;

            yield return null;
        }
    }
}
