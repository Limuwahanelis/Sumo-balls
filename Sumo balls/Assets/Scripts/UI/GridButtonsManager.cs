using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class GridButtonsManager : MonoBehaviour
{
    public Button Currentbutton => _currentlySelectedButton;
    [SerializeField] GameObject _panelWithButtons;
    [SerializeField] InputActionReference _changeTabsAction;
    [SerializeField]GridLayoutGroup _grid;
    private Button _currentlySelectedButton;
    private List<Button> _allButtons;
    private List<List<Button>> _buttonsInGrid;
    Vector2 _buttonIndex;
    private int _tabIndex = 0;
    private int _columns;
    private int _rows;
    private void Awake()
    {
        if (_grid.constraint != GridLayoutGroup.Constraint.FixedColumnCount) return;
        _columns = _grid.constraintCount;
        _rows = _grid.transform.childCount / _columns;
        _allButtons = _panelWithButtons.GetComponentsInChildren<Button>(true).ToList();
        foreach (Button button in _allButtons)
        {
            button.onClick.AddListener(OnButtonPressed);
        }
        _currentlySelectedButton = _allButtons[0];
        _currentlySelectedButton.Select();
        _buttonIndex = Vector2.zero;
        _buttonsInGrid = new List<List<Button>>();
        int _index = 0;
        for (int i = 0; i < _rows; i++)
        {
            _buttonsInGrid.Add(new List<Button>());
            for (int j = 0; j < _columns; j++)
            {
                _buttonsInGrid[i].Add(_allButtons[_index]);
                _index++;
            }
        }

        for (int i = 0; i < _rows; i++)
        {
            
            for (int j = 0; j < _columns; j++)
            {
                Navigation nav = new Navigation
                {
                    mode = Navigation.Mode.Explicit,
                };
                if (j == 0)
                {
                    nav.selectOnLeft = _buttonsInGrid[i].ElementAt(_columns - 1);
                    nav.selectOnRight = _buttonsInGrid[i].ElementAt(j + 1);
                }
                else if (j == _columns - 1)
                {
                    nav.selectOnRight = _buttonsInGrid[i].ElementAt(0);
                    nav.selectOnLeft = _buttonsInGrid[i].ElementAt(j - 1);
                }
                else
                {
                    nav.selectOnLeft = _buttonsInGrid[i].ElementAt(j - 1);
                    nav.selectOnRight = _buttonsInGrid[i].ElementAt(j + 1);
                }

                if(i==0)
                {
                    nav.selectOnUp = _buttonsInGrid[_rows - 1].ElementAt(j);
                }
                else if(i>_rows-1)
                {
                    nav.selectOnUp = _buttonsInGrid[i - 1].ElementAt(j);
                    nav.selectOnDown = _buttonsInGrid[i + 1].ElementAt(j);

                }

                _buttonsInGrid[i].ElementAt(j).navigation = nav;
            }
        }
    }
    private void Start()
    {
        

    }
    //private void OnEnable()
    //{
    //    SetActive(true);

    //}
    // Update is called once per frame
    void Update()
    {

    }
    private void GetNumberOfRowsAndColumns(out int column, out int row)
    {
         column = 0;
         row = 0;

        if (_grid.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = _grid.transform.
            GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < _grid.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = _grid.transform.
           GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }
    }
    //public void SetActive(bool value)
    //{
    //    if (value)
    //    {
    //        _buttonIndex = Vector2.zero;
    //        _changeTabsAction.action.performed += ChangeButton;
    //        _changeTabsAction.action.Enable();
    //        _currentlySelectedButton.Deselect();
    //        _currentlySelectedButton = _allButtons[0];
    //        _currentlySelectedButton.Select();
    //    }
    //    else
    //    {
    //        _changeTabsAction.action.performed -= ChangeButton;
    //        _changeTabsAction.action.Disable();
    //    }
    //}
    //private void ChangeButton(InputAction.CallbackContext context)
    //{
    //    Vector2 buttonChange = context.ReadValue<Vector2>();
    //    _buttonIndex += buttonChange;
    //    _currentlySelectedButton.Deselect();
    //    if (_buttonIndex.x >= _columns) _buttonIndex.x = 0;
    //    else if(_buttonIndex.x < 0) _buttonIndex.x = _columns-1;

    //    if (_buttonIndex.y >= _rows) _buttonIndex.y = 0;
    //    else if (_buttonIndex.y < 0) _buttonIndex.y = _rows - 1;

    //    _currentlySelectedButton = _buttonsInGrid[(int)_buttonIndex.y][(int)_buttonIndex.x];
    //    _currentlySelectedButton.Select();
    //}
    //private void OnButtonPressed(TabButtonUI button)
    //{
    //    if (button == _currentlySelectedButton) return;
    //    _currentlySelectedButton.Deselect();
    //    _currentlySelectedButton = button;

    //    for(int i=0;i<_rows;i++)
    //    {
    //        for(int j=0;j<_columns;j++)
    //        {
    //            if (_buttonsInGrid[i][j]==button)
    //            {
    //                _buttonIndex=new Vector2(_buttonIndex.x, _buttonIndex.y);
    //                return;
    //            }
    //        }
    //    }
    //    Debug.LogError("Button not found in grid");
    //}

    //private void OnDisable()
    //{
    //    SetActive(false);
    //}
    //private void OnDestroy()
    //{
    //    foreach (TabButtonUI button in _allButtons)
    //    {
    //        button.OnButtonClicked.RemoveListener(OnButtonPressed);
    //    }
    //}
}
