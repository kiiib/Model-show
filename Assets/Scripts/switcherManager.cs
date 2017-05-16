using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switcherManager : MonoBehaviour {

    public GameObject model1;
    public GameObject model2;

    public void swipeRight() {
        model1.SetActive(false);
        model2.SetActive(true);
    }

    public void swipeLeft() {
        model1.SetActive(true);
        model2.SetActive(false);

    }
}
