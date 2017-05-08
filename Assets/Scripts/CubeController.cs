using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

    public Camera mainCamera;

	// Use this for initialization
	void Start () {
        //開啟陀螺儀
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Input.touches.Length; i++) {
            Debug.Log("Point " + Input.touches[i].fingerId + ":" + Input.touches[i].position);

            Vector3 screenPos = Input.touches[i].position;

            //距離Camera多遠
            screenPos.z = this.transform.position.z - mainCamera.transform.position.z;

            Vector3 TargetPos = mainCamera.ScreenToWorldPoint(screenPos);
            this.transform.position = TargetPos;
        }

        this.transform.rotation = ConverRotation(Input.gyro.attitude);

    }

    private Quaternion ConverRotation(Quaternion q) {
        // Quaternion.Euler(90, 0, 0) 先沿著X軸旋轉90度
        // -q.z, -q.w 把原本的旋轉鏡像
        return Quaternion.Euler(90, 0, 0) * new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
