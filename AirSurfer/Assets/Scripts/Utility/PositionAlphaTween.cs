using UnityEngine;
using System.Collections;
using SimpleTween;
using UnityEngine.UI;

public class PositionAlphaTween : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Tween alphaTween;
    private Tween positionTween;
    private bool isShowing = false;

    [SerializeField]private Text myText;
    public bool tweenY;
    public float posVal = 200.0f;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();

        canvasGroup.alpha = 0.0f;
    }

    /// <summary>
    /// Show an on screen message
    /// </summary>
    /// <param name="duration">The duration to display it for (in seconds)</param>
    /// <param name="fadeTime">The transition in/out time.</param>
    public void ShowInfoPopup(float duration, float fadeTime)
    {
        // if we're still showing a previous message, then cancel the previous tweens
        if (isShowing)
        {
            SimpleTweener.RemoveTween(alphaTween);
            SimpleTweener.RemoveTween(positionTween);
        }

        isShowing = true;

        // fade up the alpha, wait for our duration, then fade it out again.
        alphaTween = SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1.0f, fadeTime).OnCompleted(() => {
            alphaTween = SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.0f, fadeTime).Delay(duration).OnCompleted(() => {
                // the popup has now disappeared
                isShowing = false;
            });
        });

        // also animate the x/y-position in a similar fashion.
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 startPos = rect.anchoredPosition;

        if (tweenY) 
        {
            startPos.y = posVal;
            rect.anchoredPosition = startPos;
            positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(startPos.x, 30), fadeTime).Ease(Easing.EaseLinear).OnCompleted(() => {
                positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(startPos.x, -30), duration).Ease(Easing.EaseLinear).OnCompleted(() => {
                    positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(-startPos.x, -200), fadeTime).Ease(Easing.EaseLinear);
                });
            });
        }
        else 
        {
            startPos.x = 200.0f;
            rect.anchoredPosition = startPos;
            positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(30, startPos.y), fadeTime).Ease(Easing.EaseLinear).OnCompleted(() => {
                positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(-30, startPos.y), duration).Ease(Easing.EaseLinear).OnCompleted(() => {
                    positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(-200, startPos.y), fadeTime).Ease(Easing.EaseLinear);
                });
            });
        }
        
    }


    public void ShowInfoPopup(float duration, float fadeTime, Vector3 pos)
    {

       

        // if we're still showing a previous message, then cancel the previous tweens
        if (isShowing)
        {
            SimpleTweener.RemoveTween(alphaTween);
            SimpleTweener.RemoveTween(positionTween);
        }

        isShowing = true;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);

        // fade up the alpha, wait for our duration, then fade it out again.
        alphaTween = SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1.0f, fadeTime).OnCompleted(() => {
            alphaTween = SimpleTweener.AddTween(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0.0f, fadeTime).Delay(duration).OnCompleted(() => {
                // the popup has now disappeared
                isShowing = false;
            });
        });

        // also animate the x/y-position in a similar fashion.
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = anchoredPosition;
        Vector2 startPos = rect.anchoredPosition;

        if (tweenY)
        {
            startPos.y = posVal;
            rect.anchoredPosition = startPos;
            positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(startPos.x, 30), fadeTime).Ease(Easing.EaseLinear).OnCompleted(() => {
                positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(startPos.x, -30), duration).Ease(Easing.EaseLinear).OnCompleted(() => {
                    positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, y => rect.anchoredPosition = y, new Vector2(-startPos.x, -200), fadeTime).Ease(Easing.EaseLinear);
                });
            });
        }
        else
        {
            startPos.x = 0.0f;
            rect.anchoredPosition = startPos;
            positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(30, startPos.y), fadeTime).Ease(Easing.EaseLinear).OnCompleted(() => {
                positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(-30, startPos.y), duration).Ease(Easing.EaseLinear).OnCompleted(() => {
                    positionTween = SimpleTweener.AddTween(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(-200, startPos.y), fadeTime).Ease(Easing.EaseLinear);
                });
            });
        }

    }

    public void SetText(string msg) 
    {
        myText.text = msg;
    }

    public void SetText(string msg, Color col)
    {
        myText.text = msg;
        myText.color = col;
    }
}
