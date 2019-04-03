using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireShell : MonoBehaviour
{
    // 残り残段数
    [SerializeField] private int remainShells;
    [SerializeField] private Text remainShellsText;
    //　弾のプレハブ
    [SerializeField] private GameObject shell;
    //　レンズからのオフセット値
    [SerializeField] private float offset;

    //　弾を飛ばす間隔時間
    [SerializeField] private float waitTime = 0.1f;
    //　経過時間
    private float elapsedTime = 0f;

    private void Start()
    {
        remainShells = 100;
        shellsCount();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime < waitTime)
        {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            if (remainShells > 0)
            {
                fire();
                remainShells --;
                shellsCount();
            }
        }
    }
    void fire()
    {
        //　カメラのレンズの中心を求める
        var centerOfLens = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane + offset));
        //　中心から弾を飛ばす
        var bulletObj = Instantiate(shell, centerOfLens, transform.rotation) as GameObject;
        elapsedTime = 0f;
    }
    void shellsCount()
    {
        if (remainShells > 0)
        {
            remainShellsText.text = remainShells + "発";
        }
        else if(remainShells == 0)
        {
            remainShellsText.text = "無し";
        }
    }
}
