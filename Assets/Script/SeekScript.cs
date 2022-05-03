using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SeekScript : MonoBehaviour
{
    Material mat;
    AIScript beCaughtAi;
    void Start()
    {

        //AIscript ���� beCaughtAi�� ����
        beCaughtAi = GameObject.Find("Game Manager").GetComponent<AIScript>();

        //Game Manager�� �ִ� AIScript�� �����ؼ� meterialclass��� class list�� 4��° �ε����� �����Ѵ�.
        mat = GameObject.Find("Game Manager").GetComponent<AIScript>().meterialclass[4].material;
    }


    void OnTriggerEnter(Collider col)//collider�� ���� ��
    {
        
        if(col.gameObject.tag == "Bot")//��ģ collider�� tag�� bot�� ��
        {

            col.gameObject.tag = "Dead"; // tag�� dead�� �ٲ۴�

            //������ �������� �ٲ۴�.(���߿� �ִϸ��̼����� �ٲ����)
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat; //col�� �ڽİ�
            
            //Navmesh Agent�� speed ���� 0���� �Ѵ�
            col.GetComponent<NavMeshAgent>().speed = 0f;

            //Rigidbody�� position(x,y,z)�� ���� ������Ų��.
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
           
            //isTrigger�� true�ؼ� �����ۿ��� ���� ���ϰ� �Ѵ� 
            col.GetComponent <CapsuleCollider>().isTrigger = true;

            //������ �� animation�� idle�� �ٲ۴�
            col.GetComponent<Animator>().SetBool("AImove", false);

            //���� Hide AI�� aIObject list���� remove �Ѵ�.
            beCaughtAi.aIObject.RemoveAt(beCaughtAi.aIObject.FindIndex(x => x.gameObject == col.gameObject));
                

        }
        else if(col.gameObject.tag == "Player")//��ģ collider�� tag�� player�� ���
        {
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat;
        }
    }
}
