using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class EditManagerScript : MonoBehaviour {

    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject editCanvas;

    [SerializeField] private InputField nicNameInputField;
    [SerializeField] private InputField passWordInputField;
    [SerializeField] private InputField mailAddressInputField;
    Text nicNameText;
    Text passWordText;
    Text mailAddressText;

    public static string playerName;

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void OnClickEdit()
    {
        playerName = nicNameInputField.text;
        SetJsonFromWww();
        loadMain();
    }

    public static string getName()
    {
        return playerName;
    }

    public void OnClickReturn()
    {
        titleCanvas.SetActive(true);
        editCanvas.SetActive(false);        
    }

    private void loadMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void SetJsonFromWww()
    {
        // APIが設置してあるURLパス
        string sTgtURL = "http://localhost/rankingsystem/accountedit/setaccount";
        string UserName = nicNameInputField.text;
        string PassWord = passWordInputField.text;
        string MailAddress = mailAddressInputField.text;

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(SetMessage(sTgtURL, UserName, PassWord, MailAddress, CallbackApiSuccess, CallbackWwwFailed));
    }
    private IEnumerator SetMessage(string urlTarget, string UserName, string PassWord, string MailAddress, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("UserName", UserName);
        form.AddField("PassWord", PassWord);
        form.AddField("MailAddress", MailAddress);

        // WWWを利用してリクエストを送る
        WWW www = new WWW(urlTarget, form);
        // WWWレスポンス待ち
        yield return StartCoroutine(ResponceCheckForTimeOutWWW(www, 5.0f));
        if (www.error != null)
        {
            //レスポンスエラーの場合
            Debug.LogError(www.error);
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (www.isDone)
        {
            // リクエスト成功の場合
            Debug.Log(string.Format("Success:{0}", www.text));
            if (null != cbkSuccess)
            {
                cbkSuccess(www.text);
            }
        }
    }
    private void CallbackApiSuccess(string response)
    {
        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
        Debug.Log(response);
    }
    private void CallbackWwwFailed()
    {
        // jsonデータ取得に失敗した
        Debug.Log("Www Failed");
    }
    private IEnumerator ResponceCheckForTimeOutWWW(WWW www, float timeout)
    {
        float requestTime = Time.time;
        while (!www.isDone)
        {
            if (Time.time - requestTime < timeout)
            {
                yield return null;
            }
            else
            {
                Debug.LogWarning("TimeOut"); //タイムアウト
                break;
            }
        }
        yield return null;
    }
}
