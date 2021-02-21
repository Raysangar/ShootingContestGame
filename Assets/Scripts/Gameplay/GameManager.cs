using System.Collections;
using UnityEngine;

public class GameManager
{
    public int Score { get; private set; }
    public PlayerController Player { get; private set; }

    public System.Action<int> OnGameFinished;
    public System.Action<int> OnScoreChanged;

    public GameManager(PlayerController player, Transform playerInitialPosition, MonoBehaviour mainObject)
    {
        Player = player;
        this.mainObject = mainObject;
        this.playerInitialPosition = playerInitialPosition;
        player.enabled = false;
        player.transform.position = playerInitialPosition.position;
        ProjectileBase.OnDisappear = OnProjectileDisappeared;
        Target.OnHit = OnTargetHit;
    }

    public void StartGame(int weaponIndex)
    {
        Player.transform.position = playerInitialPosition.position;
        Player.StartGame(weaponIndex);
        Score = 0;
        OnScoreChanged(Score);
    }

    private void OnProjectileDisappeared(ProjectileBase projectile)
    {
        if (Player.Weapon.ProjectilesLeft == 0)
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
        OnGameFinished(Score);
    }

    private readonly Transform playerInitialPosition;
    private readonly MonoBehaviour mainObject;

    private readonly WaitForSeconds WaitSeconds = new WaitForSeconds(2);
}
