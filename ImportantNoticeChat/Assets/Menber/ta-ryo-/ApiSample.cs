using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiSample : MonoBehaviour
{
    // Jsonデータにパースするための中身を解析するための地図的な
    [System.Serializable]
    public class InputFromJson 
    {
        public string[] result;
    }

    // [System.Serializable]
    // public class InputFromJson 
    // {
    //     public GetDataFromKey[] result;
    // }

    [System.Serializable]
    public class GetDataFromKey 
    {
        public string content;
    }

    public Env env;

    void Start()
    {
        StartCoroutine(GetMessage("messages"));
    }

    IEnumerator GetMessage(string endpoint)
    {
        string base_url = env.GetBaseUrl();
        UnityWebRequest getRequest = UnityWebRequest.Get(base_url+endpoint);
        yield return getRequest.SendWebRequest();
        Debug.Log($"StatusCode: {getRequest.responseCode}");
        var get_text = getRequest.downloadHandler.text;
        Debug.Log(get_text);


        InputFromJson json_data = JsonUtility.FromJson<InputFromJson>(get_text);
        
        // classの箱を用意しての方法
        // 叩いたAPIのデータが文字列型なのでInputFromJsonで宣言した型でjsonとしてパースしてる
        // InputFromJson json_data = JsonUtility.FromJson<InputFromJson>(get_text);
        // Debug.Log(json_data.result[0].content);
        // Debug.Log(json_data.result[1].content);
        // Debug.Log(json_data.result[2].content);
    }
}
