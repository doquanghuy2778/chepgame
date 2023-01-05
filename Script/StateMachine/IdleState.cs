 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float randomTime;
    float timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        timer = 0;  
        randomTime = Random.Range(2.5f, 4f);
    }

    //update state
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            enemy.ChangeState(new Patronstate());
        }
        else
        {
            enemy.ChangeState(new Patronstate());
        }
    }


    //thoat khoi state
    public void OnExit(Enemy enemy)
    {

    }
}
