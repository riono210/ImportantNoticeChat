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
    [SerializeField] private GameObject toggleParent; // priority設定の親objを取得
    private Toggle[] toggles; // 各トグルの配列
    private Image inputFieldBackgroundImg;

    // Start is called before the first frame update
    void Start () {
        inputField = this.transform.GetChild (0).GetComponent<InputField> ();
        footerPanelRect = this.transform.parent.GetComponent<RectTransform> ();
        defaultFooterPos = footerPanelRect.localPosition;
        if (bodyPanelRect != null) {
            defalutBodyPos = bodyPanelRect.localPosition;
        } else {
            Debug.LogError ("Bodyがアタッチされていません");
            return;
        }

        if (toggleParent == null) {
            Debug.LogError ("TogglePrantアタッチされていません");
            return;
        }
        // トグルの取得
        toggles = new Toggle[3];
        for (int i = 0; i < toggleParent.transform.childCount; i++) {
            toggles[i] = toggleParent.transform.GetChild (i).GetComponent<Toggle> ();
        }

        inputFieldBackgroundImg = this.GetComponent<Image> ();
        InitInputField ();
    }

    void Update () {
#if UNITY_IOS && !UNITY_EDITOR_OSX
        StartInputText ();

        // if (inputField.touchScreenKeyboard == null && !isOnceInput) { // キーボードが消えたとき
        //     ResetKeybord ();
        // }
#endif
    }

    // フィールドの初期化
    private void InitInputField () {
        inputField.text = "";
        inputText = "";
        ResetKeybord ();
        priority = 1;
        massage = new MassageClass ();
        toggleParent.SetActive (false); // priorityのトグルを消す
        inputFieldBackgroundImg.color = ToRGB (0x4FE722);
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
        } else {
            Debug.Log ("else cansel");
            TapObjectCheck ();
        }
#endif
    }

    // 入力開始時
    private void StartInputText () {
        if (inputField.isFocused && isOnceInput) {
            isOnceInput = false;
            footerPanelRect.localPosition += new Vector3 (0, 940f, 0);
            bodyPanelRect.localPosition += new Vector3 (0, 940f, 0);
            toggleParent.SetActive (true); // priorityのトグルを出す
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

    public void TapObjectCheck () {
        Vector3 tapPos = Input.GetTouch (0).position;
        var raycastResult = new List<RaycastResult> ();

        PointerEventData pointer = new PointerEventData (EventSystem.current);
        pointer.position = tapPos;
        EventSystem.current.RaycastAll (pointer, raycastResult);

        foreach (var value in raycastResult) {
            Debug.Log ("raycast名前: " + value.gameObject.name);
            if (value.gameObject.name == "SendButton") { // 送信ボタンを押した場合
                SendFunction ();
                InitInputField ();
                return;
            } else if (value.gameObject.name == "CheckmarkLow") {
                SetPriority (1);
                toggles[0].isOn = true;
                ResetKeybord ();
                return;
            } else if (value.gameObject.name == "CheckmarkMid") {
                SetPriority (10);
                toggles[1].isOn = true;
                ResetKeybord ();
                return;
            } else if (value.gameObject.name == "CheckmarkHi") {
                SetPriority (100);
                toggles[3].isOn = true;
                ResetKeybord ();
                return;
            } else {
                ResetKeybord ();
            }
        }

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
            switch (number) {
                case 100:
                    inputFieldBackgroundImg.color = ToRGB (0xF12A2A);
                    break;
                case 10:
                    inputFieldBackgroundImg.color = ToRGB (0xF1F312);
                    break;

                case 1:
                default:
                    inputFieldBackgroundImg.color = ToRGB (0x4FE722);
                    break;
            }
            Debug.Log ("重要度:" + priority);
        }
    }

    public static Color ToRGB (uint val) {
        var inv = 1f / 255f;
        var c = Color.black;
        c.r = inv * ((val >> 16) & 0xFF);
        c.g = inv * ((val >> 8) & 0xFF);
        c.b = inv * (val & 0xFF);
        c.a = 1f;
        return c;
    }
}