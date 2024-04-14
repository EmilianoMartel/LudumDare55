using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonView : MonoBehaviour
{
    [SerializeField] private DemonCreatorController _controller;
    [SerializeField] private ConfirmLogic _confirmLogic;
    [SerializeField] private Image _face;
    [SerializeField] private TMPro.TMP_Text _name;
    [SerializeField] private TMPro.TMP_Text _type;
    [SerializeField] private TMPro.TMP_Text _category;
    private Demon _demon;

    private void OnEnable()
    {
        _controller.sendDeamonAction += HandleDemon;
    }

    private void OnDisable()
    {
        _controller.sendDeamonAction -= HandleDemon;
    }

    private void HandleDemon(Demon demon, Sprite face)
    {
        _demon = demon;
        _name.text = demon.ShowName();
        _type.text = demon.ShowType().Modifier;
        _category.text = demon.ShowCategoryName();
        _face.sprite = face;
    }
}