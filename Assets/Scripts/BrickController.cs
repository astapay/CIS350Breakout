using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.UpdateScore();
            Destroy(gameObject);
        }
    }
}
