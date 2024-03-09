using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HoleInArena : MonoBehaviour
{
    [SerializeField,Layer] int _playerLayer;
    [SerializeField,Layer] int _enemyLayer;
    [SerializeField,Layer] int _playerIgnoreArenaMask;
    [SerializeField,Layer] int _enemyIgnoreArenaMask;
    [SerializeField] float _holeRadius;
    List<BallData> _ballsAtTheHole=new List<BallData>();
    Vector3 _holePos;
    private struct BallData
    {
        public Collider col;
        public float ballRadius;
        public bool isEnemy;

    }
    // Start is called before the first frame update
    void Start()
    {
        _holePos = new Vector3(transform.position.x, 0, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!_ballsAtTheHole.Exists((x)=>x.col==other))
        {
            BallData ballData = new BallData();
            ballData.col = other.GetComponent<Collider>();
            ballData.isEnemy = other.attachedRigidbody.GetComponentInParent<Enemy>() ? true : false;
            ballData.ballRadius = other.transform.localScale.x/2;
            _ballsAtTheHole.Add(ballData);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        BallData ballToCheck = _ballsAtTheHole.Find((x) => x.col == other);
        Vector3 posTocheck = new Vector3(other.transform.position.x, 0, other.transform.position.z);
        Debug.Log(Vector3.Distance(_holePos, posTocheck));

       Debug.Log(Vector3.Distance(_holePos, posTocheck));
        if (Vector3.Distance(_holePos, posTocheck) < _holeRadius)
        {
            if(ballToCheck.isEnemy) other.attachedRigidbody.gameObject.layer = _enemyIgnoreArenaMask;
            else other.attachedRigidbody.gameObject.layer = _playerIgnoreArenaMask;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BallData ballToCheck = _ballsAtTheHole.Find((x) => x.col == other);
        if(ballToCheck.isEnemy) other.attachedRigidbody.gameObject.layer = _enemyLayer;
        else other.attachedRigidbody.gameObject.layer = _playerLayer;
        _ballsAtTheHole.Remove(ballToCheck);

    }
    private void OnValidate()
    {
        if (_holeRadius < 0) _holeRadius = 0;
        transform.localScale = new Vector3(_holeRadius*2,transform.localScale.y, _holeRadius*2);
    }
}
