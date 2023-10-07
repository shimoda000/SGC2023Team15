using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("EnemyHit");
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
