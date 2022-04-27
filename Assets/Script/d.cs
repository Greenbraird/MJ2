using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class d : MonoBehaviour
{ 
    //도둑의 목적지 list
    public static List<Vector3> points = new List<Vector3>();

    public int nextNum = 0;

    Vector3 vDir = Vector3.zero;
    Color gray1;

    void Start()
    {
        points.Add(new Vector3(-17, 0, -2));
        points.Add(new Vector3(5, 0, 2));
        points.Add(new Vector3(-18, 0, -14));
        points.Add(new Vector3(10, 0, -12));
        points.Add(new Vector3(-2, 0, -6));

        //플레이어가 술래일 때 도둑 알파값 0 한다. 도둑을 한명 늘린다.
        gray1 = new Color(56 / 255f, 56 / 255f, 56 / 255f);
        gray1.a = 0 / 255f;

        if (player.IsHide == false)
            for (int i = 1; i < ch_d_list.d_cnt + 2; i++)
                ch_d_list.objs[i].GetComponent<MeshRenderer>().material.color = gray1;
    }

    void Update()
    {
        //정해진 목적지로 이동한다
        GetComponent<NavMeshAgent>().SetDestination(points[nextNum]);
        vDir = points[nextNum] - this.transform.position;

        //목적지에 도착했을 때 다음 목적지를 설정한다
        if (vDir.magnitude < 2f)
        {
            nextNum++;
            if (nextNum == 5)
                nextNum = 0;
        }
    }
    
    //다른 도둑이 갇혀있는 도둑을 풀어준다
    void OnTriggerEnter(Collider other)
    {
        if (player.IsHide == true) //플레이어가 도둑일때만 작동한다.
        {
            for (int i = 0; i < ch_d_list.d_cnt + 1; i++)
                //부딪힌 오브젝트가 도둑일때만
                if (other.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = true;

                    //도둑의 속도 되돌리고 위치 고정을 풀어준다
                    if (i != 0)
                        ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 3f;

                    ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
                }
        }
    }
}
