using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public Transform look_ob;
    public GameObject joy_s;
    public GameObject UI_mo;

    public Vector3 lookOffset = new Vector3(0, 90f, -73f);
    private void LateUpdate()
    {
        transform.position = look_ob.position + lookOffset;

        transform.LookAt(look_ob);
    }
    public void Seek_On_Click()
    {
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 20, 2);
        joy_s.SetActive(true);
        UI_mo.SetActive(false);

    }
    public void Hide_On_Click()
    {
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        joy_s.SetActive(true);
        UI_mo.SetActive(false);
    }
}
