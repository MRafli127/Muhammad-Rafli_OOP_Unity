using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private VisualElement rootUI;
    private CombatManager combatManager;
    private HealthComponent healthComponent;
    private Label waveLabel;
    private Label pointsLabel;
    private Label enemiesLeftLabel;
    private Label timerLabel;
    private Label healthLabel;

    void Start()
    {
        InitializeUI();
        UpdateUI();
    }

    private void InitializeUI()
    {
        // Retrieve root UI element and components
        rootUI = GetComponent<UIDocument>()?.rootVisualElement;
        combatManager = FindObjectOfType<CombatManager>();
        healthComponent = FindObjectOfType<HealthComponent>();

        if (rootUI != null)
        {
            // Initialize UI elements
            waveLabel = rootUI.Q<Label>("Wave");
            pointsLabel = rootUI.Q<Label>("Point");
            enemiesLeftLabel = rootUI.Q<Label>("EnemiesLeft");
            timerLabel = rootUI.Q<Label>("Timer");
            healthLabel = rootUI.Q<Label>("Health");
        }
    }

    public void UpdateUI()
    {
        if (combatManager == null || healthComponent == null)
            return;

        int playerHealth = healthComponent.Health();

        // Update Wave
        waveLabel?.SetText($"Wave: {combatManager.waveNumber}");

        // Update Points
        pointsLabel?.SetText($"Points: {combatManager.points}");

        // Update Enemies Left
        enemiesLeftLabel?.SetText($"Enemies Left: {combatManager.totalEnemies}");

        // Update Timer
        if (timerLabel != null)
        {
            timerLabel.text = combatManager.timer > 0
                ? $"Next Wave in: {(int)(combatManager.GetWaveInterval() - combatManager.timer + 1)}"
                : null;
        }

        // Update Health
        if (playerHealth > 0)
        {
            healthLabel?.SetText($"Health: {playerHealth}");
        }
        else
        {
            ClearUIOnGameOver();
        }
    }

    private void ClearUIOnGameOver()
    {
        healthLabel?.SetText(null);
        waveLabel?.SetText(null);
        pointsLabel?.SetText(null);
        enemiesLeftLabel?.SetText(null);
        if (timerLabel != null)
        {
            timerLabel.text = $"Game Over!\nYour Points: {combatManager.points}";
        }
    }
}

// Extension method for Label to simplify setting text
public static class LabelExtensions
{
    public static void SetText(this Label label, string text)
    {
        if (label != null)
        {
            label.text = text;
        }
    }
}
