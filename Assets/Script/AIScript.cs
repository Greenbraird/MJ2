using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using DG.Tweening;

// 업그레이드 : AI 수 선택하기

public class AIScript : MonoBehaviour {

    /// Declaration
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    /// Colider instance for AISeek, AIHide, Player(Hide/Seek)
    private GameObject SeekCollider;
    private GameObject BotCollider;
    private GameObject PlayerCollider;

    /// Identification number (AIHide number) : AIScript.Update() : GameManager.IsGameOver(true/false)
    public static int AIHide_Counting = 7;

    /// Choose one AISeek in GameManager.aIObject 
    private GameObject AISeek;
    public static bool isPlayerIsSeek = false;

    /// Trigger for AISeek Seeking & seeking_AI
    private bool AISeek_Seek_trigger = false;
    private int seeking_AI;

    /*                                                        [ IN GAME ]                                                                 */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    void Start() {
        for (int i = 0; i < 6; i++) {
            /// [ AIHide ] : AIHide XYZ Freeze(ON)
            GameManager.aIObject[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            /// [ AIHide ] : AIHide Animation(OFF)
            GameManager.aIObject[i].GetComponent<Animator>().SetBool("AImove", false);


        }
    }

    void Update() {

        /// Player(Hide) : case 1, Lose
        if (AIHide_Counting == 0 && isPlayerIsSeek == false) {
            if (Joyscript.IsAlive == false) {
                Debug.Log("Player(Hide) : player와 모든 도둑이 잡혔습니다 Lose");
                GameManager.IsGameOver = true;
                GameManager.IsWin = false;
            }
        }

        /// Player(Seek) : case 1, Win
        else if (AIHide_Counting == 0 && isPlayerIsSeek == true) {
            Debug.Log("Player(Hide) : player가 모든 도둑을 다 잡았습니다.");
            GameManager.IsGameOver = true;
            GameManager.IsWin = true;
        }

        if(AISeek_Seek_trigger) {
            try {
                AISeek.GetComponent<NavMeshAgent>().SetDestination(GameManager.aIObject[seeking_AI].transform.position);
            } catch (System.Exception) {
                seeking_AI = Random.Range(0, AIHide_Counting);
                //AISeek.GetComponent<NavMeshAgent>().SetDestination(GameManager.aIObject[seeking_AI].transform.position);
            }
        }
    }

    /*                                                       [ PLAYER HIDE ]                                                              */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    ///UICanvas.HideBtn(Onclick)
    public void playerHideAIMove() { 

        /// Player(Hide)
        isPlayerIsSeek = false;


        // [ AISeek ]      **************************
        
        /// Choose one AISeek Random in GameManager.aIObject 
        int randomN = Random.Range(0, AIHide_Counting);
        AISeek = GameManager.aIObject[randomN];
        GameManager.aIObject.RemoveAt(randomN);
        AISeek.tag = "Seek";
        AIHide_Counting = GameManager.aIObject.Count;


        // [ Player(Hide) ] **************************

        /// Player + Collider(Hide : Bot)
        PlayerCollider = GameObject.Instantiate(GameManager.instance.player_hide, GameManager.player.transform.position, Quaternion.identity);

        /// PlayerCollider.transform : Player 's child
        PlayerCollider.transform.parent = GameManager.player.transform;

        /// change Player(Hide) Material : (0) Hide_Material
        GameManager.player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[0];


        // [ AIHide ]       **************************
        AIHideReady();

        StartCoroutine(CorutineAiMove());

        // [ AISeek ]       **************************
        SeekReady();
        StartCoroutine(seekAiMove());
    }

    /*                                                     [ PLAYER SEEK ]                                                                */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    /// UICanvas.SeekBtn(Onclick)
    public void playerSeekAiMove() {

        // Player(Seek)
        isPlayerIsSeek = true;


        // [ Player(Seek) ] **************************
        AISeek = GameManager.player;
        ///GameManager.aIObject.RemoveAt(6);  /// Remove Last AI Object
        AISeek.tag = "Seek";
        AIHide_Counting = GameManager.aIObject.Count;
        SeekReady();


        // [ AIHide ]       **************************
        AIHideReady();

        /// change AIHide Material : (2) transparent
        for (int i = 0; i < AIHide_Counting; i++)
            GameManager.aIObject[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[2];

        StartCoroutine(CorutineAiMove());

    }


    /*                                                         [ AIHide ]                                                                 */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    public void AIHideReady() {
        for (int i = 0; i < AIHide_Counting; i++) {

            /// [ AIHide ] : AIHide + Collider(Bot)
            BotCollider = GameObject.Instantiate(GameManager.instance.radar_bot, GameManager.aIObject[i].transform.position, Quaternion.identity);

            /// [ AIHide ] : BotCollider.transform : aIObject 's child
            BotCollider.transform.parent = GameManager.aIObject[i].transform;

            /// [ AIHide ] : change AIHide Material : (0) Hide_Material
            GameManager.aIObject[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[0];

            /// [ AIHide ] : AIHide XYZ Freeze(OFF)
            GameManager.aIObject[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;

            /// [ AIHide ] : AIHide Animation(ON)
            GameManager.aIObject[i].GetComponent<Animator>().SetBool("AImove", true);

            /// [ AIHide ] : AIHide Speed(ON)
            GameManager.aIObject[i].GetComponent<NavMeshAgent>().speed = 3f;
        }
    }
    
    
    /// Move AIHide Randomly by Square Map Point
    IEnumerator CorutineAiMove() {
        /// Pick Point Randomly in Square Map each AIHide
        while (true) {
            for (int i = 0; i < AIHide_Counting; i++) {
                yield return new WaitForSeconds(Random.Range(0, 2));

                Scene scene = SceneManager.GetActiveScene();
                if (scene.name == "Square") {
                    int Position = Random.Range(0, GameManager.squarePosition.Count);
                    /// move AIHide to point
                    GameManager.aIObject[i].GetComponent<NavMeshAgent>().SetDestination(GameManager.squarePosition[Position]);
                } else if (scene.name == "Circle") {
                    int Position = Random.Range(0, GameManager.circlePosition.Count);
                    /// move AIHide to point
                    GameManager.aIObject[i].GetComponent<NavMeshAgent>().SetDestination(GameManager.circlePosition[Position]);
                } else if (scene.name == "Hexagon") {
                    int Position = Random.Range(0, GameManager.hexagonPosition.Count);
                    /// move AIHide to point
                    GameManager.aIObject[i].GetComponent<NavMeshAgent>().SetDestination(GameManager.hexagonPosition[Position]);
                }
            }
        }
    }


    /*                                                       [ AI SEEK ]                                                                  */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    IEnumerator seekAiMove() {
        yield return new WaitForSeconds(3);

        seeking_AI = Random.Range(0, AIHide_Counting);

        AISeek_Seek_trigger = true; /// : SEEK seeking_AI / if : AISeek Exist
            
    }

    /*                                                        [ SEEK ]                                                                    */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    /// AISeek, Player(Seek)
    public void SeekReady() {

        /// Seek + seek Collider
        SeekCollider = GameObject.Instantiate(GameManager.instance.radar, AISeek.transform.position, Quaternion.identity);

        /// Seek.transform : Seek 's child
        SeekCollider.transform.parent = AISeek.transform;

        // change seek Collider transform scale (1,1,1)
        SeekCollider.transform.localScale = new Vector3(1, 1, 1);

        // Seek + spotLight
        GameObject.Instantiate(GameManager.instance.spotLightPrepab, AISeek.transform.position, Quaternion.identity).transform.parent = AISeek.transform; //여기 이상할 수 있음

        // Seek scale(Up)
        AISeek.transform.DOScale(new Vector3(10, 10, 10), 2);

        /// change Seek Material : (1) Seek_Material
        AISeek.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.material[1];


        if (AISeek != GameManager.player) {
            /// Seek XYZ Freeze(OFF)
            AISeek.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;

            /// Seek Animation(ON)
            AISeek.GetComponent<Animator>().SetBool("AImove", true);

            /// Seek Speed(ON)
            AISeek.GetComponent<NavMeshAgent>().speed = 2.5f;

            Debug.Log("Bot is Seek!");
        }
        else{

            Debug.Log("player is Seek!");
            /// Player XYZ Freeze(OFF)
            Joyscript.IsAlive = true;
        }
    }
}
