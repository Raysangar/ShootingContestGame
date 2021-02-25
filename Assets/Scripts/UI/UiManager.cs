using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    public void Init(GameManager gameManager, WeaponSettings[] weaponSettings, LeaderboardManager leaderboardManager)
    {
        this.weaponSettings = weaponSettings;
        this.gameManager = gameManager;
        this.leaderboardManager = leaderboardManager;
        gameManager.OnGameFinished += OnGameFinished;
        gameManager.OnScoreChanged += OnScoreChanged;
        gameManager.Player.Weapon.OnShoot += OnWeaponShoot;
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        InitWeaponSelectionMenu(weaponSettings);
        ShowWeaponSelectionMenu();
    }

    private void InitWeaponSelectionMenu(WeaponSettings[] weaponSettings)
    {
        weapons = new WeaponPresenter[weaponSettings.Length];
        for (int i = 0; i < weapons.Length; ++i)
        {
            int iWeapon = i;
            weapons[i] = Instantiate(weaponPresenterPrefab, weaponPresenterParent);
            weapons[i].Init(weaponSettings[i], () => OnWeaponSelected(iWeapon));
        }
    }

    private void ShowWeaponSelectionMenu()
    {
        selectionMenuParent.SetActive(true);
        gameOverParent.SetActive(false);
        hudParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(weapons[0].gameObject);
    }

    private void OnWeaponSelected(int weaponIndex)
    {
        selectionMenuParent.SetActive(false);
        hudParent.SetActive(true);
        projectilesLeft.text = weaponSettings[weaponIndex].ClipSize.ToString();
        gameManager.StartGame(weaponIndex);
        UpdateLeaderBoard();
        AudioManager.Instance.PlayClickAudio();
    }

    private void OnScoreChanged(int score)
    {
        currentScore.text = score.ToString();
    }

    private void OnWeaponShoot(Weapon weapon)
    {
        projectilesLeft.text = weapon.ProjectilesLeft.ToString();
    }

    private void OnGameFinished(int finalScore)
    {
        this.finalScore.text = finalScore.ToString();
        hudParent.SetActive(false);
        gameOverParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playAgainButton.gameObject);
    }

    private void OnPlayAgainButtonClicked()
    {
        ShowWeaponSelectionMenu();
        AudioManager.Instance.PlayClickAudio();
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void UpdateLeaderBoard()
    {
        leaderboard.text = "HIGHEST SCORES";
        for (int i = 0; i < leaderboardManager.Leaderboard.Count; ++i)
            leaderboard.text += "\n" + (i + 1) + ". " + leaderboardManager.Leaderboard[i];
    }

    [Header("Selection Menu")]
    [SerializeField] GameObject selectionMenuParent;
    [SerializeField] WeaponPresenter weaponPresenterPrefab;
    [SerializeField] Transform weaponPresenterParent;
    private WeaponPresenter[] weapons;

    [Header("Game Over")]
    [SerializeField] GameObject gameOverParent;
    [SerializeField] TMPro.TMP_Text finalScore;
    [SerializeField] Button playAgainButton;
    [SerializeField] Button quitButton;

    [Header("HUD")]
    [SerializeField] GameObject hudParent;
    [SerializeField] TMPro.TMP_Text projectilesLeft;
    [SerializeField] TMPro.TMP_Text currentScore;
    [SerializeField] TMPro.TMP_Text leaderboard;

    private GameManager gameManager;
    private WeaponSettings[] weaponSettings;
    private LeaderboardManager leaderboardManager;
}