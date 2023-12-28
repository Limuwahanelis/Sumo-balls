using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

[CreateAssetMenu(menuName ="ButtonStatesColors")]
public class ButtonStatesColors : ScriptableObject
{
    [SerializeField] Color _normalColor =new Color(1,1,1);
    [SerializeField] Color _highlightedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f,1f);
    [SerializeField] Color _pressedColor = new Color(0.7843137f, 0.7843137f, 0.7843137f,1f);
    [SerializeField] Color _selectedColor = new Color(0.9607843f, 0.9607843f, 0.9607843f,1f);
    [SerializeField] Color _disabledColor = new Color(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

    public Color NormalColor => _normalColor;
    public Color HighlightedColor => _highlightedColor;
    public Color SelectedColor => _selectedColor;
    public Color DisabledColor => _disabledColor;
    public Color pressedColor => _pressedColor;
}
