using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class d : MonoBehaviour
{ 
    //������ ������ list
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

        //�÷��̾ ������ �� ���� ���İ� 0 �Ѵ�. ������ �Ѹ� �ø���.
        gray1 = new Color(56 / 255f, 56 / 255f, 56 / 255f);
        gray1.a = 0 / 255f;

        if (player.IsHide == false)
            for (int i = 1; i < ch_d_list.d_cnt + 2; i++)
                ch_d_list.objs[i].GetComponent<MeshRenderer>().material.color = gray1;
    }

    void Update()
    {
        //������ �������� �̵��Ѵ�
        GetComponent<NavMeshAgent>().SetDestination(points[nextNum]);
        vDir = points[nextNum] - this.transform.position;

        //�������� �������� �� ���� �������� �����Ѵ�
        if (vDir.magnitude < 2f)
        {
            nextNum++;
            if (nextNum == 5)
                nextNum = 0;
        }
    }
    
    //�ٸ� ������ �����ִ� ������ Ǯ���ش�
    void OnTriggerEnter(Collider other)
    {
        if (player.IsHide == true) //�÷��̾ �����϶��� �۵��Ѵ�.
        {
            for (int i = 0; i < ch_d_list.d_cnt + 1; i++)
                //�ε��� ������Ʈ�� �����϶���
                if (other.gameObject == ch_d_list.objs[i].gameObject)
                {
                    ch_d_list.obj_true[i] = true;

                    //������ �ӵ� �ǵ����� ��ġ ������ Ǯ���ش�
                    if (i != 0)
                        ch_d_list.objs[i].GetComponent<NavMeshAgent>().speed = 3f;

                    ch_d_list.objs[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;
                }
        }
    }
}
