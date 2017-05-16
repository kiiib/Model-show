using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Hit;
using DG.Tweening;
using UnityEngine.UI;
public class PlaneGestureManager : MonoBehaviour {

    public TapGesture singleTap;
    public TapGesture doubleTap;
    public Animator modelAnimator;
    public GameObject switcher;
    public Text Log;

    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float shakeDetectionThreshold = 3.0f;
    private float unShakeDetectionThreshold = 1.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;

    public float rotateSpeed = 10.0f;

    private Vector2 touchStartPos;
    private TouchPhase preAction;
    private float swipeSpeedThreshold = 0.01f;
    private float touchTimer = 0;

    //private bool DoAnimation = false;
	// Use this for initialization
	void Start () {

        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;

        singleTap.Tapped += (object sender, System.EventArgs e) => {
            Debug.Log("single touch");
            modelAnimator.SetTrigger("WIN");
        };

        doubleTap.Tapped += (object sender, System.EventArgs e) => {
            Debug.Log("double touch");
            modelAnimator.SetTrigger("LOSE");
        };
    }

    // Update is called once per frame
    void Update() {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;
        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold) {
            //Shake
            modelAnimator.SetTrigger("JUMP");
        }

        if (Input.touchCount == 1) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                
                touchStartPos = Input.GetTouch(0).position;
                Debug.Log("start " + touchStartPos);
                Log.text = "start " + touchStartPos;
                preAction = TouchPhase.Began;
            }


            if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                touchTimer += Time.deltaTime;
                Vector2 speed = Input.GetTouch(0).deltaPosition * Time.deltaTime;
                if (speed.sqrMagnitude < swipeSpeedThreshold) {
                    if (Input.GetTouch(0).deltaPosition.x > 0)
                        this.transform.Rotate(Vector3.up * rotateSpeed * Input.GetTouch(0).deltaPosition.magnitude, Space.Self);
                    else
                        this.transform.Rotate((-1) * Vector3.up * rotateSpeed * Input.GetTouch(0).deltaPosition.magnitude, Space.Self);

                }
                preAction = TouchPhase.Moved;
            }
           
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                Debug.Log ("end");
                Log.text = "end";
                Vector2 speed = Input.GetTouch(0).deltaPosition * Time.deltaTime;
                if (preAction == TouchPhase.Moved && touchTimer < 0.4f) {

                    Vector2 currPos = Input.GetTouch(0).position;
                    Vector2 direction = currPos - touchStartPos;
                    Debug.Log("currPos " + currPos);
                    Log.text = "currPos " + currPos;

                    if (direction.x > 0) {
                        Debug.Log ("move right");
                        Log.text = "move right";
                        switcher.SendMessage("swipeRight");

                    } else {
                        Debug.Log ("move left");
                        Log.text = "move left";
                        switcher.SendMessage("swipeLeft");

                    }
                }

                touchTimer = 0;
                preAction = TouchPhase.Ended;
            }
        }
    }
}
