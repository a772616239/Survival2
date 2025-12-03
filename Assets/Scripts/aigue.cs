using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aigue : Bullet
{
    
    internal Rigidbody2D rb;
    internal Vector3 LastVeclocity;
    internal bool CheckPos = true;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (GameManager.instan.Enemy.Count==0)
        {
            return;
        }
            var trages = GameManager.instan.Enemy[0];

            transform.position =
            Vector2.MoveTowards(this.transform.position,
            trages.transform.position,
            10f * Time.deltaTime);
            Vector2 direction =
                trages.transform.position
                - transform.position;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.gameObject.GetComponent<Rigidbody2D>().rotation = Angle;
    }

}
