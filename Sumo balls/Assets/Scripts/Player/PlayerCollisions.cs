using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisions : MonoBehaviour
{
    public UnityEvent<Collision> OnCollision;
    public UnityEvent<Collider> OnTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnCollision?.Invoke(collision); ;
    }
    private void OnTriggerEnter(Collider other)
    {
        OnTrigger?.Invoke(other);
    }
}
