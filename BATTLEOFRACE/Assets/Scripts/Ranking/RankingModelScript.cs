using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class RankingModelScript : MonoBehaviour {

    static public List<RankingScript> DesirializeFromJson(string sStrJson)
    {
        List<RankingScript> ret = new List<RankingScript>();
        RankingScript tmp = null;

        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト      
        IList jsonList = (IList)Json.Deserialize(sStrJson);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        foreach (IDictionary jsonOne in jsonList)
        {

            //新レコード解析開始
            tmp = new RankingScript();

            if (jsonOne.Contains("RaceNO"))
            {
                tmp.RaceNO = (int)(long)jsonOne["RaceNO"];
            }
            if (jsonOne.Contains("NicName"))
            {
                tmp.NicName = (string)jsonOne["NicName"];
            }
            if (jsonOne.Contains("Racetime"))
            {
                tmp.Racetime = (string)jsonOne["Racetime"];
            }
            
            if (jsonOne.Contains("Date"))
            {
                tmp.Date = (string)jsonOne["Date"];
            }
            
            if (jsonOne.Contains("ReceTimemm"))
            {
                tmp.ReceTimemm = (int)(long)jsonOne["ReceTimemm"];
            }
            if (jsonOne.Contains("RaceTimess"))
            {
                tmp.RaceTimess = (int)(long)jsonOne["RaceTimess"];
            }
            if (jsonOne.Contains("RacetTimeMillisecond"))
            {
                tmp.RacetTimeMillisecond = (int)(long)jsonOne["RacetTimeMillisecond"];
            }

            //現レコード解析終了
            ret.Add(tmp);
            tmp = null;
        }
        return ret;
    }
}
