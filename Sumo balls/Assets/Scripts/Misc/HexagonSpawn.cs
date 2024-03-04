using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HexagonSpawn : MonoBehaviour
{
    [Serializable]
    struct LinearFunctionParameters
    {
        public float a;
        public float b;
    }
    [SerializeField] List<LinearFunctionParameters> parameters = new List<LinearFunctionParameters>();
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [SerializeField] float _minX;
    [SerializeField] float _maxX;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
    }
    public Vector3 GetAvilablePosition()
    {
        float pointX = Random.Range(_minX, _maxX);
        float[] results= new float[parameters.Count];
        List<float> avilableYConstrains=new List<float>();
        for(int i=0;i<parameters.Count;i++)
        {
            results[i] = parameters[i].a * pointX + parameters[i].b;
        }
        for (int i = 0; i < parameters.Count; i++)
        {
            if (results[i] <= _minY) continue;
            if (results[i] >= _maxY) continue;
            avilableYConstrains.Add(results[i]);
        }
        if(avilableYConstrains.Count == 0)
        {
            avilableYConstrains.Add(_minY);
            avilableYConstrains.Add(_maxY);
        }
        avilableYConstrains.Sort();
        float pointY = Random.Range(avilableYConstrains[0], avilableYConstrains[1]);
        Vector3 pos = new Vector3(pointX, 0f, pointY);
        return pos;
    }
}
