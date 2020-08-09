using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiCall : MonoBehaviour {
    string url;
    string key;
    string message = "(・∀・)ｲｲ!!";

    // Start is called before the first frame update
    void Start()
    {
        url = Env.GetBaseUrl();
        key = Env.GetSecKey();

        Debug.Log(url);
        Debug.Log(key);
        Debug.Log("message = " + message);
        StartCoroutine(GetText());
        //StartCoroutine(PostText());
        //PostText();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator GetText() {
        var request = UnityWebRequest.Get(url+"messages");
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

    
    IEnumerator PostText() {
        // 投げるjsonデータ
        string postData = "{\"to\":\"someone\",\"content\":\"iiiiiii\"}";
        byte[] byteArray = Encoding.UTF8.GetBytes (postData);

        // cookie書き込み
        CookieContainer cc = new CookieContainer ();
        cc.Add (new Uri (url), new Cookie ("key", key));

        // リクエストの作成
        HttpWebRequest req = (HttpWebRequest) WebRequest.Create (url);
        req.Method = "POST";
        req.ContentType = "application/json";
        req.ContentLength = byteArray.Length;
        req.CookieContainer = cc;

        // ストリームに送信するデータを書き込む
        Stream dataStream = req.GetRequestStream ();
        dataStream.Write (byteArray, 0, byteArray.Length);
        dataStream.Close ();

        // 送信
        HttpWebResponse res = (HttpWebResponse) req.GetResponse ();

        for (int i = 0; i < res.Headers.Count; i++) {
            Debug.Log (res.Headers[i]);
        }

        yield return res;

    }
    
}