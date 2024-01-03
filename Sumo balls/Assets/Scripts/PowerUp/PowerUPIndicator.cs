using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPIndicator : MonoBehaviour
{
    [SerializeField] Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(_player.localPosition.x, 0, _player.localPosition.z);
    }
}
