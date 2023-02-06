using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

//효과음이 모든 함수 내에 들어있는 상태
//뮤트가 제대로 작동 안되는 상태
//버튼이 아직 정리되지 않은 상태


public class UIManager : MonoBehaviour {
    public GameObject setting;
    public GameObject blinder;

    public GameObject muteChanger;
    public GameObject speaker;
    public GameObject muteSpeaker;
    public GameObject muteChangerBtn;

    public GameObject joystick;
    public GameObject touch;

    public GameObject shop;

    public GameObject name_Input;

    string nameds;

    public void Active_Sitting_UI() { //톱니바퀴(셋팅 버튼)를 눌렀을 때
        SoundManager.instance.PlaySE("UI Click Sound");
        if (setting.activeSelf == false) {
            setting.SetActive(true);
            blinder.SetActive(true);
        }
        else
            Unactive_Sitting_UI();
    }

    public void Mute_Mugic() { //소리 뮤트 / 조건문 : 버튼 위치(초록색, 회색)
        if (muteChangerBtn.GetComponent<RectTransform>().anchoredPosition.x == 50) {
            AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
            muteChangerBtn.GetComponent<RectTransform>().DOAnchorPosX(-50, 0.5f);
            speaker.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            muteSpeaker.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            muteChanger.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        }
        else if (muteChangerBtn.GetComponent<RectTransform>().anchoredPosition.x == -50) {
            AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
            muteChangerBtn.GetComponent<RectTransform>().DOAnchorPosX(50, 0.5f);
            speaker.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            muteSpeaker.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            muteChanger.GetComponent<Image>().color = new Color32(0, 220, 3, 225);
        }
    }

    public void Unactive_Sitting_UI() { //셋팅 UI이가 SetActive(false)
        SoundManager.instance.PlaySE("UI Click Sound");
        setting.SetActive(false);
        blinder.SetActive(false);
    }

    public void On_Cleck_Joystickh() { //설정창의 조이스틱 버튼을 누르면 초록색으로 바뀌는 스크립트
        SoundManager.instance.PlaySE("UI Click Sound");
        joystick.GetComponent<Image>().color = new Color32(0, 220, 3, 255);
        touch.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
    }

    public void On_Cleck_Touch() { //설정창의 터치 버튼을 누르면 초록색으로 바뀌는 스크립트
        SoundManager.instance.PlaySE("UI Click Sound");
        joystick.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
        touch.GetComponent<Image>().color = new Color32(0, 220, 3, 255);
    }

    public void Active_Shop() {
        SoundManager.instance.PlaySE("UI Click Sound");
        shop.SetActive(true);
    }

    public void UnActive_Shop() {
        SoundManager.instance.PlaySE("UI Click Sound");
        shop.SetActive(false);
    }

    public void Input_Name() {
        Debug.Log("값이 변함");
    }
}
