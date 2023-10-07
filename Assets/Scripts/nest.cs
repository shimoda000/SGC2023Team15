using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    [SerializeField]
    //private GameObject m_enemy;   //�G�̃Q�[���I�u�W�F�N�g

    private GameObject enemyResource;

    [SerializeField]
    private int m_nCntSpringUp;   //�G���o������܂ł̃J�E���g

    [SerializeField]
    private int m_nFrame;         //�G���o�����鎞��

    [SerializeField]
    private int m_MaxSpawn;       //������o������N���̍ő吔
    private int m_nCntSpawn;      //������o�������N���̐�

    [SerializeField]
    private int m_nLife;          //�̗�

    private SpriteRenderer m_Color;

    //��Ԃ̗�
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

        if (m_nCntSpawn < m_MaxSpawn)
        {//�o���J�E���g���o���ő吔�����̂Ƃ�

            m_nCntSpringUp++;  //�o������܂ł̎��Ԃ��J�E���g�A�b�v

            if (m_nCntSpringUp % m_nFrame == 0)
            {//�o������܂ł̎��ԂƏo������t���[���̗]�肪0�̂Ƃ�

                //�G�𐶐�
                Instantiate(enemyResource, transform.position, Quaternion.identity);

                //�f�o�b�O���O
                Debug.Log("�G���o�Ă���");

                //�J�E���g��0�ɂ���
                m_nCntSpringUp = 0;

                //�o���J�E���g�A�b�v
                m_nCntSpawn++;
            }
        }

        if (m_nLife <= 0)
        {//�̗͂�0�ȉ�

            //������j��
            Destroy(gameObject);

            return;
        }
    }

    //�q�b�g����
    public void Hit()
    {//�U�����󂯂��Ƃ��ɌĂяo��

        //�̗͂����炷
        m_nLife--;

        //��Ԃ��_���[�W�ɂ���
        m_state = STATE.DAMEGE;

        
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