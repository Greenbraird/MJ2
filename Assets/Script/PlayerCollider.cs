using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Player(Hide)
public class PlayerCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider col) { 

        if (col.gameObject.tag == "Dead") {
            
            AIScript.AIHide_Counting++;

            col.gameObject.tag = "Bot";
            Debug.Log("Player가 Dead AIHide를 풀어주었습니다.");

            /// [ AIHide ] : change AIHide Material : (0) Hide_Material
            col.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[0];

            /// [ AIHide ] : AIHide XYZ Freeze(OFF)
            col.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;

            /// [ AIHide ] : AIHide Animation(ON)
            col.GetComponent<Animator>().SetBool("AImove", true);

            /// [ AIHide ] : AIHide Speed(ON)
            col.GetComponent<NavMeshAgent>().speed = 3f;
        }
    }
}
