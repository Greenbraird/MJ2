using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class CameraManager : MonoBehaviour
{
    public Transform look_ob;
    public GameObject onPointer;
    public GameObject UI_mo;
    public GameObject gameStartTimerPanel;

    private bool isGameStartTimerPanelOnClick;
    private float timer;

<<<<<<< HEAD
    public Vector3 lookOffset = new Vector3(0, 90f, -73f);
    private void Awake()
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

        timer = 0;
    }

    private void Update()
    {
            
            
            if (0.95f<timer && timer < 1)
            {
                gameStartTimerPanel.GetComponent<Text>().text = "2";
                // gameStartTimerPanel.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0.5f).From().SetEase(Ease.InOutBounce);
                Debug.Log(timer);
            }
            else if (1.95f < timer && timer < 2)
            {
                gameStartTimerPanel.GetComponent<Text>().text = "1";
                //gameStartTimerPanel.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0.5f).From().SetEase(Ease.InOutBounce);
                Debug.Log(timer);
             }

            else if (2.95f < timer && timer < 3)
            {
                gameStartTimerPanel.GetComponent<Text>().text = "0";
                Debug.Log(timer);
                //gameStartTimerPanel.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0.5f).From().SetEase(Ease.InOutBounce);
            }

            else if (0 < timer && timer < 0.1f)
            {
                gameStartTimerPanel.GetComponent<Text>().text = "3";
            Debug.Log(timer);
                //gameStartTimerPanel.GetComponent<RectTransform>().DOScale(new Vector3(0, 0, 0), 0.5f).From().SetEase(Ease.InOutBounce);
            }

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

    void Timer()
    {
        DOTween.To(() => timer, x => timer = x, 3,3);
        StartCoroutine(OnPenelSetActive());
    }

    IEnumerator OnPenelSetActive()
    {
        yield return new WaitForSeconds(3);
        gameStartTimerPanel.SetActive(false);
        onPointer.SetActive(true);
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
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 7.5f, 1);
        look_ob.transform.DOScale(new Vector3(15, 15, 15), 2);
        Timer();
        StartCoroutine(Gamestrat()); 
        gameStartTimerPanel.SetActive(true);
        UI_mo.SetActive(false);

    }
    public void Hide_On_Click()
    {
        transform.position = look_ob.position + lookOffset;
        start_flag = true;

        SoundManager.instance.PlaySE("UI Click Sound");
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        onPointer.SetActive(true);
        Timer();
        gameStartTimerPanel.SetActive(true);
        UI_mo.SetActive(false);
    }


    IEnumerator Gamestrat()
    {
        yield return new WaitForSeconds(3);
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
        
    }
}
