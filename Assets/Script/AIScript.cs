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
    GameObject seekob;

    public void Update()
    {
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
    }

    public void playerHiedAiMove()
    {
        int randemnum = Random.Range(0, aIObject.Count);

        //Ai중 하나가 Seek로 바뀐다
        seekob = aIObject[randemnum];
        aIObject.RemoveAt(randemnum);

        //Seek의 material이 변한다
        seekob.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[0].material;

        seekob.tag = "Seek";
        StartCoroutine(squareCorutineAiMove());
        StartCoroutine(SeekAiready());
    }

    IEnumerator SeekAiready()
    {
        yield return new WaitForSeconds(3);
        //Seek에게 light를 단다
        GameObject.Instantiate(spotLightPrepab, seekob.transform.position, Quaternion.identity).transform.parent = seekob.transform;

        //Seek에게 Scale를 키운다
        seekob.transform.DOScale(new Vector3(10, 10, 10), 2);

        //Seek에게 radar를 단다
        radar = GameObject.Instantiate(radar, seekob.transform.position, Quaternion.identity);
        radar.transform.parent = seekob.transform;
        radar.transform.localScale = new Vector3(1, 1, 1);
        seekob.GetComponent<NavMeshAgent>().SetDestination(aIObject[Random.Range(1, aIObject.Count)].transform.position);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7, 11));

            seekob.GetComponent<NavMeshAgent>().SetDestination(aIObject[Random.Range(1, aIObject.Count)].transform.position);
            seekob.GetComponent<Animator>().SetBool("AImove", true);
        
        }
    }

    public void playerSeekAiMove()
    {
        player.tag = "Seek";
        for (int o = 0; o < aIObject.Count; o++)
        {
            aIObject[o].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[3].material;
        }

        player.transform.DOScale(new Vector3(10, 10, 10), 2);

        StartCoroutine(squareCorutineAiMove());
        StartCoroutine(playerSeekready());

    }

    IEnumerator playerSeekready()
    {
        yield return new WaitForSeconds(3);
        GameObject.Instantiate(spotLightPrepab, player.transform.position, Quaternion.identity).transform.parent = player.transform;

        radar = GameObject.Instantiate(radar, player.transform.position, Quaternion.identity);
        radar.transform.parent = player.transform;
        radar.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator squareCorutineAiMove()
    {
        for (int i = 0; i < aIObject.Count; i++)
        {
            aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
            aIObject[i].GetComponent<Animator>().SetBool("AImove", true);
        }
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7,11));
            for (int i = 0; i < aIObject.Count; i++)
            {
                aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
                aIObject[i].GetComponent<Animator>().SetBool("AImove", true);
            }

        }
    }

   

}
