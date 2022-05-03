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

        //AIscript 값을 beCaughtAi로 설정
        beCaughtAi = GameObject.Find("Game Manager").GetComponent<AIScript>();

        //Game Manager에 있는 AIScript에 접근해서 meterialclass라는 class list의 4번째 인덱스에 접근한다.
        mat = GameObject.Find("Game Manager").GetComponent<AIScript>().meterialclass[4].material;
    }


    void OnTriggerEnter(Collider col)//collider가 들어올 때
    {
        
        if(col.gameObject.tag == "Bot")//겹친 collider의 tag가 bot일 때
        {

            col.gameObject.tag = "Dead"; // tag를 dead로 바꾼다

            //색상을 갈색으로 바꾼다.(나중에 애니메이션으로 바뀔거임)
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat; //col의 자식값
            
            //Navmesh Agent의 speed 값을 0으로 한다
            col.GetComponent<NavMeshAgent>().speed = 0f;

            //Rigidbody에 position(x,y,z)의 값을 고정시킨다.
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
           
            //isTrigger를 true해서 물리작용을 하지 못하게 한다 
            col.GetComponent <CapsuleCollider>().isTrigger = true;

            //잡혔을 때 animation를 idle로 바꾼다
            col.GetComponent<Animator>().SetBool("AImove", false);

            //잡힌 Hide AI를 aIObject list에서 remove 한다.
            beCaughtAi.aIObject.RemoveAt(beCaughtAi.aIObject.FindIndex(x => x.gameObject == col.gameObject));
                

        }
        else if(col.gameObject.tag == "Player")//겹친 collider의 tag가 player일 경우
        {
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat;
        }
    }
}
