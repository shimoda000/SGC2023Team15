using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    [SerializeField]
    //private GameObject m_enemy;   //敵のゲームオブジェクト

    private GameObject enemyResource;

    [SerializeField]
    private int m_nCntSpringUp;   //敵が出現するまでのカウント

    [SerializeField]
    private int m_nFrame;         //敵が出現する時間

    [SerializeField]
    private int m_MaxSpawn;       //巣から出現するクモの最大数
    private int m_nCntSpawn;      //巣から出現したクモの数

    [SerializeField]
    private int m_nLife;          //体力

    private SpriteRenderer m_Color;

    //状態の列挙
    private enum STATE
    {
        NONE,                      //なんもない
        DAMEGE,                    //ダメージ
    }

    STATE m_state;                 //状態  

    private int m_nDamegeCounter;  //ダメージ状態になっている時間

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        m_nCntSpringUp = 0;

        enemyResource = Resources.Load("Enemy\\enemy") as GameObject;

        m_Color = GetComponent<SpriteRenderer>();

        m_state = STATE.NONE;

        m_nDamegeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case STATE.NONE:  //通常状態

                //色がもとに戻るよ
                m_Color.material.color = Color.white;
                m_nDamegeCounter = 0;  //カウントをリセット
                break;

            case STATE.DAMEGE:  //ダメージ状態

                //ダメージをくらうと赤くなるよ
                Damege();
                break;
        }

        if (m_nCntSpawn < m_MaxSpawn)
        {//出現カウントが出現最大数未満のとき

            m_nCntSpringUp++;  //出現するまでの時間をカウントアップ

            if (m_nCntSpringUp % m_nFrame == 0)
            {//出現するまでの時間と出現するフレームの余りが0のとき

                //敵を生成
                Instantiate(enemyResource, transform.position, Quaternion.identity);

                //デバッグログ
                Debug.Log("敵が出てきた");

                //カウントを0にする
                m_nCntSpringUp = 0;

                //出現カウントアップ
                m_nCntSpawn++;
            }
        }

        if (m_nLife <= 0)
        {//体力が0以下

            //自分を破棄
            Destroy(gameObject);

            return;
        }
    }

    //ヒット処理
    public void Hit()
    {//攻撃を受けたときに呼び出す

        //体力を減らす
        m_nLife--;

        //状態をダメージにする
        m_state = STATE.DAMEGE;

        
    }

    //色が変わる処理
    public void Damege()
    {
        m_Color.material.color = Color.red;  //色を赤に変える

        m_nDamegeCounter++;  //カウントアップ

        if (m_nDamegeCounter >= 60)
        {//カウントが60以上になったら

            //状態を戻す
            m_state = STATE.NONE;
        }
    }
}