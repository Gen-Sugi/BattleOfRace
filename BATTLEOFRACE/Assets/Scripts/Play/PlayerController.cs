using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

   //走行
    public float speed = 100f;          
    public float turnSpeed = 240f;

    private float movementInputValue;
    private float turnInputValue;

    //操作
    private string movementAxisName;          
    private string turnAxisName;
    
    //現在のチェックポイント
    public int currentCheckNumber;

    private Rigidbody rigidbody;             


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    
    private void OnEnable()
    {
        //　プレイヤーを止める
        rigidbody.isKinematic = false;
        //　移動の値の初期化
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    
    private void OnDisable()
    {
        //　プレイヤーが動けるようになる
        rigidbody.isKinematic = true;
    }


    private void Start()
    {
        //　縦の動きと横の動きを設定
        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";
        //　チェックナンバーの初期化
        currentCheckNumber = 0;
    }


    private void Update()
    {
        //　キー入力を設定
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

    }


    
    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    //　直進の設定
    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        movement += movement;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    //　回転の設定
    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }

    //　逆走防止処理
    private void OnTriggerEnter(Collider col)
    {   //チェックポイント１を通過した時
        if (col.gameObject.name == "CheckPoint1")
        {
            if (currentCheckNumber == 0)
            {
                currentCheckNumber = 1;
            }else if(currentCheckNumber == 2)
            {
                currentCheckNumber = 1;
            }
        }
        //チェックポイント２を通過した時
        if (col.gameObject.name == ("CheckPoint2"))
        {
            if (currentCheckNumber == 1)
            {
                currentCheckNumber = 2;
            }
            else if (currentCheckNumber == 3)
            {
                currentCheckNumber = 2;
            }
        }
        //チェックポイント３を通過した時
        if (col.gameObject.name == ("CheckPoint3"))
        {
            if (currentCheckNumber == 2)
            {
                currentCheckNumber = 3;
            }
            else if (currentCheckNumber == 4)
            {
                currentCheckNumber = 3;
            }
        }
        //チェックポイント４を通過した時
        if (col.gameObject.name == ("CheckPoint4"))
        {
            if (currentCheckNumber == 3)
            {
                currentCheckNumber = 4;
            }
        }
        // ゴールしたとき
        if(col.gameObject.name == ("Goal"))
        {
            if (currentCheckNumber == 4)
                {
                    currentCheckNumber = 5;
                }
        }
    }
}

