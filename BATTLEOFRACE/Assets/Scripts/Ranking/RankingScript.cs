using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingScript : MonoBehaviour {

    public int RaceNO;
    public string NicName;
    public string Racetime;
    public string Date;
    public int ReceTimemm;
    public int RaceTimess;
    public int RacetTimeMillisecond;


    public RankingScript()
    {
        RaceNO = 0;
        NicName = "";
        Racetime = "";
        Date = "";
        ReceTimemm = 0;
        RaceTimess = 0;
        RacetTimeMillisecond = 0;
    }
}
