using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SeekScript : MonoBehaviour
{
    Material mat;
    void Start()
    {
        mat = GameObject.Find("Game Manager").GetComponent<AIScript>().meterialclass[4].material;
    }


    void OnTriggerEnter(Collider col)
    {
        
        if(col.gameObject.tag == "Bot")
        {
            col.gameObject.tag = "Dead";
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat;
            col.GetComponent<NavMeshAgent>().speed = 0f;
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            col.GetComponent <CapsuleCollider>().isTrigger = true;
            

        }
        else if(col.gameObject.tag == "Player")
        {
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mat;
        }
    }
}
