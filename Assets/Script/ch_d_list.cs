//chractor 폴더에 위치하는 코드

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch_d_list : MonoBehaviour
{
    //술래가 잡을 GameObject 목록
    public static List<GameObject> objs = new List<GameObject>(8);

    public GameObject pl;
    public GameObject d1;
    public GameObject d2;
    public GameObject d3;
    public GameObject d4;
    public GameObject d5;
    public GameObject d6;
    public GameObject sl;

    //술래가 잡은 사용자는 false로 설정
    public static List<bool> obj_true = new List<bool>();

    //도둑의 수, static 변수로 한곳에서 관리
    public static int d_cnt = 5;

    void Start()
    {
        for (int i = 0; i < d_cnt + 1; i++) //도둑 5명과 플레이어 한명, 총 6명
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
