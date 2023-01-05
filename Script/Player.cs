using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //SerializeField để lấy từ bên ngoài vào
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 250f;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

     
    //
    private Vector3 savePoint;
    //
    private bool IsGrounded = true;
    private bool IsJumping = false;
    private bool IsAttack = false;
    //private bool isDeath = false;

    private float horizonal;
    
    private int coin = 0;
    // Start is called before the first frame update
    // Update is called once per frame

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
    }
    void Update()
    {
        IsGrounded = CheckGrounded();

        //horizonal huong trai phai ( -1 -> 0 -> 1: -1 khi bam sang trai; 0 thi khong bam gi; 1 khi bam sang phai)
        //horizonal = Input.GetAxisRaw("Horizontal");
        //verticle huong len xuong ( -1 -> 0 -> 1: -1 khi bam xuong; 0 khi khong bam gi; 1 khi bam len)
        //verticle = Input.GetAxisRaw("Vertical");
        if (IsAttack) 
        {
            rb.velocity = Vector2.zero;  
            return;
        }
        if (IsGrounded)
        {
            if (IsJumping)
            {
                return;
            } 
            //nhay
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            {
                Jump();
            }
            //run
            if(Mathf.Abs(horizonal) > 0.1f)
            {
                ChangeAnim("run");
            }

            //tan cong
            if (Input.GetKeyDown(KeyCode.J) && IsGrounded)
            {
                Attack();
            }

            //throw
            if (Input.GetKeyDown(KeyCode.K) && IsGrounded)
            {
                Throw();
            }
        }

        //roi
        if (!IsGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            IsJumping = false;
        }

        //chay
        if (Mathf.Abs(horizonal) >  0.1f)
        {           
            rb.velocity = new Vector2(horizonal * Time.fixedDeltaTime * speed, rb.velocity.y);
            //horizonal > 0 thi tra ve 0; horizontal <= 0 thi tra ve 180
            transform.rotation = Quaternion.Euler(new Vector3(0, horizonal > 0 ? 0 : 180, 0));
            //doi vi tri thi xoay nguoi theo huong do
            //transform.localScale = new Vector3(horizonal, 1, 1); -> khong nen dung cach nay vi trong 1 so truong hop se khong dk duoc
        } 
        else if(IsGrounded)
        {
            ChangeAnim("Idle");
            //neu khong bam thi van toc bang 0 
            rb.velocity = Vector2.zero;
        }
        if (IsDeath)
        {
            return;
        }
    }

    //reset thong so hoac dua ve trang thai dau tien
    public override void OnInit()
    {
        base.OnInit();       

        IsAttack = false;

        transform.position = savePoint;

        ChangeAnim("Idle");
        DeActiveAttack();
        SavePoint();
        UIManager.Instance.SetCoin(coin);  
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position  + Vector3.down * 1.1f, color: Color.red);

        //raycast (0,1,2,3): 0: vi trí đầu tiên
        //                   1: hướng
        //                   2: khoảng cách
        //                   3: đối tượng cần kiếm tra
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        //kiem tra ban trung
        if(hit.collider != null)
        {
            return true;
        } else
        {
            return false;   
        }
        // cach ngan hon
        // return hit.collider != null;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        IsAttack = true;
        Invoke(nameof(ResetAtackThrow), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw()
    {
        ChangeAnim("throw");
        IsAttack = true;    
        Invoke(nameof(ResetAtackThrow), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation); 
    } 

    public void Jump()
    {
        IsJumping = true;
        //ChangeAnim: truyen vao animation
        ChangeAnim("jump");
        rb.AddForce(Vector2.up * jumpForce);
        if (IsJumping)
        {
            return;
        }
    }

    private void ResetAtackThrow()
    {
        IsAttack = false;
        ChangeAnim("Idle");
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }



    //set di chuyen khi bam chuot
    public void SetMove(float horizontal)
    {
        this.horizonal = horizontal;
    }

    //va cham voi coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.Instance.SetCoin(coin);
            //khi va cham thi xoa dong tien do di
            Destroy(collision.gameObject);
        }

        //death
        if (collision.tag == "DeathZone")
        {
            ChangeAnim("die");

            Invoke(nameof(OnInit), 1f);
        }
    }
}