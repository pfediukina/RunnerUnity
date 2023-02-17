using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _touchLength = 3f;

    private Rigidbody _rb;
    private Vector2 _startTouch;
    private Vector2 _lastDirection;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _touchLength = Screen.width / 3;
    }

    void OnEnable()
    {
        Touch.OnDownEvent += PointerDown;
        Touch.OnUpEvent += PointerUp;
        Touch.OnDragEvent += PointerDrag;
    }

    void OnDisable()
    {
        Touch.OnDownEvent -= PointerDown;
        Touch.OnUpEvent -= PointerUp;
        Touch.OnDragEvent -= PointerDrag;
    }

    private void PointerDown(Vector2 pos)
    {
        _startTouch = pos;
    }

    private void PointerUp(Vector2 pos)
    {
        //WIP
    }

    private void PointerDrag(Vector2 pos)
    {
        if(Vector2.Distance(_startTouch, pos) >= _touchLength)
        {
            CheckSwipeDirection(pos - _startTouch);
            _startTouch = pos;
        }
    }

    private void CheckSwipeDirection(Vector2 delta)
    {
        _lastDirection = Vector2.zero;

        if(Mathf.Abs(delta.y) > _touchLength)
            _lastDirection += delta.y > 0 ? Vector2.up : Vector2.down; 

        if(Mathf.Abs(delta.x) > _touchLength)
            _lastDirection += delta.x > 0 ? Vector2.right : Vector2.left;   

        TemporaryJump();
    }

    public void TemporaryJump()
    {
        if(_lastDirection.y > 0 && _rb.velocity.magnitude == 0)
        {
            _rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
