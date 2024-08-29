using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 lastPosition;
    private Vector2 endPosition;
    public Vector2 swipeDelta;

    private Coroutine coroutine;
    public static SwipeDetector instance;

    void Awake()
    {
        instance = this;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        lastPosition = data.position;
    }

    public void OnDrag(PointerEventData data)
    {
        swipeDelta = data.position - lastPosition;

        if (swipeDelta.magnitude < 2)
        {
            swipeDelta = Vector2.zero;
        }
        else
        {
            swipeDelta.Normalize();
        }

        lastPosition = data.position;

        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(Wait());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        swipeDelta = Vector2.zero;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        swipeDelta = Vector2.zero;
    }
}