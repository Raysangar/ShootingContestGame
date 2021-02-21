using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Awake()
    {
        player.Init(weaponSettings);
        gameManager = new GameManager(player, playerInitialPosition, this);
        uiManager.Init(gameManager, weaponSettings);
    }

    [SerializeField] UiManager uiManager;
    [SerializeField] PlayerController player;
    [SerializeField] Transform playerInitialPosition;
    [SerializeField] WeaponSettings[] weaponSettings;

    private GameManager gameManager;
}
