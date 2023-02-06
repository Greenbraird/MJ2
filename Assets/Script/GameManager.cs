using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;            ///�ִϸ��̼� �ε巴�� ������ִ� ��
using TMPro;                  ///�ؽ�Ʈ ����ϰ� ������ִ� ��

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;


    /*                                          [ AI Bot ], [ Player ], [ Material ], [ Collider ]                                        */
    /// Declaration
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    public static List<GameObject> aIObject;
    public static GameObject player;
    public static List<Material> material;

    void Start() {
        instance = this;

        Debug.Log("START NEW GAME : MAP NUM " + MapNum.stage_cnt);

        /// "AI Bot" ( Hide + Seek )
        aIObject = new List<GameObject>() {
            GameObject.Find("Game Manager/Bot1"),
            GameObject.Find("Game Manager/Bot2"),
            GameObject.Find("Game Manager/Bot3"),
            GameObject.Find("Game Manager/Bot4"),
            GameObject.Find("Game Manager/Bot5"),
            GameObject.Find("Game Manager/Bot6"),
            GameObject.Find("Game Manager/Bot7")
        };

        /// "player"
        player = GameObject.Find("Game Manager/player");

        /// "Material"
        material = new List<Material>() {
            Resources.Load<Material>("Material/Hide_Material"),
            Resources.Load<Material>("Material/Seek_Material"),
            Resources.Load<Material>("Material/transparent"),
            Resources.Load<Material>("Material/Dead_Material")
        };
    }

    /// "Collider"
    public GameObject spotLightPrepab;     /// spotLight
    public GameObject radar;               /// Seek Collider
    public GameObject radar_bot;           /// Bot Collider
    public GameObject player_hide;         /// Hide player Collider





    /*                                           [ MAP AI AI Moving Spot Position ]                                                       */
    /// Declaration
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    /// "Square Map AI Moving Spot"
    public static List<Vector3> squarePosition = new List<Vector3>() {
        new Vector3(-14, 0.5f, -14),
        new Vector3( 16, 0.5f, -14),
        new Vector3( 12, 0.5f, -15),
        new Vector3( 15, 0.5f,  -2),
        new Vector3( 15, 0.5f,  15),
        new Vector3(-16, 0.5f,  15),
        new Vector3(-16, 0.5f,   0),
        new Vector3( -8, 0.5f,  -8),
        new Vector3(  4, 0.5f,   8)
    };

    /// "Hexagon Map AI Moving Spot"
    public static List<Vector3> hexagonPosition = new List<Vector3>() {
        new Vector3( -13, 0.5f, -4),
        new Vector3( -19, 0.5f,  5),
        new Vector3( -2, 0.5f,   7),
        new Vector3( -2, 0.5f, -14),
        new Vector3( 11, 0.5f,  -4),
        new Vector3(  1, 0.5f,  18),
        new Vector3(3, 0.5f,   0),
        new Vector3( 10, 0.5f,  7),
        new Vector3(  4, 0.5f,   8)
    };

    /// "Circle Map AI Moving Spot"
    public static List<Vector3> circlePosition = new List<Vector3>() {
        new Vector3(-16, 0.5f,  -4),
        new Vector3(-15, 0.5f,   5),
        new Vector3( -2, 0.5f, 8),
        new Vector3( -8, 0.5f, -11),
        new Vector3(  3, 0.5f, -13),
        new Vector3( 11, 0.5f,  -2),
        new Vector3( 11, 0.5f,   7),
        new Vector3( -8, 0.5f,  -8),
        new Vector3(  4, 0.5f,   8)
    };



    /*                                                      [ GAME START ]                                                                */
    /// Declaration
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    public GameObject joystick_panel;       /// joystick
    public GameObject start_screen_panel;   /// UI canvas
    public GameObject startTimer_panel;     /// delayText (���� ���� ���� �� ó�� ������ ū �ؽ�Ʈ)

    public GameObject InGameTimer_panel;    /// Image, Timer


    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
    /// HideBtn_Onclick() or SeekBtn_Onclick()

    public void GameStart() {
        /// sound(play ON)
        SoundManager.instance.PlaySE("UI Click Sound");

        /// Timer panel(ON)
        startTimer_panel.SetActive(true);

        /// Start screen (OFF)
        start_screen_panel.SetActive(false);
    }

    /****[ HIDE ]**************************************************************************************************************************/
    /// Unity : UICanvas.HideBtn(Onclick)

    public void HideBtn_Onclick() {

        // player can move  / if : player(Hide)
        joystick_panel.SetActive(true);

        GameStart();    /// Game Canvas, Panel(ON/OFF)
        Timer();        /// Time for Hide : function

        /// change Camera position
        /// Unity : UICanvas.SeekBtn(Onclick) : CameraManager.Camera_Hide();

        /// Unity : UICanvas.SeekBtn(Onclick) : AIScript.playerHideAIMove();
    }

    /****[ SEEK ]**************************************************************************************************************************/
    /// Unity : UICanvas.SeekBtn(Onclick)

    public void SeekBtn_Onclick() {

        GameStart();    /// Game Canvas, Panel(ON/OFF)
        Timer();        /// Time for Hide : function
        
        /// change Camera position
        /// Unity : UICanvas.SeekBtn(Onclick) : CameraManager.Camera_Seek();

        /// Unity : UICanvas.SeekBtn(Onclick) : AIScript.playerSeekAiMove();
    }

    /****[ START TIMER ]*******************************************************************************************************************/
    /// Time for Hide : function

    public void Timer() {
        GMTimer.GMupdateTime = true;
        StartCoroutine(OnPanelSetActive());
    }
    IEnumerator OnPanelSetActive() {

        /// *coroutine condition
        yield return new WaitForSeconds(3);

        /// Timer panel(OFF) After Time off
        startTimer_panel.SetActive(false);

        // player can move After Time off  / if : player(Seek)
        joystick_panel.SetActive(true);

        /// Timer On
        InGameTimer_panel.SetActive(true);

        /// Start In Game Timer
        TimerScript.updateTime = true;
    }




    /*                                                        [ IN GAME ]                                                                 */
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    /// AIScript : gameover (case 1)
    /// 
    /// Collider : Seek (for AISeek & Player(Seek)) : gameover(case 3)
    /// Collider : Bot  (for AIHide)
    /// Collider : PlayerCollider  (for Player(Hide))
    /// 
    /// /// GameManager.player_hide(playercolider) // (1 ����) if : col.gameObject.tag == "Dead" : AIScript.seeking_AI++
    /// /// GameManager.radar(Seek)                // (2 ����) if : col.gameObject.tag == "Bot"  : AIScript.seeking_AI--, | if : col.gameObject.tag == "Player" : none
    /// /// GameManager.radar_bot(Bot : Hide)      // (3 ����) if : col.gameObject.tag == "Dead" : AIScript.seeking_AI++ | if : col.gameObject.tag == "Bot" : none | if : col.gameObject.tag == "Player"

    /// 
    /// MapNum
    /// 
    /// Joyscript
    /// TimerScript : gameover / fillAmountImg
    /// 
    /// SoundManager
    /// UIManager
    /// CameraManager
    /// 
    /// three scenes : square, hexagon, circle




    /*                                      [ GAME OVER ], [ WIN / LOSE ], [ Next Stage ]                                                 */
    /// Declaration
    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    /// "GAME OVER"  : Identification Bool : GameManager.update() <-- (ON/OFF)
    public static bool IsGameOver = false;

    /// "WIN / LOSE"  : Panel
    public GameObject win_;
    public GameObject lose_;

    /// "WIN / LOSE"  : Identification Bool : GameManager.update() --> PlayerWin()/PlayerLose() 
    public static bool IsWin = false;

    /// "Next Stage"  : Button : Unity.Onclick --> GameManager.GoNextStage() 
    public GameObject gotonextmap;

    /// "Next Stage"  : Identification number : GameManager.GoNextStage() --> Scene index 
    public int stage = 0;



    /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/

    /****[ GAME OVER ]*********************************************************************************************************************/

    /// If : Game Over : WIN of Lose & Go to Next Stage!
    void Update() {

        /// IsGameOver(true)
        /// case 1 : All AIHide & player(Hide) founded / AIScript : AIHide_Counting & player(Hide) : Time --> 0 / TimerScript : Update() 
        /// case 2 : Time --> 0 / TimerScript : Update()
        /// case 3 : player founded / Seek : col.gameObject.tag == "Player"

        if (IsGameOver) {

            TimerScript.updateTime = false;
            //AIScript.AIHide_Counting = 7;

            if (IsWin) PlayerWin(); 
            else PlayerLose();
        } else {
            lose_.SetActive(false);
            win_.SetActive(false);
            gotonextmap.SetActive(false);
        }

    }

    /****[ WIN / LOSE ]********************************************************************************************************************/


    /** "WIN / LOSE"  : Function **/

    /// Panel(ON), Button(ON)
    public void PlayerWin() {
        win_.SetActive(true);
        gotonextmap.SetActive(true);
    }

    /// Panel(ON), Button(ON)
    public void PlayerLose() {
        lose_.SetActive(true);
        gotonextmap.SetActive(true);
    }


    /****[ Next Stage ]*********************************************************************************************************************/


    /** "Next Stage"  : Function **/

    /// if : Unity.Onclick(gotonextmap)
    public void GoNextStage() {


        //���� ��ġ�� ---------------------- �� ���� ��� ���� ������?
        /// GameManager.Update(OFF)
        IsGameOver = false;

        /// Panel(OFF), Button(OFF)
        lose_.SetActive(false);
        win_.SetActive(false);
        gotonextmap.SetActive(false);


        /// Player XYZ Freeze(OFF)
        Joyscript.IsAlive = true;

        for (int i = 0; i < 6; i++) {

            /// Bot XYZ Freeze(OFF)
            aIObject[i].GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeAll;

            /// Bot Animation(ON)
            aIObject[i].GetComponent<Animator>().SetBool("AImove", true);

            /// Bot Speed(ON)
            aIObject[i].GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3f;
        
        }

        //������� ------------------------- �� ���� ��� ���� ������?

        /// Randon Next Stage : LoadScene / Scene change
        stage = Random.Range(0, 100) % 3;
        if(stage == 0)
            SceneManager.LoadScene("Square");
        else if (stage == 1)
            SceneManager.LoadScene("Circle");
        else if (stage == 2)
            SceneManager.LoadScene("Hexagon");

        /// Map counting number(UP)
        MapNum.stage_cnt++;
    }

    


}
