using TMPro;
using UnityEngine;

public class HSVText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Color _currentTextColor = Color.red;
    [SerializeField] private float _colorScrollSpeed = 0.5f;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        CalculateColor();
    }

    private void CalculateColor()
    {
        Color.RGBToHSV(_currentTextColor, out float h, out float s, out float v);

        h += _colorScrollSpeed * Time.deltaTime;
        if (h > 360f)
            h = 0;
        _text.color = _currentTextColor = Color.HSVToRGB(h, s, v);
    }
}
