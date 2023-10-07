using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;   //プレイヤーのゲームオブジェクト取得
   
    [SerializeField]
    private float m_Speed;         //速さ

    [SerializeField]
    private int m_nLife;           //体力

    //敵の状態の列挙
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
        m_state = STATE.NONE;
        m_nDamegeCounter = 0;

        m_Player = GameObject.Find("Player");
   }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case STATE.NONE:  //通常状態

                //色がもとに戻るよ
                GetComponent<SpriteRenderer>().material.color = Color.white;
                m_nDamegeCounter = 0;  //カウントをリセット
                break;

            case STATE.DAMEGE:  //ダメージ状態

                //ダメージをくらうと赤くなるよ
                Damege();
                break;
        }

        if (Vector2.Distance(transform.position, m_Player.transform.position) > 0.1f)
        {//プレイヤーとの距離が0.1未満になるまで

            //敵をプレイヤーの方に向かせる
            Quaternion rot = Quaternion.LookRotation(m_Player.transform.position - transform.position, Vector2.up);

            rot.y = 0;
            rot.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);

            //敵のプレイヤーの方に移動させる
            transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, m_Speed);
        }
    }

    //ゲームオブジェクトが何かに当たった時に呼び出される
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //敵同士の当たり判定
        if (collision.collider.tag == ("Enemy"))
        {
            //デバッグログ
            Debug.Log("敵と接触した！");
        }
    }

    //ヒット処理
    public void Hit()
    {//攻撃を受けたときに呼び出す

        //体力を減らす
        m_nLife--;

        //状態をダメージにする
        m_state = STATE.DAMEGE;

        if (m_nLife <= 0)
        {//体力が0以下

            //自分を破棄
            Destroy(gameObject);
        }
    }

    //色が変わる処理
    public void Damege()
    {
        GetComponent<SpriteRenderer>().material.color = Color.red;  //色を赤に変える

        //デバッグログ
        Debug.Log("色が変わった");

        m_nDamegeCounter++;  //カウントアップ

        if (m_nDamegeCounter >= 60)
        {//カウントが60以上になったら

            //状態を戻す
            m_state = STATE.NONE;
        }
    }
}
