using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public GameObject myMessage;
    public GameObject othersMessage;

    public GameObject messages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        var message = new Dictionary<string, string>(){
            {"content", "Hello !"},
            {"name", "Aさん"}
        };
        this.appendMessage(message);
    }

    void appendMessage(Dictionary<string, string> message){	
        GameObject mes = Instantiate(myMessage);
        mes.transform.parent = messages.transform;
    }
}
