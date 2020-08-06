using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

[System.Serializable]
public class JsonParser {

    public string result;

    public string from;
    public string to;
    public string content;
    public string timestamp;
    public int priority;
    public int parent;

    public string name;
    public string status;
    public string tickets;

    public static void ConvertFromJson (string result) {
        Dictionary<string, object> dic1 = Json.Deserialize (result) as Dictionary<string, object>;

        // foreach (var value in dic1) {
        //     Debug.Log ("key: " + value.Key + "value: " + value.Value);
        // }

        foreach (var value in (List<object>) dic1["result"]) {
            Debug.Log (value);
            foreach (var diction in (Dictionary<string, object>) value) {
                    Debug.Log ("key: " + diction.Key + " value: " + diction.Value);
                }
            }

            //string resultText = dic1["result"];

            //Dictionary<string, object> dic2 = Json.Deserialize (resultText) as Dictionary<string, object>;

            // foreach (var value in dic2) {
            //     Debug.Log ("key: " + value.Key + "value: " + value.Value);
            // }
        }

    }