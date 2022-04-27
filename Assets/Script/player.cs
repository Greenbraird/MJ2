using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{ 
    public static float speed = 10f;
    Vector3 movement;

    int cnt = ch_d_list.d_cnt;
    public static bool IsHide = true;

    Color gray = new Color(56 / 255f, 56 / 255f, 56 / 255f);

    void Start()
    {
        if (IsHide == false)
        {
            //레이저 생긴다
        }
    }

    public float pushPower = 2.0f;

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //rigidbody 심어준다.
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;


        if (IsHide == true) //플레이어가 도둑일때
        {
            for (int i = 1; i < ch_d_list.d_cnt + 1; i++)
                //부딪힌 오브젝트가 도둑일때만
                if (hit.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = true;

                    //도둑의 속도 되돌리고 위치 고정을 풀어준다
                    ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 3f;
                    //ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
                }
        }
        else //플레이어가 술래일때
        {
            for (int i = 1; i < ch_d_list.d_cnt + 2; i++)
                //부딪힌 오브젝트가 도둑일때만
                if (hit.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = false;

                    //도둑의 속도 멈추고 위치 고정하고 색 변하게 한다.
                    ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 0f;
                    ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    //ch_d_list.objs[i].GetComponent<MeshRenderer>().material.color = gray;

                    cnt--;
                    if (cnt == 0)
                        Debug.Log("end");

                }
        }
    }



    void OnTriggerEnter(Collider other)
    {
        
    }
}
