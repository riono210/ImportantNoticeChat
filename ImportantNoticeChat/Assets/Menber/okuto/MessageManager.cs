using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class MessageManager : MonoBehaviour
{

    public GameObject messages;

    private ApiSample.InputFromJson inputFromJson;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        this.loadMessagesFromDatabase();
    }

    void loadMessagesFromDatabase(){
        Debug.Log("load messages");
    }

    void displayMessagesFromDatabase(){

    }

    void getImportantMessages(){

    }





    public void messagesLoaded(ApiSample.InputFromJson json_data ){
        Debug.Log("messages loaded");
        Debug.Log(json_data);
        inputFromJson = json_data;
    }
}

