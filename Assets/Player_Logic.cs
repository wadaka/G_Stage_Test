using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Logic : MonoBehaviour
{
    //プレイヤーの移動する速さ
    public float move_speed = 15;

    //プレイヤーの回転する速さ
    public float rotate_speed = 5;

    //プレイヤーの回転する向き
    //1 -> （プレイヤーから見て）時計回り
    //-1 -> （プレイヤーから見て）反時計回り
    private int rotate_direction = 0;

    //プレイヤーのRigidbody
    private Rigidbody Rig = null;

    //地面に着地しているか判定する変数
    public bool Grounded;

    //ジャンプ力
    public float Jumppower;


    void Start()
    {
        Rig = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Horizontal_Rotate();

        Vector3 move_direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
/*
        Rig.MovePosition(Rig.position + transform.TransformDirection(move_direction) * move_speed * Time.deltaTime);
        */
        if(Rig.velocity.magnitude<5.5){
        Rig.AddForce(transform.TransformDirection(move_direction)*50);
        }
    }

    void Jump()
    {
        if (Grounded == true)//  もし、Groundedがtrueなら、
        {
            if (Input.GetKeyDown(KeyCode.Space))//  もし、スペースキーがおされたなら、  
            {
                //Grounded = false;//  Groundedをfalseにする
                Rig.AddForce(transform.up * Jumppower * 120);//  上にJumpPower分力をかける
            }
        }
    }

    void OnCollisionEnter(Collision other)//  他オブジェクトに触れた時の処理
    {
        if (other.gameObject.tag == "Planet")//  もしPlanetというタグがついたオブジェクトに触れたら、
        {
            Grounded = true;//  Groundedをtrueにする
        }
    }

    void Horizontal_Rotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            rotate_direction = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotate_direction = 1;
        }
        else
        {
            rotate_direction = 0;
        }

        // オブジェクトからみて垂直方向を軸として回転させるQuaternionを作成
        Quaternion rot = Quaternion.AngleAxis(rotate_direction * rotate_speed, transform.up);
        // 現在の自信の回転の情報を取得する。
        Quaternion q = this.transform.rotation;
        // 合成して、自身に設定
        this.transform.rotation = rot * q;
    }
}
