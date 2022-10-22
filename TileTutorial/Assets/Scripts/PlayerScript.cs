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
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public float jumpForce;
    private bool facingRight = true;
    private bool stageTwo = false;

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
        if (scoreValue == 8 && stageTwo == true)
           {
            win.gameObject.SetActive(true);
            Destroy(gameObject);

        }
        else if (scoreValue == 4 && stageTwo == false )
        {
            transform.position = new Vector2(42, 0 );
            livesValue = 3;
            stageTwo = true;
        }

    }
    void checkLose()
    {
        if(livesValue == 0)
        {
            lose.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if ( facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if ( facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (hozMovement > 0 && facingRight == true)
        {
            Debug.Log("facing right");
            
        }
        if (hozMovement < 0 && facingRight == false)
        {
            Debug.Log("facing left");
        }
        if (vertMovement > 0 && isOnGround == false)
        {
            Debug.Log("Jumping");
        }
        else if (vertMovement > 0 && isOnGround == true)
        {
            Debug.Log("not Jumping");
        }
        checkWin();
        checkLose();




    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}

