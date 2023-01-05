using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    
    //enter: bat dau vao state
   public void OnEnter(Enemy enemy)
   {
        
   }

    //update state
    public void OnExecute(Enemy enemy)
    {

    }


    //thoat khoi state
    public void OnExit(Enemy enemy)
    {

    }
}
