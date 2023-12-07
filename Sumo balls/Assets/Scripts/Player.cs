using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] GameObject _pivot;
    [SerializeField] GameObject _powerUpIndicator;
    [SerializeField] float _force;
    [SerializeField] float _powerUpStrength = 15f;
    [SerializeField] float _powerUpDuration;
    [SerializeField] private bool _hasPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushBall(float direction)
    {
        _playerRB.AddForce(_pivot.transform.forward*_force*direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerUp>()) 
        {
            _hasPowerUp = true;
            _powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountdownCor());
            Debug.Log("picked");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy && _hasPowerUp)
        {
            Debug.Log("dsad");
            Vector3 pushVector = (enemy.transform.position - transform.position)* _powerUpStrength;
            enemy.Push(pushVector);
        }
    }

    private IEnumerator PowerUpCountdownCor()
    {
        yield return new WaitForSeconds(_powerUpDuration);
        _hasPowerUp = false;
        _powerUpIndicator.SetActive(false);
    }
}
