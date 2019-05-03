using UnityEngine;
using AIStates;

public class FirstState : State<AI>
{
    private static FirstState _instance;

    private FirstState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }  

    public static FirstState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FirstState();
            }

            return _instance;
        }
    }

    public override void EnterState(AI owner)
    {
        Debug.Log("Entering First State");
    }

    public override void ExitState(AI owner)
    {
        Debug.Log("Exiting First State");
    }

    public override void UpdateState(AI owner)
    {
        if (owner.switchState)
        {
            owner.stateMachine.ChangeState(SecondState.Instance);
        }
    }
}
