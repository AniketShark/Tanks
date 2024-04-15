using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField]
	private float _currentHealth;
	[SerializeField]
	private float _maxHealth;
	private void Awake()
	{
		_currentHealth = _maxHealth;
	}
	public float Damage(float amount)
	{
		_currentHealth -= amount;
		return Mathf.Clamp(_currentHealth, 0, _maxHealth);
	}
	public float Heal(float amount)
	{
		_currentHealth += amount;
		return Mathf.Clamp(_currentHealth, 0, _maxHealth);
	}
}
