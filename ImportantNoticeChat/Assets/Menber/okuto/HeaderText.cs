using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeaderText : MonoBehaviour
{
    public Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        this.textObject.text = Env.from + "さんとのチャット";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
