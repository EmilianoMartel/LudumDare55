using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpLogic : MonoBehaviour
{
    [SerializeField] private Color _liveColor;
    [SerializeField] private Color _deathColor;
    [SerializeField] private GameObject _total;
    [SerializeField] private List<string> _liveText;
    [SerializeField] private List<string> _deathText;
    [SerializeField] private TMPro.TMP_Text _wizardName;
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private float _timeWiew = 5f;

    private bool _isActive = false;
    public bool isActive { get {  return _isActive; } }

    private void Awake()
    {
        _isActive = false;
        _total.SetActive(false);
    }

    public void HandleResolution(Wizards wizard, bool isWizardWin)
    {
        _isActive = true;
        _total.SetActive(true);
        if (isWizardWin)
        {
            int index = UnityEngine.Random.Range(0,_liveText.Count);
            _wizardName.color = _liveColor;
            _wizardName.text = wizard.Name;
            _text.text = _liveText[index];
        }
        else
        {
            int index = UnityEngine.Random.Range(0, _deathText.Count);
            _wizardName.color = _deathColor;
            _wizardName.text = wizard.Name;
            _text.text = _deathText[index];
        }
        StartCoroutine(WaitForDeactive());
    }

    private IEnumerator WaitForDeactive()
    {
        yield return new WaitForSeconds(_timeWiew);
        _total.SetActive(false);
        _isActive = false;
    }
}
