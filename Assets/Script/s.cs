using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class s : MonoBehaviour
{
    //list index 관리 상수
    int nextNum = 1;
    int cnt = 0;

    //캐릭터의 색상
    Color yellow = new Color(255 / 255f, 240 / 255f, 124 / 255f);
    Color gray = new Color(56 / 255f, 56 / 255f, 56 / 255f);

    void Start()
    {
        //플레이어가 술래일때
        if (player.IsHide == false)
            gameObject.SetActive(false);
        //플레이어가 도둑일때 6번째 도둑을 없앤다.
/*        else if(player.IsHide == true)
        {
            ch_d_list.objs[ch_d_list.d_cnt + 1].gameObject.SetActive(false);
           // playerTransform = controller.GetComponent<Transform>();
        }
*/    }

    void Update()
    {
        //도둑을 잡으러 다닌다.
        GetComponent<NavMeshAgent>().SetDestination(ch_d_list.objs[nextNum].transform.position);

        //모든 도둑을 찾았는지 확인하고 색을 칠한다
        cnt = 0;
        for(int n = 0; n < ch_d_list.d_cnt + 1; n++)
        {
            //잡히지 않은 도둑 - 애니메이션 삽입 부분
            if (ch_d_list.obj_true[n] == true)
            {
                cnt++;
                //ch_d_list.objs[n].GetComponent<MeshRenderer>().material.color = yellow;
            }
            //잡힌 도둑 - 애니메이션 삽입 부분
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
        //rigidbody 심어준다.
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;

        for (int i = 0; i < ch_d_list.d_cnt + 1; i++)
        {
            //부딪힌 오브젝트가 도둑일때만
            if (hit.gameObject == ch_d_list.objs[i].gameObject)
            {
                //도둑의 속도 0으로 바꾸고 위치 고정한다.
                if (i != 0)
                    ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 0f;
                ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                ch_d_list.obj_true[i] = false;
            
                //도둑과 부딪힐 때마다 잡을 도둑을 변경한다 (랜덤 위함)
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