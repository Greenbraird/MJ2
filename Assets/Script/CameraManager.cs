using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public Transform look_ob;
    public GameObject onPointer;
    public GameObject UI_mo;

    public Vector3 lookOffset = new Vector3(0, 90f, -73f);

    void Start()
    {
        transform.position = new Vector3(0, 0, 0) + lookOffset;
        

        transform.LookAt(GameObject.Find("Map").GetComponent<Transform>());
    }
    private void LateUpdate()
    {
        if(Camera.main.fieldOfView < 50)
        {
            transform.position = look_ob.position + lookOffset;
            transform.LookAt(look_ob);
        }
    }
    public void Seek_On_Click()
    {
        
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        UI_mo.SetActive(false);

    }
    public void Hide_On_Click()
    {
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        UI_mo.SetActive(false);
    }
}
