
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TatetiCell : MonoBehaviour
{
    private Tateti _tateti;
    private Button _button;
    private void Awake()
    {
        _tateti = GetComponentInParent<Tateti>();
        
        _button= GetComponent<Button>();
        _button.onClick.AddListener(PressCell);
        _button.onClick.AddListener(_tateti.Check);
    }

    private void PressCell()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = _tateti.playerATurn ? "X":"O";
        _tateti.playerATurn = !_tateti.playerATurn;
        _button.interactable = false;
    }
}
