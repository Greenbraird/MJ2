using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Seek
public class Seek : MonoBehaviour {

    void OnTriggerEnter(Collider col) { 

        if (col.gameObject.tag == "Bot") {

            AIScript.AIHide_Counting--;

            col.gameObject.tag = "Dead";
            Debug.Log("술래가 Bot을 잡음");

            /// [ AIHide ] : change AIHide Material : (3) Dead_Material
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[3];

            /// [ AIHide ] : AIHide XYZ Freeze(ON)
            col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            /// [ AIHide ] : AIHide Animation(OFF)
            col.GetComponent<Animator>().SetBool("AImove", false);

            /// [ AIHide ] : AIHide Speed(OFF)
            col.GetComponent<NavMeshAgent>().speed = 0f;

            ///isTrigger를 true해서 물리작용을 하지 못하게 한다 
            ///col.GetComponent <CapsuleCollider>().isTrigger = true;

            ///잡힌 Hide AI를 aIObject list에서 remove 한다.
            ///beCaughtAi.aIObject.RemoveAt(beCaughtAi.aIObject.FindIndex(x => x.gameObject == col.gameObject));
        } 
        
        else if (col.gameObject.tag == "Player") {

            Debug.Log("술래가 player를 잡았습니다.");

            /// [ Player(Hide) ] : change Player Material : (3) Dead_Material
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[3];

            /// [ Player(Hide) ] : player cannot move
            Joyscript.IsAlive = false;

            ///case 3 : Lose
            GameManager.IsGameOver = true;
            GameManager.IsWin = false;
        }
    }
}
