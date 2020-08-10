using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAutoDown : MonoBehaviour {
    private ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start () {
        scrollRect = this.gameObject.GetComponent<ScrollRect> ();
    }

    // Update is called once per frame
    void Update () {

    }

    // スクロールビューを一番下にする
    public IEnumerator AutoSetScroll () {
        if (scrollRect.verticalNormalizedPosition <= 0.08f) {
            yield return new WaitForSeconds (0.5f);
            Debug.Log ("すくろーる");
            scrollRect.verticalNormalizedPosition = 0f;
            scrollRect.SetLayoutVertical ();
        }
    }
}