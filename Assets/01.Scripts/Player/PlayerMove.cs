using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0);
        dir.Normalize();

        transform.localScale = x >= 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1); 

        if(x != 0 || y != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        transform.position += dir * _speed * Time.deltaTime;

        

    }
}
