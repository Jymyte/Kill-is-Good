using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
  private float health;
  private float lerpTimer;
  public float maxHealth = 100f;
  public float chipSpeed = 2f;
  public Image frontHealthBar;
  public Image backHealthBar;
  [SerializeField]
  private TextMeshProUGUI healthTMP;
  private string healthText;

  // Start is called before the first frame update
  void Start()
  {
    health = maxHealth;
    UpdateHealthText();
  }

  // Update is called once per frame
  void Update()
  {
    health = Mathf.Clamp(health, 0, maxHealth);
    UpdateHealthUI();
    if(Input.GetKeyDown(KeyCode.A)) TakeDamage(Random.Range(5, 10));
    if(Input.GetKeyDown(KeyCode.D)) RestoreHealth(Random.Range(5, 10));
  }

  public void UpdateHealthUI() {
    float fillF = frontHealthBar.fillAmount;
    float fillB = backHealthBar.fillAmount;
    float hFraction = health / maxHealth;
    if (fillB > hFraction) {
      frontHealthBar.fillAmount = hFraction;
      backHealthBar.color = Color.red;
      lerpTimer += Time.deltaTime;
      float percentComplete = lerpTimer / chipSpeed;
      percentComplete = percentComplete * percentComplete;
      backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
    } else {
      backHealthBar.color = Color.green;
      backHealthBar.fillAmount = hFraction;
      lerpTimer += Time.deltaTime;
      float percentComplete = lerpTimer / chipSpeed;
      percentComplete = percentComplete * percentComplete;
      frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
    }
  }

  public void TakeDamage(float damage) {
    health -= damage;
    lerpTimer = 0f;
    UpdateHealthText();
  }

  public void RestoreHealth(float healAmount) {
    health += healAmount;
    lerpTimer = 0f;
    UpdateHealthText();
  }

  void UpdateHealthText() {
    if (health > maxHealth) health = maxHealth;
    healthText = health + " / " + maxHealth;
    healthTMP.text = healthText;
  }
}
