using UnityEngine;

public interface IState<T> where T : MonoBehaviour
{
    public void Enter(T owner);
    public void Exit(T owner);
    public void Update(T owner);
}