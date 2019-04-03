using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class SetwwwEditModel : MonoBehaviour {

    static public List<SetwwwEdit> DesirializeFromJson(string sStrJson)
    {
        List<SetwwwEdit> ret = new List<SetwwwEdit>();
        SetwwwEdit tmp = null;

        // JSONデータは最初は配列から始まるので、Deserialize（デコード）した直後にリストへキャスト      
        IList jsonList = (IList)Json.Deserialize(sStrJson);

        // リストの内容はオブジェクトなので、辞書型の変数に一つ一つ代入しながら、処理
        foreach (IDictionary jsonOne in jsonList)
        {

            //新レコード解析開始
            tmp = new SetwwwEdit();

            if (jsonOne.Contains("UserName"))
            {
                tmp.UserName = (string)jsonOne["UserName"];
            }
            if (jsonOne.Contains("Createdatetime"))
            {
                tmp.Createdatetime = (string)jsonOne["Createdatetime"];
            }
            if (jsonOne.Contains("PassWord"))
            {
                tmp.PassWord = (string)jsonOne["PassWord"];
            }
            if (jsonOne.Contains("MailAddress"))
            {
                tmp.MailAddress = (string)jsonOne["MailAddress"];
            }
            //現レコード解析終了
            ret.Add(tmp);
            tmp = null;
        }
        return ret;
    }
}
