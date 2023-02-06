using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadME : MonoBehaviour { 
}

/// C : Change
/// T : SetActive(True)
/// F : SetActive(False)

/// 
/// [1] class CameraManager
/// 
/// Awake : ī�޶� ����
/// look_ob : player
/// 
/// 
/// [2] ���� ����
/// 
/// [ MapNum c# ]
/// --> MAP��ȣ(C)
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// [GameManager c#] Unity : GameManager
/// --> List<GameObject> aIObject : AI bot
/// --> List<Material> material   : AI material, Player material
/// --> MAP : SquarePosition, HexagonPosition, CirclePosition
/// --> Colider : spotLightPrepab, radar, radar_bot, player_hide
/// --> IsGameOver : AIScript.update() : IsGameOver = true, false : 
/// --> pannel : win_, lose_, gotonextmap
/// 
/// --> gotonextmap onclick : 
///     GoNextStage() : �÷��̾ �ٽ� �����̰� �Ѵ�, �����ϰ� stage�� �����Ѵ�, 
///                     MapNum.map_num_text(c), MapNum.stage_cnt++
/// 
/// --> Update() : if IsGameOver // if IsLose : PlayerLose() : lose_, gotonextmap | if !IsLose : PlayerWin() : win_, gotonextmap
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// [ AIScript c# ]
/// --> int AIcount
/// 
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// (1) [ Hide ]
/// Unity : HideBtn Onclick : CameraManager.Hide_On_Click() --> Camera(C), UICanvas(F), JoystickPanel(T), TimerPanel(T) : 3��, 
/// Unity : HideBtn Onclick : AIScript.playerHideAIMove()   --> AIHideTag(C), AISeekTag(C), Collider(AIHide / AISeek / Player)���̱�
/// Unity : HideBtn Onclick : TimerScript.TimerStart() : 100��
/// 
/// --> AI     / 5 / Hide  / Collider : GameManager.radar_bot
/// --> AI     / 1 / Seek  / Collider : GameManager.radar , GameManager.spotLightPrepab     / �ڵ� Seek on / 
/// --> Player / 1 / Hide  / Collider : GameManager.player_hide
/// 
/// 
/// 
/// AIScript.playerHideAIMove() : 
/// 1. �������� Ai�� �ϳ��� Seek�� ��ȯ : AI�� tag�� Seek�� ��ȯ
/// 2. Hide Ai�� ��� Bot �ݶ��̴��� �޾��ش�
/// 3. player���� Hide Player �ݶ��̴��� �޾��ش�
/// 4. �ڷ�ƾ �Լ� ����
/// 4-1. SeekAiready() : Seek Ai���� �ʿ��� ��� ����
/// 
/// 4-2. Square Map ��ǥ ���� ������ ��ġ�� �̵� : Bot���� ��� Ȱ��ȭ�Ѵ�, AI ������ ��ġ�� �����̰� �Ѵ�.
/// 4-3. seekAiMove()
/// AIScript.Update() : ���� �������ǰ� Seek�� seekob�� �������� �i���� �Ѵ�
/// 
/// [ AIcount ] : 6
/// 
/// GameManager.player_hide(playercolider) // (1 ����) if : col.gameObject.tag == "Dead" : AIScript.AIcount++
/// GameManager.radar(Seek)                // (2 ����) if : col.gameObject.tag == "Bot"  : AIScript.AIcount--, | if : col.gameObject.tag == "Player" : none
/// GameManager.radar_bot(Bot : Hide)      // (3 ����) if : col.gameObject.tag == "Dead" : AIScript.AIcount++ | if : col.gameObject.tag == "Bot" : none | if : col.gameObject.tag == "Player"
/// 
/// if : AIcount == 0 && Seek.isPlayerIsSeek == true (Player Hide) : GameManager.IsGameOver = true;
/// 
/// 
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// 
/// (2) [ Seek ]
/// Unity : SeekBtn Onclick : CameraManager.Seek_On_Click() --> Camera(C), UICanvas(F), JoystickPanel(T), TimerPanel(T) : 3��, 
/// Unity : SeekBtn Onclick : AIScript.playerSeekAiMove()   --> AIHideTag(C), AISeekTag(N), PlayerTag(C), Collider(AIHide / PlayerSeek)���̱�
/// Unity : SeekBtn Onclick : TimerScript.TimerStart() : 100��
/// 
/// --> AI     / 5 / Hide  / Collider : GameManager.radar_bot
/// --> AI     / 1 / Hide  / Collider : GameManager.radar_bot / �ڵ� Seek off
/// --> Player / 1 / Seek  / Collider : GameManager.radar , GameManager.spotLightPrepab 
/// 
/// 
/// AIScript.playerSeekAiMove() : 
/// 
/// AIScript.Update() : ���� �������ǰ� Seek�� seekob�� �������� �i���� �Ѵ�
/// 
/// if : AIcount == 0 && Seek.isPlayerIsSeek == false (Player Seek) : GameManager.IsGameOver = true;
/// 
/// 
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// 
/// 
/// Timer : ��ư�� ������ ���� �� ���۵Ǵ� 3��
/// 2. AIScript
/// MAP �ٲٱ�, �ð� �ʱ�ȭ�ϱ�, �̱�� ���� ���� �����
/// 
/// 
/// 
/// 
/// 















///
/// 
/// GameManager 164 : for�� �� i = 6 :AICount�� �ؾ����� ����ؾ���
///
/// 



