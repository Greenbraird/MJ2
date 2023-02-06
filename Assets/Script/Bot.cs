using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Hide
public class Bot : MonoBehaviour {

    /*    
    GameManager beCaughtAi;
    void Start() {
        beCaughtAi = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }*/

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Dead") {

            AIScript.AIHide_Counting++;

            col.gameObject.tag = "Bot";
            Debug.Log("Live AIHide가 Dead AI Hide를 풀어줌");

            /// [ AIHide ] : change AIHide Material : (0) Hide_Material
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[0];

            /// [ AIHide ] : AIHide XYZ Freeze(OFF)
            col.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;

            /// [ AIHide ] : AIHide Animation(ON)
            col.GetComponent<Animator>().SetBool("AImove", true);

            /// [ AIHide ] : AIHide Speed(ON)
            col.GetComponent<NavMeshAgent>().speed = 3f;
        }

        else if (col.gameObject.tag == "Bot") { 

            Debug.Log("Live AIHide끼리 충돌함");

        } 
        
        else if (col.gameObject.tag == "Player") {

            Debug.Log("Live AIHide가 Player와 충돌함");

            /// [ Player(Hide) ] : change Player Material : (0) Hide_Material
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[0];

            /// [ Player(Hide) ] : player can move
            Joyscript.IsAlive = true;
            GameManager.IsGameOver = false;
        } 
    }

}
