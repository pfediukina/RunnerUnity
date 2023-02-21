using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject Long;
    public GameObject Short;
    
    [SerializeField] public Collider ShortCollider;
    [SerializeField] public Collider LongCollider;

    public void SetObstacle(bool isShort)
    {
        Short.SetActive(isShort);
        Long.SetActive(!isShort);
        
        ShortCollider.enabled = isShort;
        LongCollider.enabled = !isShort;
    }
}
