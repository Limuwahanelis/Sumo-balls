using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridButtonsNaviagtion : MonoBehaviour
{
    [SerializeField] GameObject _panelWithButtons;
    [SerializeField] GridLayoutGroup _grid;
    private List<Button> _allButtons;
    private List<List<Button>> _buttonsInGrid;
    private int _columns;
    private int _rows;
    private void Awake()
    {
        if (_grid.constraint != GridLayoutGroup.Constraint.FixedColumnCount) return;
        _columns = _grid.constraintCount;
        _rows = _grid.transform.childCount / _columns;
        _allButtons = _panelWithButtons.GetComponentsInChildren<Button>(true).ToList();
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

                if (i == 0)
                {
                    nav.selectOnUp = _buttonsInGrid[_rows - 1].ElementAt(j);
                }
                else if (i > _rows - 1)
                {
                    nav.selectOnUp = _buttonsInGrid[i - 1].ElementAt(j);
                    nav.selectOnDown = _buttonsInGrid[i + 1].ElementAt(j);

                }

                _buttonsInGrid[i].ElementAt(j).navigation = nav;
            }
        }
    }
    public void SetUpNavigation()
    {

    }
}
