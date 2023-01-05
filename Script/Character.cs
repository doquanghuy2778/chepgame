using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Character là cha của player và enemy
public class Character : MonoBehaviour
{
    private float hp;
    private string currAnimName;
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText CombatTextPref;

    //getter setter (cach viet nhanh)
    public bool IsDeath => hp <= 0;


    //dung protected de ham con cung co the goi toi

    private void Start()
    {
        OnInit(); 
    }
    

    //giong ham khoi tao nhung duoc goi nhieu lan, ham khoi tao chi duoc goi 1 lan
    public virtual void OnInit()
    {

        hp = 100;
        //set mau bang 100       
        healthBar.OnInit(100, transform);      
    }

    //ham huy
    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }
    public void OnHit(float dame)
    {      
        if(!IsDeath)
        {
            hp -= dame;

            if(hp <= dame)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp); 
            Instantiate(CombatTextPref, transform.position + Vector3.up, Quaternion.identity).OnInit(dame);
            //prefab, vi tri, goc xoay
        }
    }
    protected void ChangeAnim(string animName)
    {
        if (currAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currAnimName = animName;
            anim.SetTrigger(currAnimName);
        }
    }
}
