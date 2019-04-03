using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.UI;

public class RankingManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject rankingPrefabContents;
    [SerializeField] private Transform contentsTransform;
    [SerializeField] private Text playerResultText;
    [SerializeField] private GameObject failedDataText;


    // Use this for initialization
    void Start()
    {
        Stopwatch setTime = GameManagerScript.getTime();

        GetJsonFromWww();

        playerResultText.text = "あなたのＴＩＭＥは" +
            string.Format("{0:00}:{1:00}.{2:#.##}", setTime.Elapsed.Minutes, setTime.Elapsed.Seconds, setTime.Elapsed.Milliseconds)
            + "です。";

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetJsonFromWww()
    {

        // APIが設置してあるURLパス
        string sTgtURL = "http://localhost/rankingsystem/raceranking/getRankings";

        // Wwwを利用して json データ取得をリクエストする
        StartCoroutine(GetMessages(sTgtURL, CallbackWwwSuccess, CallbackWwwFailed));
 
    }

    private IEnumerator GetMessages(string sTgtURL, Action<string> cbkSuccess = null, Action cbkFailed = null)
    {

        // WWWを利用してリクエストを送る
        WWW www = new WWW(sTgtURL);

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
        else
        if (www.isDone)
        {
            // リクエスト成功の場合
            if (null != cbkSuccess)
            {
                cbkSuccess(www.text);
            }
        }
    }


    private void CallbackWwwSuccess(string response)
    {

        // json データ取得が成功したのでデシリアライズして整形し画面に表示する
        List<RankingScript> messageList = RankingModelScript.DesirializeFromJson(response);

        string sStrOutput = "";
        foreach (RankingScript ranking in messageList)
        {
 //         sStrOutput += string.Format("RaceNO:{0}\n", ranking.RaceNO);
            sStrOutput += string.Format("NicName:{0}\n", ranking.NicName);
            sStrOutput += string.Format("Racetime:{0}\n", ranking.Racetime);
            //sStrOutput += string.Format("Date:{0}\n", ranking.Date);
            //sStrOutput += string.Format("ReceTimemm:{0}\n", ranking.ReceTimemm);
            //sStrOutput += string.Format("RaceTimess:{0}\n", ranking.RaceTimess);
            //sStrOutput += string.Format("RacetTimeMillisecond:{0}\n", ranking.RacetTimeMillisecond);


            CreateRankingContents(ranking);
        }
        //text.text = sStrOutput;
        
    }


    /// <summary>
    /// Callbacks the www failed.
    /// </summary>
    private void CallbackWwwFailed()
    {

        // jsonデータ取得に失敗した
        failedDataText.SetActive(true);
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

    private void CreateRankingContents(RankingScript ranking)
    {
        GameObject contents = Instantiate(rankingPrefabContents);
        contents.transform.SetParent(contentsTransform);


        contents.GetComponent<RankingContents>().nameText.text = ranking.NicName + "         " + ranking.Racetime;

        //int racetime = Convert.ToInt32(ranking.Racetime);
        //UnityEngine.Debug.Log("タイムは" + racetime);

        //contents.GetComponent<RankingContents>().timeText.text = string.Format("{0:00}:{1:00}.{2:#.##}", ranking.Racetime);
    }

    // タイトル画面へ
    public void OnClickToTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }



}
