using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiCall : MonoBehaviour
{
    string url = "http://db.denchu.cloud:5111/uiuxchat3287bivsgfbivf/test2/messages";
    // アクセスキーは削除すること
    string key = "";
    string message = "(・∀・)ｲｲ!!";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(url);
        Debug.Log(key);
        Debug.Log("message = " + message);
        StartCoroutine(GetText());
        // StartCoroutine(PostText());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator GetText() {
        var request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.isHttpError) {
            // レスポンスコードを見て処理
            Debug.Log($"[Error]Response Code : {request.responseCode}");
        }
        else if (request.isNetworkError) {
            // エラーメッセージを見て処理
            Debug.Log($"[Error]Message : {request.error}");
        }
        else{
            // 成功したときの処理
            Debug.Log($"[Success]");
            Debug.Log(request.downloadHandler.text);

        }
    }

    /*
    IEnumerator PostText() {
        WWWForm data = new WWWForm();
        data.AddField("to", "someone");
        data.AddField("content", "hello");

        // 参考になりそう: https://qiita.com/mosin_nozomi/items/1be9098b659be151b48a
        var request = UnityWebRequest.Post(url, data);
        request.SetRequestHeader("key", key);
        yield return request.SendWebRequest();
        
        if(request.isHttpError) {
            // レスポンスコードを見て処理
            Debug.Log($"[Error]Response Code : {request.responseCode}");
        }
        else if (request.isNetworkError) {
            // エラーメッセージを見て処理
            Debug.Log($"[Error]Message : {request.error}");
        }
        else{
            // 成功したときの処理
            Debug.Log($"[Success]");
            Debug.Log(request.downloadHandler.text);

        }
    }
    */
}
