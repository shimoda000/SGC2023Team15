using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemy;   //�Q�[���I�u�W�F�N�g�擾

    public float speed;
    public Vector3 direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //�G�̃C���X�^���X���擾
            enemy enemy = collision.gameObject.GetComponent<enemy>();

            //�q�b�g����
            enemy.Hit();

            //�I�u�W�F�N�g��j��
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Nest")
        {
            //�G�̃C���X�^���X���擾
            nest nest = collision.gameObject.GetComponent<nest>();

            //�q�b�g����
            nest.Hit();

            //�I�u�W�F�N�g��j��
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
