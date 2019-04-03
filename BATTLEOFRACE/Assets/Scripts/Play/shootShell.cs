using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class shootShell : MonoBehaviour {


    // 弾を飛ばす力
    [SerializeField] private float power = 1000f;
    // 弾が消滅する時間
    [SerializeField] private float deleteTime = 2f;

    private Rigidbody rigid;
    // Rayを使用
    private Ray ray;

    void Awake()
    {
        //　Rigidbodyを取得し速度を0に初期化
        rigid = GetComponent<Rigidbody>();
    }

    //　弾がアクティブになった時
    void OnEnable()
    {
        //　カメラからクリックした位置にレイを飛ばす
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        rigid.AddForce(ray.direction * power, ForceMode.VelocityChange); 

        //　弾を発射してから指定した時間が経過したら自動で削除
        Destroy(this.gameObject, deleteTime);
    }

    //　弾が存在していればレイの方向に力を加える
    void FixedUpdate()
    {
        //rigid.AddForce(ray.direction * power, ForceMode.Force);
        rigid.AddForce(0f, 15f, 0f,ForceMode.Force);
    }

    void OnCollisionEnter(Collision col)
    {   //障害物タグがついたものを破壊
        if (col.gameObject.CompareTag("Obj"))
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}

