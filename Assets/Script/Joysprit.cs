using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joysprit : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;

    private float radius;

    [SerializeField] private GameObject go_Plaayer;
    [SerializeField] private float moveSpeed;

    private bool isTouch = false;
    private Vector3 movePosition;

    void Update()
    {
        if (isTouch)
            go_Plaayer.transform.position += movePosition;
    }

    void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;
        Debug.Log("비정규화" + value);
        value = value.normalized;
        Debug.Log("정규화" + value);
        movePosition = new Vector3(value.x * moveSpeed * Time.deltaTime, 0f, value.y * moveSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect_Joystick.localPosition = Vector3.zero;
    }
}
