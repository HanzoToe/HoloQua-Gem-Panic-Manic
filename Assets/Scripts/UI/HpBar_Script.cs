using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class HpBar_Script : MonoBehaviour
{
    [SerializeField]
    private Player_hp _hp;  // Reference to the Player_hp script

    [SerializeField]
    private RectTransform _barRect;  // Reference to the RectTransform of the bar

    [SerializeField]
    private RectMask2D _mask;  // Reference to the RectMask2D component

    [SerializeField]
    private TMP_Text _hpIndicator;  // Reference to the TMP_Text component for displaying HP

    private float _maxRightMask;
    private float _initialRightMask;

    void Start()
    {
        // Initialize mask padding values
        _maxRightMask = _barRect.rect.width - _mask.padding.x - _mask.padding.z;
        _initialRightMask = _mask.padding.z;

        // Set initial HP display
        _hpIndicator.SetText($"{Player_hp.hp}/{_hp.maxhp}");

        // Subscribe to the HP change event
         Player_hp.OnHpChanged += UpdateHpBar;
    }



   


    void OnDestroy()
    {
        // Unsubscribe from the HP change event
        Player_hp.OnHpChanged -= UpdateHpBar;
    }

    private void UpdateHpBar(int currentHp)
    {
        // Calculate the new width for the HP bar
        var targetWidth = currentHp * _maxRightMask / _hp.maxhp;
        var newRightMask = _maxRightMask + _initialRightMask - targetWidth;

        // Update the mask padding
        var padding = _mask.padding;
        padding.z = newRightMask;
        _mask.padding = padding;

        // Update the HP text display
        _hpIndicator.SetText($"{currentHp}/{_hp.maxhp}");
    }
}
