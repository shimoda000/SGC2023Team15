using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;   //敵のゲームオブジェクト

    private GameObject enemyResource;

    [SerializeField]
    private int m_nCntSpringUp;   //敵が出現するまでのカウント

    [SerializeField]
    private int m_nFrame;         //敵が出現する時間

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        m_nCntSpringUp = 0;

        enemyResource = Resources.Load("Enemy\\enemy") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        m_nCntSpringUp++;  //カウントアップ

        if(m_nCntSpringUp % m_nFrame == 0)
        {//出現するまでの時間と出現するフレームの余りが0のとき
            
            //敵を生成
            Instantiate(enemyResource, transform.position, Quaternion.identity);

            //デバッグログ
            Debug.Log("敵が出てきた");
            
            //カウントを0にする
            m_nCntSpringUp = 0;  
        }
    }
}