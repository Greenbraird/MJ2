using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    //Text Mesh Pro UHUI�� �ҷ��´�.
    private TextMeshProUGUI textmash;
    public int Timer;
    int delayTime;

    void Awake()
    {
        //textmash�� ���� ���ش�
        textmash = GetComponent<TextMeshProUGUI>();
        Timer = 40;
        delayTime = Timer;
    }
    void Update()
    {
        //Debug.Log(Timer);
        //�Ź� fram ���� textmash�� text�� �ʿ� �°� ���߾� �ش�.
        textmash.text = Timer.ToString();

        //Timer�� 0�̶�� TimerScene���� �ٽ� �����Ѵ�.
        if (Timer == 0)
        {
            SceneManager.LoadScene("TimerScene");
        }
    }

    public void TimerStart()
    {
        //�ڷ���� ���� 3�ʸ� ��ٷ� �ش�.
        StartCoroutine(TimerStratco());
    }

    IEnumerator TimerStratco()
    {
        yield return new WaitForSeconds(3);
        //Ÿ�̸Ӱ� �Ǵ� �������� ��ũ��Ʈ
        DOTween.To(() => Timer, x => Timer = x, 0, delayTime);
    }
}
