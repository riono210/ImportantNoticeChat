# ImportantNoticeChat
本アプリは，重要なメッセージを絶対に見逃さないことを目的としたチャットアプリです．

## フレームワーク
- Unity(C#)， バージョン:2019.4.5f1

## デプロイ方法
Unity画面上部メニューにある，File→Build Settings→iosを選択して，画面右下にあるSwich platformボタンを押す．  
そこで，Unity Editorまたは実機ビルド(ios)の場合で，以下の2つの操作に分かれる．
- Unity Editor上で動かす場合: 画面中央上部の実行ボタンを押す
- 実機ビルドの場合: PCとios端末を接続して、Build And Runボタンを押す→Xcodeプロジェクトに変換されるのでXcode側でBuildおよびRunされる


## 実行方法
本アプリは，エンドポイントのURLやシークレットキー，送信元，送信先をEnvファイルで管理した．  
Envファイルは，GitHub上にPushしていないため，以下に記載事項を示す．  
なお，ファイル名はEnv.csとして保存した．

```C#
public class Env{
    public static string base_url = “<エンドポイントのURL>”;
    public static string sec_key = “<シークレットキー>”;
    
    public static string GetBaseUrl(){
        return base_url;
    }
    public static string GetSecKey(){
        return sec_key;
    }

    public static string to = “<送信先>”;
    public static string from = “<送信元>”;
}
```
