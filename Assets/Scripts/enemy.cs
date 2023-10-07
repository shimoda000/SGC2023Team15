using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;   //�v���C���[�̃Q�[���I�u�W�F�N�g�擾
   
    [SerializeField]
    private float m_Speed;         //����

    [SerializeField]
    private int m_nLife;           //�̗�

    private SpriteRenderer m_Color;

    //�G�̏�Ԃ̗�
    private enum STATE
    {
        NONE,                      //�Ȃ���Ȃ�
        DAMEGE,                    //�_���[�W
    }

    STATE m_state;                 //���   

    private int m_nDamegeCounter;  //�_���[�W��ԂɂȂ��Ă��鎞��

    // Start is called before the first frame update
    void Start()
    {
        //������
        m_Player = GameObject.Find("Player");
        m_Color = transform.Find("enemy").GetComponent<SpriteRenderer>();

        m_state = STATE.NONE;
        m_nDamegeCounter = 0;
   }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case STATE.NONE:  //�ʏ���

                //�F�����Ƃɖ߂��
                m_Color.material.color = Color.white;
                m_nDamegeCounter = 0;  //�J�E���g�����Z�b�g
                break;

            case STATE.DAMEGE:  //�_���[�W���

                //�_���[�W�����炤�ƐԂ��Ȃ��
                Damege();
                break;
        }

        if (Vector2.Distance(transform.position, m_Player.transform.position) > 0.1f)
        {//�v���C���[�Ƃ̋�����0.1�����ɂȂ�܂�

            //�G���v���C���[�̕��Ɍ�������
            Quaternion rot = Quaternion.LookRotation(m_Player.transform.position - transform.position, Vector2.up);

            rot.y = 0;
            rot.x = 0;

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);

            //�G�̃v���C���[�̕��Ɉړ�������
            transform.position = Vector2.MoveTowards(transform.position, m_Player.transform.position, m_Speed);
        }
    }

    //�Q�[���I�u�W�F�N�g�������ɓ����������ɌĂяo�����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ////�G���m�̓����蔻��
        //if (collision.collider.tag == ("Enemy"))
        //{
            
        //}
    }

    //�q�b�g����
    public void Hit()
    {//�U�����󂯂��Ƃ��ɌĂяo��

        //�̗͂����炷
        m_nLife--;

        //��Ԃ��_���[�W�ɂ���
        m_state = STATE.DAMEGE;

        if (m_nLife <= 0)
        {//�̗͂�0�ȉ�

            //������j��
            Destroy(gameObject);
        }
    }

    //�F���ς�鏈��
    public void Damege()
    {
        m_Color.material.color = Color.red;  //�F��Ԃɕς���
        
        m_nDamegeCounter++;  //�J�E���g�A�b�v

        if (m_nDamegeCounter >= 60)
        {//�J�E���g��60�ȏ�ɂȂ�����

            //��Ԃ�߂�
            m_state = STATE.NONE;
        }
    }
}
