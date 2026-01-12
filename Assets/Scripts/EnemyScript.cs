using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public int maxHealth = 100; // Максимальное здоровье
    private int currentHealth;  // Текущее здоровье

    public GameObject waterIcon;
    public GameObject victoryText;

   

    private void Start()
    {
        currentHealth = maxHealth; // Инициализация текущего здоровья
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Уменьшаем здоровье на величину урона
        if (currentHealth <= 0)
        {
            Die(); // Если здоровье меньше или равно нулю, вызываем метод смерти
        }
    }

    private void Die()
    {
        // Удаление магического эффекта, если он существует
        if (waterIcon != null)
        {
            Destroy(waterIcon);
        }

        // Помечаем, что игрок атаковал персонажа (выполнил задание)
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.attackedCharacter = true;
        }

        if (victoryText != null)
        {
            victoryText.SetActive(true);
            StartCoroutine(EndFightAfterDelay(2f)); // Показать текст и подождать 2 секунды
        }
        else
        {
            Destroy(gameObject); // Если текста нет — сразу уничтожаем врага
        }
    }

    private IEnumerator EndFightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (victoryText != null)
            victoryText.SetActive(false);
        Destroy(gameObject); // Уничтожаем врага после задержки
    }

    public void StartFight()
    {
        if (waterIcon != null)
            waterIcon.SetActive(true);
    }

    public void EndFight()
    {
        

        // Удаление магического эффекта, если он существует
        if (waterIcon != null)
        {
            Destroy(waterIcon);
        }

        if (waterIcon != null)
            waterIcon.SetActive(false);

    }
}