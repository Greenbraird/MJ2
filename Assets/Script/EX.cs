using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX : MonoBehaviour
{
    public RectTransform start;
    public RectTransform end;
    void Update()
    {
        Vector2 v2 = end.position - start.position;
        Debug.Log(Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg);
    }


}
