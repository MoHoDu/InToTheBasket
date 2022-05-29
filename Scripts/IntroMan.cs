using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMan : MonoBehaviour
{
    private Vector3 spawnPoint;         // 처음 생성 위치
    private float   runSpeed = 2.0f;    // 이동 속도 
    private Vector3 velocity;
    [SerializeField]
    private GameObject lastBall;

    private void Awake()
    {
        spawnPoint = this.transform.position;
    }

    void Update()
    {
        if (lastBall.transform.position.x > 10)
        {
            this.transform.position = spawnPoint;
            return;
        }

        Rigidbody2D i = this.GetComponent<Rigidbody2D>();
        if (velocity.x < 5.0f)
            velocity.x += runSpeed;

        Vector3 currentVelocity = velocity * Time.deltaTime;
        transform.position += currentVelocity;

    }
}
