using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MassageClass {
    public string from = "riono";
    public string to = "okuto";
    public string content;
    //public string timestamp;
    public int priority;
    //public int parent;

    // コンストラクタ
    public MassageClass (string from, string to, string content, int priority) {
        this.from = from;
        this.to = to;
        this.content = content;
        this.priority = priority;
    }

    // 何もしないやつ
    public MassageClass () {

    }
}