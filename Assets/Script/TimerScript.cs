using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening; //�ִϸ��̼� �ε巴�� ������ִ� ��
using TMPro; //�ؽ�Ʈ ����ϰ� ������ִ� ��


public class TimerScript : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textmash;

    public float timer = 60f;
    public static bool updateTime = false;

    void Awake() {
        textmash = GetComponent<TextMeshProUGUI>();
    }
    
    void Update() {

        if (updateTime){
            timer -= Time.deltaTime;    
            textmash.text = ((int)timer + 1).ToString();
            if (timer <= 0) {
                timer = 60f;
                updateTime = false;
            }
        }

        if (timer == 0) {

            /// Player(Hide) : AIHide_Counting > 0, Win
            if (AIScript.AIHide_Counting > 0 && AIScript.isPlayerIsSeek == false) {
                GameManager.IsGameOver = true;
                GameManager.IsWin = true;
            }

            /// Player(Seek) : AIHide_Counting > 0, Lose
            else if (AIScript.AIHide_Counting > 0 && AIScript.isPlayerIsSeek == true) {
                GameManager.IsGameOver = true;
                GameManager.IsWin = false;
            }
        }
    }
}