using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour

{
    private Rigidbody2D rd2d;
    public Text score;
    public Text win;
    public float speed;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    public Text lose;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        win.gameObject.SetActive(false);
        lives.text = "Lives: " + livesValue.ToString();
        lose.gameObject.SetActive(false);
        
    }
    void checkWin()
    {
        if (scoreValue == 4)
           {
            win.gameObject.SetActive(true);
           }

    }
    void checkLose()
    {
        if(livesValue == 0)
        {
            lose.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            checkWin();
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            checkLose();

        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}

