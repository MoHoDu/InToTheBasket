using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBall : MonoBehaviour
{
    private Vector3 spawnPoint;             // 처음 생성 위치
    private float   ballSpeed = 2.0f;       // 이동 속도
    private float   jumpForce = 10.0f;       // 점프 파워
    private bool    isJump = false;          // 점프 상태 파악
    private Vector3 velocity;
    private float   gravity = -10.0f;
    [SerializeField]
    private GameObject lastBall;

    private void Awake()
    {
        spawnPoint = this.transform.position;
    }

    private void Update()
    {
        if (lastBall.transform.position.x > 10)
        {
            this.transform.position = spawnPoint;
            return;
        }
    }

    void FixedUpdate()
    {
        if (velocity.x < 5.0f)
            velocity.x += ballSpeed;

        if (isJump == true)
        {
            velocity.y = jumpForce;
            isJump = false;
        }  
        else
        {
            isJump = false;
            velocity.y += gravity * Time.deltaTime;
        }

        Vector3 currentVelocity = velocity * Time.deltaTime;
        transform.position += currentVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        velocity.y = 0;
        isJump = true;   
    }
}
