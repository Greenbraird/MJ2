using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


[System.Serializable]
public class Meterialclass
{
    public string Meterialname;
    public Material material;
}
public class AIScript : MonoBehaviour
{
    [Header("square맵 일때")]
    [SerializeField] public List <Vector3> squarePosition = new List<Vector3>();

    [Header("circle맵 일때")]
    [SerializeField] public List <Vector3> circlePosition = new List<Vector3>();

    [Header("hexagon맵 일때")]
    [SerializeField] public List <Vector3> hexagonPosition = new List<Vector3>();
   
    [Header("AI list")]
    public List<GameObject> aIObject = new List<GameObject>();

    [Header("Marterial")]
    [SerializeField]public Meterialclass[] meterialclass;

    public GameObject spotLightPrepab;
    public GameObject radar;
    public GameObject player;
    int seekHideAiRandemN;
    GameObject seekob;
    bool seekAssignment;

    void Update()
    {
        if(aIObject.Count == 0)
        {
            Time.timeScale = 0f;
            Debug.Log("술래가 모든 도둑을 다 잡았습니다.");
        }
            

        //seekob가 할당 되었을 때 랜덤한 hide Ai를 쫓는다
        try
        {
            if (seekAssignment)
                seekob.GetComponent<NavMeshAgent>().SetDestination(aIObject[seekHideAiRandemN].transform.position);
        }
        catch(System.Exception ex)
        {
            seekHideAiRandemN = Random.Range(0, aIObject.Count);
            
        }
        
        /*
        for(int j = 0; j< aIObject.Count; j++)
        {
            for(int h = 0; h < squarePosition.Count; h++)
            {
                if (aIObject[j].transform.position == squarePosition[h])
                {
                    aIObject[j].GetComponent<Animator>().SetBool("AImove", false);
                }
            }
        }
        */
    }

    IEnumerator seekAiMove()
    {
        yield return new WaitForSeconds(3);
        seekHideAiRandemN = Random.Range(0, aIObject.Count);
        while (seekAssignment)
        {
            yield return new WaitForSeconds(Random.Range(5, 7));
            seekHideAiRandemN = Random.Range(0, aIObject.Count);
        }
    }

    public void playerHiedAiMove()
    {
        int randemnum = Random.Range(0, aIObject.Count);

        //Ai중 하나가 Seek로 바뀐다
        seekob = aIObject[randemnum];
        aIObject.RemoveAt(randemnum);

        //Seek의 material이 변한다
        seekob.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[0].material;

        //tag를 Seek로 바꾼다
        seekob.tag = "Seek";

        //네모맵에 관한 코루틴 함수를 호출
        StartCoroutine(squareCorutineAiMove());

        //Seek Ai에게 필요한 요소들은 다는 코루틴 함수를 호출 
        StartCoroutine(SeekAiready());

        //Seek Ai의 움직임에 관한 코루틴 함수
        StartCoroutine(seekAiMove());

    }

    IEnumerator SeekAiready()//Seek Ai에게 필요한 요소들은 다는 코루딘 함수
    {
        yield return new WaitForSeconds(3);
        //Seek에게 light를 단다
        GameObject.Instantiate(spotLightPrepab, seekob.transform.position, Quaternion.identity).transform.parent = seekob.transform;

        //Seek에게 Scale를 키운다
        seekob.transform.DOScale(new Vector3(10, 10, 10), 2);

        //Seek에게 radar를 단다
        radar = GameObject.Instantiate(radar, seekob.transform.position, Quaternion.identity);

        //radar의 transform을 Seek Ai의 자슥으로 위치 시킨다.
        radar.transform.parent = seekob.transform;

        //radar의 transform Scale를 1,1,1로 바꾼다.
        radar.transform.localScale = new Vector3(1, 1, 1);

        //animation를 걷기로 바꾼다
        seekob.GetComponent<Animator>().SetBool("AImove", true);

        //seek Ai를 움직이게 하기 위한 trigger
        seekAssignment = true;

        //Ai를 hide에 추가
        //aIObject.Add(player);


    }

    public void playerSeekAiMove()//playe가 seek일때 Ai의 움직임
    {
        player.tag = "Seek"; //player의 tag를 seek롤 바꿈

        seekob = player;
        for (int o = 0; o < aIObject.Count; o++) //Ai들을 다 투명하게 바꿈
        {
            aIObject[o].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[3].material;
        }

        //player의 scale를 키운다
        player.transform.DOScale(new Vector3(10, 10, 10), 2);

        //네모맵에 관한 코루딘 함수를 호출
        StartCoroutine(squareCorutineAiMove());

        //play가 seek일 때 필요한 요소들을 추가 시키는 코루딘 함수를 호출
        StartCoroutine(playerSeekready());

    }

    IEnumerator playerSeekready()//play가 seek일 때 필요한 요소들을 추가 시키는 코루딘 함수
    {
        yield return new WaitForSeconds(3);
        //right를 단다
        GameObject.Instantiate(spotLightPrepab, player.transform.position, Quaternion.identity).transform.parent = player.transform;

        radar = GameObject.Instantiate(radar, player.transform.position, Quaternion.identity);
        radar.transform.parent = player.transform;
        radar.transform.localScale = new Vector3(1, 1, 1);

    }

    IEnumerator squareCorutineAiMove()//네모맵에 관한 코루딘 함수
    {
        for (int i = 0; i < aIObject.Count; i++)// AI 랜덤한 위치로 움직이게 한다.(초기값)
        {
            float temp = 0f;
            Vector3 govetor3 = new Vector3(0, 0, 0);
            for (int j = 0; j < squarePosition.Count; j++)
            {
                Vector3 seekpoMaObpo = seekob.transform.position - aIObject[i].transform.position;
                
                if (temp < (seekpoMaObpo- squarePosition[j]).magnitude)
                {
                    temp = (seekpoMaObpo - squarePosition[j]).magnitude;

                    govetor3 = squarePosition[j];
                }
                
            }

            aIObject[i].GetComponent<NavMeshAgent>().SetDestination(govetor3);
            aIObject[i].GetComponent<Animator>().SetBool("AImove", true);
        }
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5,6));
            for (int i = 0; i < aIObject.Count; i++)// AI 랜덤한 위치로 움직이게 한다.
            {
                aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
                aIObject[i].GetComponent<Animator>().SetBool("AImove", true);
            }

        }
    }

   

}
