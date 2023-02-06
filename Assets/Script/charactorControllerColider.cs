using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 충돌 반응을 할 수 있게 만들어주는 스크립트입니다
public class charactorControllerColider : MonoBehaviour
{
    public float pushPower = 2.0f;

    AIScript beCaughtAi;
    void Start() {
        beCaughtAi = GameObject.Find("Game Manager").GetComponent<AIScript>();
    }

    public void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
