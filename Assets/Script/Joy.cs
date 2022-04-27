using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joy : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;
    [SerializeField] private CharacterController controller;

    [SerializeField] Transform playerTransform;
    private float radius;
    private float playerRotation;
    private bool isTouch;

    [SerializeField] private float moveSpeed;

    private Vector3 movePosition;
    public Animator anim;

    private void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
        isTouch = false;
        playerTransform = controller.GetComponent<Transform>();
    }

    void Update()
    {
        if (isTouch)
            controller.Move(movePosition * Time.deltaTime * moveSpeed);

        playerTransform.transform.rotation = Quaternion.Euler(0, playerRotation, 0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Player Cha y값 회전에 대한 코드
        Vector2 v2 = rect_Joystick.position - rect_Background.position;
        playerRotation = Mathf.Atan2(v2.x, v2.y) * Mathf.Rad2Deg;
        Debug.Log(eventData.GetType().Name);
        //Player Char의 움직임에 대한 코드
        anim.SetBool("player walk", true);
        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;
        //Debug.Log("비정규화" + value);

        value = value.normalized;

        //Debug.Log("정규화" + value);
        movePosition = new Vector3(value.x, 0f, value.y);


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rect_Background.position = new Vector2(eventData.position.x, eventData.position.y);
        isTouch = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
        anim.SetBool("player walk", false);
    }

}
