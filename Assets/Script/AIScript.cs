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
    [Header("square�� �϶�")]
    [SerializeField] public List <Vector3> squarePosition = new List<Vector3>();

    [Header("circle�� �϶�")]
    [SerializeField] public List <Vector3> circlePosition = new List<Vector3>();

    [Header("hexagon�� �϶�")]
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
            Debug.Log("������ ��� ������ �� ��ҽ��ϴ�.");
        }
            

        //seekob�� �Ҵ� �Ǿ��� �� ������ hide Ai�� �Ѵ´�
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

        //Ai�� �ϳ��� Seek�� �ٲ��
        seekob = aIObject[randemnum];
        aIObject.RemoveAt(randemnum);

        //Seek�� material�� ���Ѵ�
        seekob.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[0].material;

        //tag�� Seek�� �ٲ۴�
        seekob.tag = "Seek";

        //�׸�ʿ� ���� �ڷ�ƾ �Լ��� ȣ��
        StartCoroutine(squareCorutineAiMove());

        //Seek Ai���� �ʿ��� ��ҵ��� �ٴ� �ڷ�ƾ �Լ��� ȣ�� 
        StartCoroutine(SeekAiready());

        //Seek Ai�� �����ӿ� ���� �ڷ�ƾ �Լ�
        StartCoroutine(seekAiMove());

    }

    IEnumerator SeekAiready()//Seek Ai���� �ʿ��� ��ҵ��� �ٴ� �ڷ�� �Լ�
    {
        yield return new WaitForSeconds(3);
        //Seek���� light�� �ܴ�
        GameObject.Instantiate(spotLightPrepab, seekob.transform.position, Quaternion.identity).transform.parent = seekob.transform;

        //Seek���� Scale�� Ű���
        seekob.transform.DOScale(new Vector3(10, 10, 10), 2);

        //Seek���� radar�� �ܴ�
        radar = GameObject.Instantiate(radar, seekob.transform.position, Quaternion.identity);

        //radar�� transform�� Seek Ai�� �ڽ����� ��ġ ��Ų��.
        radar.transform.parent = seekob.transform;

        //radar�� transform Scale�� 1,1,1�� �ٲ۴�.
        radar.transform.localScale = new Vector3(1, 1, 1);

        //animation�� �ȱ�� �ٲ۴�
        seekob.GetComponent<Animator>().SetBool("AImove", true);

        //seek Ai�� �����̰� �ϱ� ���� trigger
        seekAssignment = true;

        //Ai�� hide�� �߰�
        //aIObject.Add(player);


    }

    public void playerSeekAiMove()//playe�� seek�϶� Ai�� ������
    {
        player.tag = "Seek"; //player�� tag�� seek�� �ٲ�

        seekob = player;
        for (int o = 0; o < aIObject.Count; o++) //Ai���� �� �����ϰ� �ٲ�
        {
            aIObject[o].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = meterialclass[3].material;
        }

        //player�� scale�� Ű���
        player.transform.DOScale(new Vector3(10, 10, 10), 2);

        //�׸�ʿ� ���� �ڷ�� �Լ��� ȣ��
        StartCoroutine(squareCorutineAiMove());

        //play�� seek�� �� �ʿ��� ��ҵ��� �߰� ��Ű�� �ڷ�� �Լ��� ȣ��
        StartCoroutine(playerSeekready());

    }

    IEnumerator playerSeekready()//play�� seek�� �� �ʿ��� ��ҵ��� �߰� ��Ű�� �ڷ�� �Լ�
    {
        yield return new WaitForSeconds(3);
        //right�� �ܴ�
        GameObject.Instantiate(spotLightPrepab, player.transform.position, Quaternion.identity).transform.parent = player.transform;

        radar = GameObject.Instantiate(radar, player.transform.position, Quaternion.identity);
        radar.transform.parent = player.transform;
        radar.transform.localScale = new Vector3(1, 1, 1);

    }

    IEnumerator squareCorutineAiMove()//�׸�ʿ� ���� �ڷ�� �Լ�
    {
        for (int i = 0; i < aIObject.Count; i++)// AI ������ ��ġ�� �����̰� �Ѵ�.(�ʱⰪ)
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
            for (int i = 0; i < aIObject.Count; i++)// AI ������ ��ġ�� �����̰� �Ѵ�.
            {
                aIObject[i].GetComponent<NavMeshAgent>().SetDestination(squarePosition[Random.Range(0, 7)]);
                aIObject[i].GetComponent<Animator>().SetBool("AImove", true);
            }

        }
    }

   

}
