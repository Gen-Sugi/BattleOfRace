using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ストップウォッチ用
using System.Diagnostics;
using System;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    //          時計関連
    public static Stopwatch st;
    //1時間＝3600000f
    float limitTime = 3600000f;
    TimeSpan nowTime;
    [SerializeField] private Text timeText;

    //          ゲーム進行関連
    // [SerializeField] private GameObject checkPoint;
    //[SerializeField] private GameObject goal;
    //public int checkPointNumber = 0;
    PlayerController p_script;

    //  音関連
    [SerializeField] private AudioClip raceStart_do;
    [SerializeField] private AudioClip raceStart_highdo;
    private AudioSource audioSource;

    //プレイヤーの


    // Use this for initialization
    void Start () {
        // ストップウォッチ作成
        st = new Stopwatch();
        
        timeText.GetComponent<Text>();
        
        timeText.text = st.Elapsed.ToString();

        StartTimer();

        //レース開始音
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine("CountDown");

        p_script = new PlayerController();
       
    }

    // Update is called once per frame
    void Update ()
    {
        if (st.ElapsedMilliseconds < limitTime)
        {
            nowTime = st.Elapsed;
            timeText.text = string.Format("{0:00}:{1:00}.{2:#.##}",
                nowTime.Minutes, nowTime.Seconds, nowTime.Milliseconds);
        }
        else
        {
            StopTimer();
        }

        //ゴール処理
        if (p_script.currentCheckNumber == 5)
        {
            StopTimer();

            SetJsonFromWww();

            SceneManager.LoadScene("RankingScene");
        }
    }

/*    private IEnumerator Goal()
    {

    }
*/

    void SceneToRanking()
    {
        SceneManager.LoadScene("RankingScene");
    }

    void StartTimer()
    { 
        st.Start();
    }
    
    void StopTimer()
    {
        st.Stop();
    }
    // ランキング画面に時間の受け渡し
    public static Stopwatch getTime()
    {
        return st;
    }
    //  スタートのカウントダウン
    private IEnumerator CountDown()
    {
        UnityEngine.Debug.Log("カウントダウン開始");

        audioSource.clip = raceStart_do;
        audioSource.Play();
        yield return new WaitForSeconds(1f);

        audioSource.clip = raceStart_do;
        audioSource.Play();
        yield return new WaitForSeconds(1f);

        audioSource.clip = raceStart_do;
        audioSource.Play();
        yield return new WaitForSeconds(1f);

        audioSource.clip = raceStart_highdo;
        audioSource.Play();
        yield return new WaitForSeconds(1f);
       
        yield break;
    }
    // ーーーーーーーーーデータを送信するーーーーーーーーーーーーー

    public void OnCLickRegister()
    {
        SetJsonFromWww();
    }
    private void SetJsonFromWww()
    {
        string pName = EditManagerScript.getName();
        // APIが設置してあるURLパス
        string sTgtURL = "http://localhost/raceranking2/Raceranking/setRankings";
        string NicName = pName;
        //string Racetime = st.Elapsed.ToString();
        string Racetime = string.Format("{0:00}:{1:00}.{2:#.##}",
                nowTime.Minutes, nowTime.Seconds, nowTime.Milliseconds);
        int ReceTimemm = st.Elapsed.Minutes;
        int RaceTimess = st.Elapsed.Seconds;
        //floatをＩＮＴに変換
        int stMilliseconds = (int)st.ElapsedMilliseconds;
        int RacetTimeMillisecond = stMilliseconds;
        //// Wwwを利用して json データ取得をリクエストする
        StartCoroutine(SetMessage(sTgtURL, NicName, Racetime, ReceTimemm, RaceTimess, RacetTimeMillisecond,
            CallbackApiSuccess, CallbackWwwFailed));

    }
    private IEnumerator SetMessage(string urlTarget, 
        string NicName, string Racetime, int ReceTimemm, int RaceTimess, int RacetTimeMillisecond,
        Action<string> cbkSuccess = null, Action cbkFailed = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("NicName", NicName);
        form.AddField("Racetime", Racetime);
        form.AddField("ReceTimemm", ReceTimemm);
        form.AddField("RaceTimess", RaceTimess);
        form.AddField("RacetTimeMillisecond", RacetTimeMillisecond);

        // WWWを利用してリクエストを送る
        WWW www = new WWW(urlTarget, form);
        // WWWレスポンス待ち
        yield return StartCoroutine(ResponceCheckForTimeOutWWW(www, 5.0f));
        if (www.error != null)
        {
            //レスポンスエラーの場合
            if (null != cbkFailed)
            {
                cbkFailed();
            }
        }
        else if (www.isDone)
        {
            // リクエスト成功の場合
            UnityEngine.Debug.Log(string.Format("Success:{0}", www.text));
            if (null != cbkSuccess)
            {
                cbkSuccess(www.text);
            }
        }
    }
    private void CallbackApiSuccess(string response)
    {
        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
    }
    private void CallbackWwwFailed()
    {
        // jsonデータ取得に失敗した
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
                UnityEngine.Debug.LogWarning("TimeOut"); //タイムアウト
                break;
            }
        }
        yield return null;
    }
}
