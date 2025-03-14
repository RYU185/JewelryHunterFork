using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float velocity = 3.0f;

    public float jump = 9.0f;
    bool goJump = false;
    bool onGround = false;

    Animator animator;
    string oldClip = "Idle";
    string newClip = "Idle";

    public static string gameState = "Playing";

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        //Application.targetFrameRate = 60;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() // 1/60�� (�� 0.016��)
    {
        if (gameState != "Playing")
        {
            return;
        }

        //Debug.Log(Time.deltaTime);
        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }     
        else if (axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // �ִϸ��̼� �÷��̴� Update()���� ó���ϴ°��� ȿ������
        if (onGround)
        {
            if (axisH == 0)
            {
                newClip = "Idle";
            }
            else
            {
                newClip = "Run";
            }
        }
        else
        {
            newClip = "Jump";
        }

        if (oldClip != newClip)
        {
            oldClip = newClip;
            animator.Play(newClip);
        }
    }

    private void FixedUpdate() // 0.02��
    {
        if (gameState != "Playing")
        {
            return;
        }

        // �׶��忡 ���ִ��� �����ϴ� �ڵ�
        onGround = Physics2D.Linecast(transform.position, 
            transform.position - (transform.up * 0.1f), 
            LayerMask.GetMask("Ground"));

        //Debug.Log(Time.fixedDeltaTime);
        if (onGround || axisH != 0)
        {
            rbody.velocity = new Vector2(axisH * velocity, rbody.velocity.y);
        }
        
        if (onGround && goJump)
        {
            rbody.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            goJump = false;
        }        
    }

    public void Jump()
    {
        goJump = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("�������� �Ϸ�");
            gameState = "gameClear";
            rbody.velocity = Vector2.zero;
            animator.Play("Clear");
        }
        else if (collision.gameObject.tag == "Dead")
        {
            gameState = "gameOver";
            rbody.velocity = Vector2.zero;
            rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            GetComponent<CapsuleCollider2D>().enabled = false;
            animator.Play("Dead");
        }

    }
}
