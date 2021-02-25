using UnityEngine;

public class Main : MonoBehaviour
{
    private void Awake()
    {
        player.Init(weaponSettings);
        var targets = targetsParent.GetComponentsInChildren<Target>(true);
        gameManager = new GameManager(player, playerInitialPosition, this, targets);
        leaderboardManager = new LeaderboardManager(gameManager);
        uiManager.Init(gameManager, weaponSettings, leaderboardManager);
        audioManager = new AudioManager(player, audioLibrary);
    }

    [SerializeField] UiManager uiManager;
    [SerializeField] PlayerController player;
    [SerializeField] Transform playerInitialPosition;
    [SerializeField] WeaponSettings[] weaponSettings;
    [SerializeField] AudioLibrary audioLibrary;
    [SerializeField] Transform targetsParent;

    private GameManager gameManager;
    private AudioManager audioManager;
    private LeaderboardManager leaderboardManager;
}
