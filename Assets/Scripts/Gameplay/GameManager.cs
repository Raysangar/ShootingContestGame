using System.Collections;
using UnityEngine;

public class GameManager
{
    public int Score { get; private set; }
    public PlayerController Player { get; private set; }

    public System.Action<int> OnGameFinished;
    public System.Action<int> OnScoreChanged;

    public GameManager(PlayerController player, Transform playerInitialPosition, MonoBehaviour mainObject, Target[] targets)
    {
        Player = player;
        this.mainObject = mainObject;
        this.playerInitialPosition = playerInitialPosition;
        this.targets = targets;
        player.enabled = false;
        player.transform.position = playerInitialPosition.position;
        player.Weapon.OnOutOfProjectiles += OnPlayerOutOfProjectiles;
        foreach (var target in targets)
            target.OnHit += OnTargetHit;
    }

    public void StartGame(int weaponIndex)
    {
        Player.transform.position = playerInitialPosition.position;
        Player.StartGame(weaponIndex);
        foreach (var target in targets)
            target.ResetParent();
        Score = 0;
        OnScoreChanged(Score);
    }

    private void OnPlayerOutOfProjectiles()
    {
        mainObject.StartCoroutine(WaitForEndGame());
    }

    private void OnTargetHit(int score)
    {
        Score += score;
        OnScoreChanged(Score);
    }

    private IEnumerator WaitForEndGame()
    {
        yield return WaitSeconds;
        Player.enabled = false;
        OnGameFinished(Score);
    }

    private readonly Transform playerInitialPosition;
    private readonly MonoBehaviour mainObject;
    private readonly Target[] targets;
    private readonly WaitForSeconds WaitSeconds = new WaitForSeconds(1);
}
