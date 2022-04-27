using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s : MonoBehaviour
{
    //list index ���� ���
    int nextNum = 1;
    int cnt = 0;

    //ĳ������ ����
    Color yellow = new Color(255 / 255f, 240 / 255f, 124 / 255f);
    Color gray = new Color(56 / 255f, 56 / 255f, 56 / 255f);

    void Start()
    {
        //�÷��̾ �����϶�
        if (player.IsHide == false)
            gameObject.SetActive(false);
        //�÷��̾ �����϶� 6��° ������ ���ش�.
/*        else if(player.IsHide == true)
        {
            ch_d_list.objs[ch_d_list.d_cnt + 1].gameObject.SetActive(false);
           // playerTransform = controller.GetComponent<Transform>();
        }
*/    }

    void Update()
    {
        //������ ������ �ٴѴ�.
        GetComponent<NavMeshAgent>().SetDestination(ch_d_list.objs[nextNum].transform.position);

        //��� ������ ã�Ҵ��� Ȯ���ϰ� ���� ĥ�Ѵ�
        cnt = 0;
        for(int n = 0; n < ch_d_list.d_cnt + 1; n++)
        {
            //������ ���� ���� - �ִϸ��̼� ���� �κ�
            if (ch_d_list.obj_true[n] == true)
            {
                cnt++;
                //ch_d_list.objs[n].GetComponent<MeshRenderer>().material.color = yellow;
            }
            //���� ���� - �ִϸ��̼� ���� �κ�
            //else if (ch_d_list.obj_true[n] == false)
                //ch_d_list.objs[n].GetComponent<MeshRenderer>().material.color = gray;
        }
            
        if (cnt == 0)
        {
            GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
            Debug.Log("end");
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

        for (int i = 0; i < ch_d_list.d_cnt + 1; i++)
        {
            //�ε��� ������Ʈ�� �����϶���
            if (hit.gameObject == ch_d_list.objs[i].gameObject)
            {
                //������ �ӵ� 0���� �ٲٰ� ��ġ �����Ѵ�.
                if (i != 0)
                    ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 0f;
                ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                ch_d_list.obj_true[i] = false;
            
                //���ϰ� �ε��� ������ ���� ������ �����Ѵ� (���� ����)
                for (int j = 0; j < ch_d_list.d_cnt + 1; j++)
                    if (ch_d_list.obj_true[j] == true)
                        nextNum = j;

                if (i == 0)
                {
                    ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                    Debug.Log("player end");
                }
            }
        }
    }
}