using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public Transform look_ob;
    public GameObject onPointer;
    public GameObject UI_mo;
    public TextMeshProUGUI delayGameStrat;
    private int Delay = 3;

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
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 7.5f, 2);
        look_ob.transform.DOScale(new Vector3(15, 15, 15), 2);
        UI_mo.SetActive(false);
        StartCoroutine(Gamestrat());
        Delay_Strat();

    }
    public void Hide_On_Click()
    {
        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        UI_mo.SetActive(false);
        Delay_Strat();
    }

    public void Delay_Strat()
    {
        for (int i = 0; i < 4; i++)
        {
            delayGameStrat.text = Delay.ToString();
            delayGameStrat.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 1).From().SetEase(Ease.OutBounce).SetDelay(i);
            Delay--;
        }
    }

    IEnumerator Gamestrat()
    {
        yield return new WaitForSeconds(3);
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
    }
}
