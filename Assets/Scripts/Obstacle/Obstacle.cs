using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject Long;
    public GameObject Short;
    
    [SerializeField] public Collider ShortCollider;
    [SerializeField] public Collider LongCollider;

    public void SetLongObstacle()
    {
        Long.SetActive(true);
        Short.SetActive(false);
        
        ShortCollider.enabled = false;
        LongCollider.enabled = true;
    }

    public void SetShortObstacle()
    {
        Long.SetActive(false);
        Short.SetActive(true);

        ShortCollider.enabled = true;
        LongCollider.enabled = false;
    }
}
