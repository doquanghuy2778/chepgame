using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patronstate : IState
{

    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(1f, 3f);
    }

    //update state
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(enemy.Target != null)
        {
            enemy.ChangeDiretion(enemy.Target.transform.position.x > enemy.transform.position.x);        
            if (enemy.IstargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
                 
        }
        else
        {           
            if (timer < randomTime)
            {               
                enemy.Moving();             
            }         
            else
            {
                enemy.ChangeState(new IdleState());              
            }
        }
       
    }


    //thoat khoi state
    public void OnExit(Enemy enemy)
    {

    }
}
