using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class PostTest : MonoBehaviour {
    string url = "http://db.denchu.cloud:5111/uiuxchat3287bivsgfbivf/test2/messages";
    string key = "";

    private void PostMessage () {
        // 投げるjsonデータ
        string postData = "{\"to\":\"someone\",\"content\":\"aaaaaaaaaaaallllllililili\"}";
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

    }

}