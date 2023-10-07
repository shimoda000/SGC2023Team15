using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : MonoBehaviour
{
    //�ړ����x
    [SerializeField]
    private float _speed = 3.0f;

    //x�������̓��͂�ۑ�
    private float _input_x;
    //z�������̓��͂�ۑ�
    private float _input_y;

    [SerializeField]
    Gun gun;

    [SerializeField]
    GameObject dbgObj;

    Vector3 direction;

    void Update()
    {
        //x�������Az�������̓��͂��擾
        //Horizontal�A�����A�������̃C���[�W
        _input_x = Input.GetAxis("Horizontal");
        //Vertical�A�����A�c�����̃C���[�W
        _input_y = Input.GetAxis("Vertical");

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //mousePos = new Vector3(mousePos.x,mousePos.y,0);
        //dbgObj.transform.position = mousePos;
        

        //�ړ��̌����ȂǍ��W�֘A��Vector3�ň���
        Vector3 velocity = new Vector3(_input_x, _input_y, 0);
        //�x�N�g���̌������擾
        direction = velocity.normalized;

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 diff = mousePos - transform.position;
            gun.Shot(direction);
        }

        //�ړ��������v�Z
        float distance = _speed * Time.deltaTime;
        //�ړ�����v�Z
        Vector3 destination = transform.position + direction * distance;

        float rad = Mathf.Atan2(direction.y,direction.x);
        float deg = Mathf.Rad2Deg * rad;

        //�ړ���Ɍ����ĉ�]
        transform.rotation = Quaternion.Euler(0,0,deg);
        //�ړ���̍��W��ݒ�
        transform.position = destination;
    }
}
