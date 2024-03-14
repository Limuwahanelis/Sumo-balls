using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private List<LinearFunctionParameters> offsetParameters;
    private void Awake()
    {
        offsetParameters = new List<LinearFunctionParameters>(parameters);
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Update()
    {
    }
    // offset is r
    public Vector3 GetAvilablePosition(float xOffset = 0, float yOffset = 0)
    {
        float newMinX = _minX + xOffset * math.SQRT2;
        float newMaxX = _maxX - xOffset * math.SQRT2;
        float pointX = Random.Range(newMinX, newMaxX);
        float newMaxY = _maxY - yOffset * math.SQRT2;
        float newMinY = _minY + yOffset * math.SQRT2;
        float yToSub = parameters[0].a * newMaxY + parameters[0].b;
        float[] results = new float[parameters.Count];
        List<float> avilableYConstrains = new List<float>();
        for (int i = 0; i < parameters.Count; i++)
        {
            results[i] = parameters[i].a * pointX + parameters[i].b;
            if (yOffset != 0)
            {
                if (parameters[i].a >= 0)
                {
                    if (Math.Abs(parameters[i].a * _minX + parameters[i].b - 0) <= 0.001) results[i] =math.clamp(results[i] - yToSub,0,newMaxY);
                    else if (Math.Abs(parameters[i].a * _maxX + parameters[i].b - 0) <= 0.001) results[i] = math.clamp(results[i] + yToSub,newMinY,0);
                }
                else
                {
                    if (Math.Abs(parameters[i].a * _minX + parameters[i].b - 0) <= 0.001) results[i] = math.clamp(results[i] + yToSub, newMinY, 0);
                    else if (Math.Abs(parameters[i].a * _maxX + parameters[i].b - 0) <= 0.001) results[i] = math.clamp(results[i] - yToSub, 0, newMaxY);
                }
            }
        }
        for (int i = 0; i < parameters.Count; i++)
        {
            if (results[i] <= newMinY ) continue;
            if (results[i] >= newMaxY ) continue;
            avilableYConstrains.Add(results[i]);
        }
        if (avilableYConstrains.Count == 0)
        {
            avilableYConstrains.Add(newMinY );
            avilableYConstrains.Add(newMaxY );
        }
        avilableYConstrains.Sort();
        float pointY = Random.Range(avilableYConstrains[0], avilableYConstrains[1]);
        Vector3 pos = new Vector3(pointX, 0f, pointY);
        return pos;
    }
    public Vector3 GetAvilablePositionForSphereInside(float radius)
    {
        if (radius > _maxY) Debug.LogError("Radius is too big ");
        Debug.Log(radius);
        if (offsetParameters == null|| offsetParameters.Count==0) offsetParameters= new List<LinearFunctionParameters>(parameters);
        float offset = (radius * 2) / math.sqrt(3);
        Debug.Log("off: "+offset);
        for (int i=0;i<parameters.Count;i++)
        {
            LinearFunctionParameters newParam = new LinearFunctionParameters()
            {
                a = parameters[i].a,
                b = parameters[i].b
            };
            if (i == 1) newParam.b = _maxY - radius;
            else if(i==4) newParam.b = _minY + radius;
            offsetParameters[i] = newParam;
        }
        float pointXOrg = Random.Range(_minX+offset, _maxX-offset);
        float pointX = pointXOrg;
        Debug.Log("x:" + pointXOrg);
        float[] results = new float[offsetParameters.Count];
        List<float> avilableYConstrains = new List<float>();
        for (int i = 0; i < offsetParameters.Count; i++)
        {
            if (Math.Abs(parameters[i].a * _minX + parameters[i].b - 0) <= 0.001) pointX = pointXOrg - offset;
            else if (Math.Abs(parameters[i].a * _maxX + parameters[i].b - 0) <= 0.001) pointX = pointXOrg + offset;
            Debug.Log("calaculate for x: " + pointX);
            results[i] = offsetParameters[i].a * pointX + offsetParameters[i].b;
            Debug.Log(string.Format("i:{0} y:{1}",i,results[i]));
        }
        for (int i = 0; i < offsetParameters.Count; i++)
        {
            if (results[i] <= _minY + radius) continue;
            if (results[i] >= _maxY - radius) continue;
            avilableYConstrains.Add(results[i]);
        }

        if (avilableYConstrains.Count == 0)
        {
            avilableYConstrains.Add(_minY + radius);
            avilableYConstrains.Add(_maxY - radius);
        }
        Debug.Log("constr: " + avilableYConstrains.Count);
        avilableYConstrains.Sort();
        // due to floatingp oint error somteimes max and min value are also added, but shouldn't. Here we remove them.
        if (avilableYConstrains.Count > 2)
        {
            avilableYConstrains.RemoveAt(avilableYConstrains.Count - 1);
            avilableYConstrains.RemoveAt(0);
        }
        float pointY = Random.Range(avilableYConstrains[0], avilableYConstrains[1]);
        Debug.Log("y:" + pointY);
        Vector3 pos = new Vector3(pointXOrg, 0f, pointY);
        return pos;
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
