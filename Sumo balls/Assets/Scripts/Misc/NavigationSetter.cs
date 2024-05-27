using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationSetter : MonoBehaviour
{
    [SerializeField,HideInInspector]Selectable _selectable;
    private Navigation _startingNavSettings;
    protected void Awake()
    {
        _selectable = GetComponent<Selectable>();
        _startingNavSettings = _selectable.navigation;
    }
    public void SetNavigationMode(bool isExplicit)
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        Navigation nav = _selectable.navigation;
        nav.mode = isExplicit ? Navigation.Mode.Explicit : Navigation.Mode.Automatic;
        _selectable.navigation = nav;
    }
    public void SetSelectableOnTop(Selectable selectable)
    {
        if(_selectable == null) _selectable = GetComponent<Selectable>();
        Navigation nav = _selectable.navigation;
        nav.selectOnUp = selectable;
        _selectable.navigation = nav;
    }
    public void SetSelectableOnDown(Selectable selectable)
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        Navigation nav = _selectable.navigation;
        nav.selectOnDown = selectable;
        _selectable.navigation = nav;
    }
    public void SetSelectableOnLeft(Selectable selectable)
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        Navigation nav = _selectable.navigation;
        nav.selectOnLeft = selectable;
        _selectable.navigation = nav;
    }
    public void SetSelectableOnRight(Selectable selectable)
    {
        if (_selectable == null) _selectable = GetComponent<Selectable>();
        Navigation nav = _selectable.navigation;
        nav.selectOnRight = selectable;
        _selectable.navigation = nav;
    }
    public void ResetNavigation()
    {
        _selectable.navigation = _startingNavSettings;
    }
}
