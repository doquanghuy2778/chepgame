using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            //doi huong Enemy toi huong player
            enemy.ChangeDiretion(enemy.Target.transform.position.x > enemy.transform.position.x);           
            enemy.StopMoving();
            enemy.Attack();
        }

        timer = 0; 
    }

    //update state
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if(timer >= 1.5f)
        {
            enemy.ChangeState(new Patronstate());
        }
    }


    //thoat khoi state
    public void OnExit(Enemy enemy)
    {

    }
}
