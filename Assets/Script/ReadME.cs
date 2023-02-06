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
/// Awake : 카메라 셋팅
/// look_ob : player
/// 
/// 
/// [2] 게임 시작
/// 
/// [ MapNum c# ]
/// --> MAP번호(C)
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
///     GoNextStage() : 플레이어를 다시 움직이게 한다, 랜덤하게 stage를 선택한다, 
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
/// Unity : HideBtn Onclick : CameraManager.Hide_On_Click() --> Camera(C), UICanvas(F), JoystickPanel(T), TimerPanel(T) : 3초, 
/// Unity : HideBtn Onclick : AIScript.playerHideAIMove()   --> AIHideTag(C), AISeekTag(C), Collider(AIHide / AISeek / Player)붙이기
/// Unity : HideBtn Onclick : TimerScript.TimerStart() : 100초
/// 
/// --> AI     / 5 / Hide  / Collider : GameManager.radar_bot
/// --> AI     / 1 / Seek  / Collider : GameManager.radar , GameManager.spotLightPrepab     / 자동 Seek on / 
/// --> Player / 1 / Hide  / Collider : GameManager.player_hide
/// 
/// 
/// 
/// AIScript.playerHideAIMove() : 
/// 1. 랜덤으로 Ai중 하나를 Seek로 전환 : AI의 tag를 Seek로 전환
/// 2. Hide Ai에 모두 Bot 콜라이더를 달아준다
/// 3. player에게 Hide Player 콜라이더를 달아준다
/// 4. 코루틴 함수 실행
/// 4-1. SeekAiready() : Seek Ai에게 필요한 요소 지정
/// 
/// 4-2. Square Map 좌표 따라 랜덤한 위치로 이동 : Bot들을 모두 활성화한다, AI 랜덤한 위치로 움직이게 한다.
/// 4-3. seekAiMove()
/// AIScript.Update() : 게임 종료조건과 Seek인 seekob가 랜덤으로 쫒도록 한다
/// 
/// [ AIcount ] : 6
/// 
/// GameManager.player_hide(playercolider) // (1 가지) if : col.gameObject.tag == "Dead" : AIScript.AIcount++
/// GameManager.radar(Seek)                // (2 가지) if : col.gameObject.tag == "Bot"  : AIScript.AIcount--, | if : col.gameObject.tag == "Player" : none
/// GameManager.radar_bot(Bot : Hide)      // (3 가지) if : col.gameObject.tag == "Dead" : AIScript.AIcount++ | if : col.gameObject.tag == "Bot" : none | if : col.gameObject.tag == "Player"
/// 
/// if : AIcount == 0 && Seek.isPlayerIsSeek == true (Player Hide) : GameManager.IsGameOver = true;
/// 
/// 
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// 
/// (2) [ Seek ]
/// Unity : SeekBtn Onclick : CameraManager.Seek_On_Click() --> Camera(C), UICanvas(F), JoystickPanel(T), TimerPanel(T) : 3초, 
/// Unity : SeekBtn Onclick : AIScript.playerSeekAiMove()   --> AIHideTag(C), AISeekTag(N), PlayerTag(C), Collider(AIHide / PlayerSeek)붙이기
/// Unity : SeekBtn Onclick : TimerScript.TimerStart() : 100초
/// 
/// --> AI     / 5 / Hide  / Collider : GameManager.radar_bot
/// --> AI     / 1 / Hide  / Collider : GameManager.radar_bot / 자동 Seek off
/// --> Player / 1 / Seek  / Collider : GameManager.radar , GameManager.spotLightPrepab 
/// 
/// 
/// AIScript.playerSeekAiMove() : 
/// 
/// AIScript.Update() : 게임 종료조건과 Seek인 seekob가 랜덤으로 쫒도록 한다
/// 
/// if : AIcount == 0 && Seek.isPlayerIsSeek == false (Player Seek) : GameManager.IsGameOver = true;
/// 
/// 
/// 
/// 
/// --------------------------------------------------------------------------------------------------------------------------------------------------
/// 
/// 
/// Timer : 버튼을 누르면 게임 전 시작되는 3초
/// 2. AIScript
/// MAP 바꾸기, 시간 초기화하기, 이기고 지는 기준 세우기
/// 
/// 
/// 
/// 
/// 















///
/// 
/// GameManager 164 : for문 안 i = 6 :AICount로 해야할지 고민해야함
///
/// 



