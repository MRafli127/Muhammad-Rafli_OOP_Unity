using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    private VisualElement uiRoot;
    private CombatManager combatManager;
    private HealthComponent healthComponent;

    private Label waveLabel;
    private Label pointsLabel;
    private Label enemiesLeftLabel;
    private Label timerLabel;
    private Label healthLabel;

    void Start()
    {
        // Initialize UI and game components
        uiRoot = GetComponent<UIDocument>()?.rootVisualElement;
        combatManager = FindObjectOfType<CombatManager>();
        healthComponent = FindObjectOfType<HealthComponent>();

        // Initialize UI elements if uiRoot is not null
        if (uiRoot != null)
        {
            waveLabel = uiRoot.Q<Label>("Wave");
            pointsLabel = uiRoot.Q<Label>("Point");
            enemiesLeftLabel = uiRoot.Q<Label>("EnemiesLeft");
            timerLabel = uiRoot.Q<Label>("Timer");
            healthLabel = uiRoot.Q<Label>("Health");
        }

        UpdateUI(); // Initial UI update
    }

    public void UpdateUI()
    {
        // Ensure required components are present
        if (combatManager == null || healthComponent == null || uiRoot == null) return;

        // Update individual UI elements
        UpdateWave();
        UpdatePoints();
        UpdateEnemiesLeft();
        UpdateTimer();
        UpdateHealth();
    }

    private void UpdateWave()
    {
        waveLabel?.SetText($"Wave: {combatManager.waveNumber}");
    }

    private void UpdatePoints()
    {
        pointsLabel?.SetText($"Points: {combatManager.points}");
    }

    private void UpdateEnemiesLeft()
    {
        enemiesLeftLabel?.SetText($"Enemies Left: {combatManager.totalEnemies}");
    }

    private void UpdateTimer()
    {
        if (timerLabel == null) return;

        if (combatManager.timer > 0)
        {
            int timeLeft = Mathf.Max(0, (int)(combatManager.GetWaveInterval() - combatManager.timer + 1));
            timerLabel.SetText($"Next Wave in: {timeLeft}");
        }
        else
        {
            timerLabel.SetText(string.Empty);
        }
    }

    private void UpdateHealth()
    {
        if (healthLabel == null) return;

        int playerHealth = healthComponent.GetHealth();

        if (playerHealth > 0)
        {
            healthLabel.SetText($"Health: {playerHealth}");
        }
        else
        {
            DisplayGameOverUI();
        }
    }

    private void DisplayGameOverUI()
    {
        healthLabel.SetText(string.Empty);
        waveLabel?.SetText(string.Empty);
        pointsLabel?.SetText(string.Empty);
        enemiesLeftLabel?.SetText(string.Empty);
        timerLabel?.SetText($"Game Over!\nYour Points: {combatManager.points}");
    }
}

// Extension Method for Label to Set Text
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
