using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hit;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    public void OnInit()
    {
        rb.velocity = transform.right * 10f;
        Invoke(nameof(OnDespawn), 2f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    //tuong tac voi Enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(30f);
            //khi Enemy trung dao thi sinh ra Effet co vi tri va goc quay tuong ung
            Instantiate(hit, transform.position, transform.rotation);
            OnDespawn();
        } 
    }


}
