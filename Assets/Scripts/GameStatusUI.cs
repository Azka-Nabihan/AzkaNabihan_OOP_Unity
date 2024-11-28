using UnityEngine;
using UnityEngine.UIElements;

public class GameStatusUI : MonoBehaviour
{

    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    private CombatManager combatManager;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Menghubungkan elemen UI 
        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLeftLabel = root.Q<Label>("EnemiesLeft");

        player = Player.Instance;
        combatManager = FindAnyObjectByType<CombatManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && healthLabel != null){
            healthLabel.text = "Health: " + player.GetComponent<HealthComponent>().GetHealth(); 
        }
            

        if (combatManager != null)
        {
            if (pointsLabel != null)
                pointsLabel.text = "Points: " + combatManager.totalEnemiesKilled;
            
            if (waveLabel != null)
                waveLabel.text = "Wave: " + combatManager.waveNumber;

            if (enemiesLeftLabel != null)
                enemiesLeftLabel.text = "Enemies Left: " + combatManager.totalEnemies;
        }
    }

    
}
