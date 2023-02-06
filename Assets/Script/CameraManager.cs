using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;


public class CameraManager : MonoBehaviour {

    public Transform Player;
    public Vector3 lookOffset = new Vector3(0, 90f, -73f); 

    private void Awake() {

        //Player = GameManager.player.transform;

        /// Camera position reset
        transform.position = new Vector3(0, 0, 0) + lookOffset; 

        /// Camera position : Map
        transform.LookAt(GameObject.Find("Map").GetComponent<Transform>()); 
    }

    private void LateUpdate() {

        if(Camera.main.fieldOfView < 50) {

            /// Camera position : player
            transform.position = Player.position + lookOffset; 
            transform.LookAt(Player); 
        }
    }

    /// Camera position : GAME START, SEEK
    public void Camera_Seek() { 
        DOTween.To(() => Camera.main.fieldOfView, x=> Camera.main.fieldOfView = x, 7.5f, 1);        
        StartCoroutine(CameraStart()); 
    }
    /// After GameManager.Timer()
    IEnumerator CameraStart() {
        yield return new WaitForSeconds(3);
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
    }

    /// Camera position : GAME START, SEEK
    public void Camera_Hide() { 
        transform.position = Player.position + lookOffset;
        DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, 20, 2);
    }
}
