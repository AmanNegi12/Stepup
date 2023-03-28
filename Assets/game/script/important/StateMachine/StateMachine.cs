using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
   
    State currentState;

    public void SwitchState(State newstate)
    {
        if (currentState!=null)
        {
            currentState.Exit();
        }
        currentState = newstate;
        if (currentState!=null)
        {
          currentState.Enter();

        }
    }
    private void Update()
    {
        if (currentState!=null)
        {
            currentState.Tick(Time.deltaTime);
        }
    }

}
