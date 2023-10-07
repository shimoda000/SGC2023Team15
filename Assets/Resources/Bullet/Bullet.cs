using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;   //ゲームオブジェクト取得

    public float speed;
    public Vector3 direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //敵のインスタンスを取得
            enemy enemy = collision.gameObject.GetComponent<enemy>();

            //ヒット処理
            enemy.Hit();

            //オブジェクトを破棄
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Nest")
        {
            //敵のインスタンスを取得
            nest nest = collision.gameObject.GetComponent<nest>();

            //ヒット処理
            nest.Hit();

            //オブジェクトを破棄
            Destroy(gameObject);
        }
    }
   

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = direction * speed * Time.deltaTime;
        //Debug.Log("dest" + destination);
        transform.Translate(destination,Space.World);
    }
}
