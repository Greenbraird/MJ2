using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joyscript : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rect_Background;
    [SerializeField] private RectTransform rect_Joystick;
    [SerializeField] private CharacterController controller; //

    [SerializeField] Transform playerTransform;
    private float radius;
    private float playerRotation;
    private bool isTouch;

    public static bool IsAlive = true;

    [SerializeField] private float moveSpeed;

    private Vector3 movePosition;
    public Animator anim; //애니메이션  //

    private void Start() {
        playerTransform = controller.GetComponent<Transform>();

        radius = rect_Background.rect.width * 0.5f;
        isTouch = false;
    }

    void Update() {
        if (isTouch) {
            controller.Move(movePosition * Time.deltaTime * moveSpeed);
            //SoundManager.instance.PlaySE("Footprint Sound");
        }
        playerTransform.transform.rotation = Quaternion.Euler(0, playerRotation, 0);
    }

    public void OnDrag(PointerEventData eventData) {
        if (IsAlive) {
            //SoundManager.instance.PlaySE("Footprint Sound");

            //Player Cha y값 회전에 대한 코드
            Vector2 v2 = rect_Joystick.position - rect_Background.position;
            playerRotation = Mathf.Atan2(v2.x, v2.y) * Mathf.Rad2Deg;

            //Player Char의 움직임에 대한 코드
            anim.SetBool("player walk", true);
            Vector2 value = eventData.position - (Vector2)rect_Background.position;

            value = Vector2.ClampMagnitude(value, radius);
            rect_Joystick.localPosition = value;

            value = value.normalized;
            movePosition = new Vector3(value.x, 0f, value.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        rect_Background.position = new Vector2(eventData.position.x, eventData.position.y);
        isTouch = true;
        //SoundManager.instance.PlaySE("Footprint Sound");
    }

    public void OnPointerUp(PointerEventData eventData) {
        //SoundManager.instance.OffSE("Footprint Sound");

        rect_Joystick.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
        anim.SetBool("player walk", false);
    }
}
