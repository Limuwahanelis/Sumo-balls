using System;
using UnityEngine;
using UnityEngine.UI;

public static class GridLayoutGroupHelper
{
    public static Vector2Int Size(this GridLayoutGroup grid)
    {
        int itemsCount = grid.transform.childCount;
        Vector2Int size = Vector2Int.zero;

        if (itemsCount == 0)
            return size;

        switch (grid.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                size.x = grid.constraintCount;
                size.y = getAnotherAxisCount(itemsCount, size.x);
                break;

            case GridLayoutGroup.Constraint.FixedRowCount:
                size.y = grid.constraintCount;
                size.x = getAnotherAxisCount(itemsCount, size.y);
                break;

            case GridLayoutGroup.Constraint.Flexible:
                size = flexibleSize(grid);
                break;

            default:
                throw new ArgumentOutOfRangeException($"Unexpected constraint: {grid.constraint}");
        }

        return size;
    }

    private static Vector2Int flexibleSize(this GridLayoutGroup grid)
    {
        int itemsCount = grid.transform.childCount;
        float prevX = float.NegativeInfinity;
        int xCount = 0;

        for (int i = 0; i < itemsCount; i++)
        {
            Vector2 pos = ((RectTransform)grid.transform.GetChild(i)).anchoredPosition;

            if (pos.x <= prevX)
                break;

            prevX = pos.x;
            xCount++;
        }

        int yCount = getAnotherAxisCount(itemsCount, xCount);
        return new Vector2Int(xCount, yCount);
    }

    private static int getAnotherAxisCount(int totalCount, int axisCount)
    {
        return totalCount / axisCount + Mathf.Min(1, totalCount % axisCount);
    }
    public static void GetNumberOfItemsInRow(GridLayoutGroup glg, out int itemsNum, int row)
    {
        int curRow = 1;
        itemsNum = 0;
        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        int column = 1;
        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.
            GetChild(0).GetComponent<RectTransform>();

        itemsNum++;
        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;
        
        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.
           GetChild(i).GetComponent<RectTransform>();
            
            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
                itemsNum = 0;
            }
            else
            {
                itemsNum++;
                if (!stopCountingRow)
                {
                    if (curRow == row) return;
                }
                curRow++;
                itemsNum = 0;
            }
        }
    }
    public static void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.
            GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.
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
}