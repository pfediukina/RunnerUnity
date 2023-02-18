using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;

    void Awake()
    {
        if(_stateMachine == null)
            _stateMachine = new StateMachine(this);
    }

    void Update()
    {
        if(_stateMachine == null)
            _stateMachine.UpdateState();
    }
}