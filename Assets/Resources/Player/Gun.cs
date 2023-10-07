using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    public void Shot(Vector3 direction)
    {
        direction = direction.normalized;
        float rad = Mathf.Atan2(direction.y,direction.x);
        float deg = Mathf.Rad2Deg * rad;
        Bullet bullet = Instantiate(bulletPrefab,transform.position,Quaternion.Euler(0,0,deg));
        bullet.speed = 5.0f;
        bullet.direction = direction;
        //Debug.Log(direction);
    }
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
