using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldManager : MonoBehaviour {

    private InputField inputField;
    private RectTransform footerPanelRect; // footerのrecttransfrom
    [SerializeField] private RectTransform bodyPanelRect; // bodyのrect インスペクタで指定
    private bool isOnceInput = true; // 入力時のfooter・bodyの位置移動フラグ

    private Vector3 defaultFooterPos; // footerの初期位置
    private Vector3 defalutBodyPos; // bodyの初期位置

    string inputText; // 入力されたテキスト
    int priority; // メッセージの重要度
    bool isCancel;

    private MassageClass massage; // メッセージの格納クラス
    [SerializeField] private GameObject toriggerParent; // priority設定の親objを取得

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
#if UNITY_IOS && !UNITY_EDITOR_OSX
        StartInputText ();

        if (inputField.touchScreenKeyboard == null && !isOnceInput) { // キーボードが消えたとき
            ResetKeybord ();
        }
#endif
    }

    // フィールドの初期化
    private void InitInputField () {
        inputField.text = "";
        inputText = "";
        ResetKeybord ();
        priority = 1;
        massage = new MassageClass ();
        toriggerParent.SetActive (false); // priorityのトグルを消す
    }

    // キーボードによって上にずれたUIの位置を戻す
    public void ResetKeybord () {
        isOnceInput = true;
        footerPanelRect.localPosition = defaultFooterPos;
        bodyPanelRect.localPosition = defalutBodyPos;
        isCancel = false;
    }

    // 入力完了
    public void FinishEditText () {
        Debug.Log ("りざると:" + inputText);
#if UNITY_EDITOR_OSX
        // エディタでは入力キャンセルは処理しない
        InitInputField ();
        //ResetKeybord ();

#elif UNITY_IOS && !UNITY_EDITOR_OSX
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done) {
            // 入力完了時
            // 送信のための準備

            // フィールドの初期化
            InitInputField ();
            Debug.Log ("Done");
        } else if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled ||
            inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus) {
            // 入力キャンセル時もしくは、
            inputField.text = inputText;
            // キーボードだけ戻す
            ResetKeybord ();
            Debug.Log ("Canseled");
        }
#endif
    }

    // 入力開始時
    private void StartInputText () {
        if (inputField.isFocused && isOnceInput) {
            isOnceInput = false;
            footerPanelRect.localPosition += new Vector3 (0, 940f, 0);
            bodyPanelRect.localPosition += new Vector3 (0, 940f, 0);
            toriggerParent.SetActive (true); // priorityのトグルを出す
        }
    }

    // テキストの内容が変更された時に呼ばれる
    public void ChangeTextField () {
        Debug.Log ("呼び出しフィールド: " + inputField.text);
        if (inputField.text == "") { // 空白は無視
            return;
        }

#if UNITY_EDITOR_OSX
        inputText = inputField.text;

#elif UNITY_IOS && !UNITY_EDITOR_OSX
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled) {
            isCancel = true;
            // Debug.Log ("フィールド: " + inputField.text + " テキスト: " + inputText);
            // if (inputField.text != "") {
            //     inputText = inputField.text;
            //     Debug.Log ("cancel inputText: " + inputText);
            // }
        } else if (inputField.isFocused && !isCancel) { // 他のところをタップした時
            inputText = inputField.text;
            Debug.Log ("isFocuse inputText: " + inputText);
        }
#endif
    }

    // 送信ボタン関数
    public void SendFunction () {
        if (inputText != "") { // 空白は除外
            Debug.Log ("送信ボタン");
            massage.from = "user_name";
            massage.to = "someone";
            massage.content = inputText;

            // 最後に初期化
            InitInputField ();
        }
    }

    // プライオリティーの変更
    public void SetPriority (int number) {
        if (priority != number) {
            priority = number;
            Debug.Log ("重要度:" + priority);
        }
    }
}