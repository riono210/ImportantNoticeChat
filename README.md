# ImportantNoticeChat
本アプリは，重要なメッセージを絶対に見逃さないことを目的とした，チャットアプリです．

## フレームワーク
- Unity(C#)

## デプロイ
- ios

## 実行方法
本アプリは，エンドポイントのURLやシークレットキー，送信元，送信先の変数をEnvファイルに管理した．  
Envファイルは，GitHub上に載せていないため，以下に記載事項を示す．  
なお，ファイル名は，Env.csとして保存した．

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
