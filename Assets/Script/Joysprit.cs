using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joysprit : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;
    [SerializeField] private CharacterController controller;

    private float radius;

    [SerializeField] private GameObject go_Plaayer;
    [SerializeField] private float moveSpeed;

    //private bool isTouch = false;
    private Vector3 movePosition;
    public Animator anim;

    private void Start()
    {
        radius = rect_Background.rect.width * 0.5f;
    }

    void Update()
    {
        // if (isTouch)
        //go_Plaayer.transform.position += movePosition;
        controller.Move(movePosition * Time.deltaTime * moveSpeed);
    }

  
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 value = eventData.position - (Vector2)rect_Background.position;

        value = Vector2.ClampMagnitude(value, radius);
        rect_Joystick.localPosition = value;
        Debug.Log("비정규화" + value);
        value = value.normalized;
        Debug.Log("정규화" + value);
        movePosition = new Vector3(value.x , 0f, value.y);
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //isTouch = true;
        anim.SetBool("player walk", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
        anim.SetBool("player walk", false);
    }


}
