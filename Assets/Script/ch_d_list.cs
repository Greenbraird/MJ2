//chractor ������ ��ġ�ϴ� �ڵ�

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch_d_list : MonoBehaviour
{
    //������ ���� GameObject ���
    public static List<GameObject> objs = new List<GameObject>(8);

    public GameObject pl;
    public GameObject d1;
    public GameObject d2;
    public GameObject d3;
    public GameObject d4;
    public GameObject d5;
    public GameObject d6;
    public GameObject sl;

    //������ ���� ����ڴ� false�� ����
    public static List<bool> obj_true = new List<bool>();

    //������ ��, static ������ �Ѱ����� ����
    public static int d_cnt = 5;

    void Start()
    {
        for (int i = 0; i < d_cnt + 1; i++) //���� 5��� �÷��̾� �Ѹ�, �� 6��
            obj_true.Add(true);

        objs.Add(pl);
        objs.Add(d1);
        objs.Add(d2);
        objs.Add(d3);
        objs.Add(d4);
        objs.Add(d5);
        objs.Add(d6);
        objs.Add(sl);
    }

}
