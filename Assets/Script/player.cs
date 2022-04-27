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
            //������ �����
        }
    }

    public float pushPower = 2.0f;

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //rigidbody �ɾ��ش�.
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;


        if (IsHide == true) //�÷��̾ �����϶�
        {
            for (int i = 1; i < ch_d_list.d_cnt + 1; i++)
                //�ε��� ������Ʈ�� �����϶���
                if (hit.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = true;

                    //������ �ӵ� �ǵ����� ��ġ ������ Ǯ���ش�
                    ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 3f;
                    //ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
                }
        }
        else //�÷��̾ �����϶�
        {
            for (int i = 1; i < ch_d_list.d_cnt + 2; i++)
                //�ε��� ������Ʈ�� �����϶���
                if (hit.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = false;

                    //������ �ӵ� ���߰� ��ġ �����ϰ� �� ���ϰ� �Ѵ�.
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
