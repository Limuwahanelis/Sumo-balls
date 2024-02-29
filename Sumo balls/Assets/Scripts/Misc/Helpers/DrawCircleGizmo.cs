using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleGizmo : MonoBehaviour
{
    [SerializeField] float _radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
