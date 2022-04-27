using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    [SerializeField]Meterialclass[] meterialclass;

    public GameObject seekob;


    void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    void Update()
    {
        //GO.GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
    }

    public void playerHiedAiMove()
    {
        int randemnum = Random.Range(0, aIObject.Count);

        //Ai중 하나가 Seek로 바뀐다
        GameObject seekob = aIObject[randemnum];
        aIObject.RemoveAt(randemnum);

        //Seek의 material이 변한다
        seekob.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[0].material;

        //Seek
        
        StartCoroutine(squareCorutineAiMove());
    }

    

    IEnumerator squareCorutineAiMove()
    {
        for (int i = 0; i < aIObject.Count; i++)
        {
            aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
        }
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(7,11));
            for (int i = 0; i < aIObject.Count; i++)
            {
                aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
            }

        }
    }

    public void playerSeekAiMove()
    {
        for (int i = 0; i < aIObject.Count; i++)
        {
            aIObject[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[3].material;
        }

        StartCoroutine(squareCorutineAiMove());

    }

}
