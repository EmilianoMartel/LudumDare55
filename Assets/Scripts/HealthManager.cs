using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;

    public void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        _healthBar.fillAmount = 1.0f * _currentHealth / _maxHealth;
    }

    [ContextMenu("Get 20 point damage")]
    private void Get20Point()
    {
        ReceiveDagage(20);
        _currentHealth -= 20;
        _healthBar.fillAmount = 1.0f * _currentHealth / _maxHealth;
    }
    public void ReceiveDagage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.fillAmount = 1.0f * _currentHealth / _maxHealth;
    }
}
