using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialPopUps : MonoBehaviour
{
    [SerializeField] List<GameObject> popUps=new List<GameObject>();
    public UnityEvent OnPopUpsEnded;
    private void Awake()
    {
        foreach(GameObject go in popUps)
        {
            go.GetComponentInChildren<Button>().onClick.AddListener(() => ShowNextPopUp(go));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowNextPopUp(GameObject gameObject)
    {
        int index = popUps.FindIndex(x => x == gameObject);
        popUps[index].SetActive(false);
        if (index == popUps.Count - 1) OnPopUpsEnded?.Invoke();
        else popUps[index+1].SetActive(true);
    }
    public void ShowFirstPopUp()
    {
        popUps[0].SetActive(true);
    }

    private void OnDestroy()
    {
        foreach (GameObject go in popUps)
        {
            go.GetComponentInChildren<Button>().onClick.RemoveAllListeners() ;
        }
    }
}
