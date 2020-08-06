using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

    private InputField inputField;
    private RectTransform footerPanelRect; // footerのrecttransfrom
    [SerializeField] private RectTransform bodyPanelRect; // bodyのrect インスペクタで指定
    private bool isOnceInput = true; // 入力時のfooter・bodyの位置移動フラグ

    private Vector3 defaultFooterPos; // footerの初期位置
    private Vector3 defalutBodyPos; // bodyの初期位置

    // Start is called before the first frame update
    void Start () {
        inputField = this.transform.GetChild (0).GetComponent<InputField> ();
        footerPanelRect = this.transform.parent.GetComponent<RectTransform> ();
        defaultFooterPos = footerPanelRect.localPosition;
        if (bodyPanelRect != null) {
            defalutBodyPos = bodyPanelRect.localPosition;
        } else {
            Debug.LogError ("Bodyがアタッチされていません");
        }

        InitInputField ();
    }

    void Update () {
        StartInputText ();
    }

    // フィールドの初期化
    private void InitInputField () {
        inputField.text = "";
        isOnceInput = true;
        footerPanelRect.localPosition = defaultFooterPos;
        bodyPanelRect.localPosition = defalutBodyPos;
        //inputField.ActivateInputField ();
    }

    // 入力完了
    public void FinishEditText () {
        // 一旦終了した後に再入力パターンがあるのでそれを対応する

        string inputText = inputField.text;
        Debug.Log ("inputtext:" + inputText);

        InitInputField ();
    }

    // 入力開始時
    private void StartInputText () {
        if (inputField.isFocused && isOnceInput) {
            isOnceInput = false;
            footerPanelRect.localPosition += new Vector3 (0, 940f, 0);
            bodyPanelRect.localPosition += new Vector3 (0, 940f, 0);
        }
    }
}