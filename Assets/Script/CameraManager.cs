using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public Transform look_ob;
    public GameObject onPointer;
    public GameObject UI_mo;

<<<<<<< HEAD
    public Vector3 lookOffset = new Vector3(0, 90f, -73f);

    void Start()
    {
        transform.position = new Vector3(0, 0, 0) + lookOffset;
        

=======
    bool start_flag = false;

    public Vector3 lookOffset = new Vector3(0, 90f, -73f); 
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0) + lookOffset;
>>>>>>> 8c404fb187dd8626e1366a38a49d800c4b9ea8f5
        transform.LookAt(GameObject.Find("Map").GetComponent<Transform>());
    }
    private void LateUpdate()
    {
<<<<<<< HEAD
        if(Camera.main.fieldOfView < 50)
        {
            transform.position = look_ob.position + lookOffset;
            transform.LookAt(look_ob);
        }
    }
    public void Seek_On_Click()
    {
        
=======
        if(start_flag)
            transform.LookAt(look_ob);
    }
    public void Seek_On_Click()
    {
        transform.position = look_ob.position + lookOffset;
        start_flag = true;

>>>>>>> 8c404fb187dd8626e1366a38a49d800c4b9ea8f5
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        UI_mo.SetActive(false);

    }
    public void Hide_On_Click()
    {
        transform.position = look_ob.position + lookOffset;
        start_flag = true;

        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        UI_mo.SetActive(false);
    }
}
