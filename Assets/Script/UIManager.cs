using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

//ȿ������ ��� �Լ� ���� ����ִ� ����
//��Ʈ�� ����� �۵� �ȵǴ� ����
//��ư�� ���� �������� ���� ����


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

    public void Active_Sitting_UI() { //��Ϲ���(���� ��ư)�� ������ ��
        SoundManager.instance.PlaySE("UI Click Sound");
        if (setting.activeSelf == false) {
            setting.SetActive(true);
            blinder.SetActive(true);
        }
        else
            Unactive_Sitting_UI();
    }

    public void Mute_Mugic() { //�Ҹ� ��Ʈ / ���ǹ� : ��ư ��ġ(�ʷϻ�, ȸ��)
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

    public void Unactive_Sitting_UI() { //���� UI�̰� SetActive(false)
        SoundManager.instance.PlaySE("UI Click Sound");
        setting.SetActive(false);
        blinder.SetActive(false);
    }

    public void On_Cleck_Joystickh() { //����â�� ���̽�ƽ ��ư�� ������ �ʷϻ����� �ٲ�� ��ũ��Ʈ
        SoundManager.instance.PlaySE("UI Click Sound");
        joystick.GetComponent<Image>().color = new Color32(0, 220, 3, 255);
        touch.GetComponent<Image>().color = new Color32(0, 0, 0, 100);
    }

    public void On_Cleck_Touch() { //����â�� ��ġ ��ư�� ������ �ʷϻ����� �ٲ�� ��ũ��Ʈ
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
        Debug.Log("���� ����");
    }
}
