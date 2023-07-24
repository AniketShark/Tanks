using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private float currentHealth;
	private float maxHealth;
	public float Damage(float amount)
	{
		currentHealth -= amount;
		return Mathf.Clamp(currentHealth, 0, maxHealth);
	}
	public float Heal(float amount)
	{
		currentHealth += amount;
		return Mathf.Clamp(currentHealth, 0, maxHealth);
	}
}
