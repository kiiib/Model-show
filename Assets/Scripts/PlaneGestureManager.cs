using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;
using DG.Tweening;

public class PlaneGestureManager : MonoBehaviour {

    public TapGesture singleTap;
    public TapGesture doubleTap;

    public Rigidbody Box;

	// Use this for initialization
	void Start () {
        singleTap.Tapped += (object sender, System.EventArgs e) => {
            Debug.Log("single touch");
            TouchHit hit;
            singleTap.GetTargetHitResult(out hit);

            Vector3 targetPoint = hit.Point;
            targetPoint.y = Box.transform.position.y;

            Box.transform.DOMove(targetPoint, 0.5f);
        };

        doubleTap.Tapped += (object sender, System.EventArgs e) => {
            Debug.Log("double touch");
            TouchHit hit;
            doubleTap.GetTargetHitResult(out hit);

            Vector3 targetPoint = hit.Point;

            Box.velocity = new Vector3(0, 9.9f * 1f / 2.0f, 0);
            Box.transform.DOLocalMoveX(targetPoint.x, 1f);
            Box.transform.DOLocalMoveY(targetPoint.z, 1f);

        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
