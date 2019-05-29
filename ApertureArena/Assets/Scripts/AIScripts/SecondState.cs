using UnityEngine;
using AIStates;

// Attacking State
public class SecondState : State<AI>
{
    private static SecondState _instance;

    private SecondState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static SecondState Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SecondState();
            }
            return _instance;
        }
    }

    public void StateBehavior()
    {

    }

    public override void EnterState(AI owner)
    {
        Debug.Log("Entering Second State");
    }

    public override void ExitState(AI owner)
    {
        Debug.Log("Exiting Second State");
    }

    public override void UpdateState(AI owner)
    {

    }
}