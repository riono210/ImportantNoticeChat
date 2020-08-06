using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParesTest : MonoBehaviour {

    public ApiCall call;
    private bool once = true;

    // Start is called before the first frame updat
    void Start () {
        StartCoroutine (call.GetText ());
    }

    void Update () {
        if (once) {
            if (call.postresult != "") {
                once = false;

                JsonParser.ConvertFromJson (call.postresult);
            }
        }
    }

}