﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 780.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

	// Use this for initialization
	void Start () {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //ジャンプ
        //if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0){
        //this.rigid2D.AddForce(transform.up * this.jumpForce);
        if (Input.GetMouseButtonDown(0) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //左右移動
        int key = 0;
        //if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        //if(Input.GetKey(KeyCode.LeftArrow)) key = -1;
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;

        //プレイヤーの速度
        float sppedx = Mathf.Abs(this.rigid2D.velocity.x);

        //スピード制限
        if(sppedx < this.maxWalkSpeed){
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //動く方向に応じて反転
        if(key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //プレイヤーの速度に応じてアニメーション速度を変更
        if(this.rigid2D.velocity.y == 0){
            this.animator.speed = sppedx / 2.0f;
        }else{
            this.animator.speed = 1.0f;
        }

        //画面外
        if(transform.position.y < -10) {
            SceneManager.LoadScene("GameScene");
        }
    }

    //ゴール
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("goal");
        SceneManager.LoadScene("ClearScene");
    }
}
