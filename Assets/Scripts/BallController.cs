using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private bool hasBeenLaunched;
    [SerializeField] private GameObject paddle;
    [SerializeField] private float paddleYOffset;
    [SerializeField] private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(10,10);
    }

    private void Update()
    {
        if(!hasBeenLaunched)
        {
            transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + paddleYOffset, paddle.transform.position.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>())
        {
            if(collision.gameObject.name == "Background")
            {
                lives--;
                hasBeenLaunched = false;

                if (lives <= 0)
                {
                    gm.LoseGame();
                }
            }
        }
    }

    public void Launch()
    {
        if(!hasBeenLaunched)
        {
            hasBeenLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
        }
    }
}
