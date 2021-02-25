using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public System.Action<Weapon> OnShoot;
    public System.Action OnOutOfProjectiles;

    public int ProjectilesLeft { get; private set; }

    public void Init(WeaponSettings[] settings)
    {
        this.settings = settings;
        aliveProjectiles = new List<ProjectileBase>();
        SetupModels();
    }

    public void StartGame(int settingsIndex)
    {
        currentSettingsIndex = settingsIndex;
        ProjectilesLeft = CurrentSettings.ClipSize;
        ShowCurrentModel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void SetupModels()
    {
        models = new WeaponModel[settings.Length];
        for (int i = 0; i < settings.Length; ++i)
        {
            models[i] = Instantiate(settings[i].ModelPrefab, visualParent.position, visualParent.rotation, visualParent);
            models[i].gameObject.SetActive(false);
        }
    }

    private void ShowCurrentModel()
    {
        for (int i = 0; i < settings.Length; ++i)
            models[i].gameObject.SetActive(i == currentSettingsIndex);
    }

    private void Shoot()
    {
        if (ProjectilesLeft > 0)
        {
            --ProjectilesLeft;
            var projectile = Instantiate(CurrentSettings.ProjectilePrefab, CurrentModel.ProjectileInitialPosition.position,
                CurrentModel.ProjectileInitialPosition.rotation);
            projectile.OnDisappear += OnProjectileDisappeared;
            aliveProjectiles.Add(projectile);
            AudioSource.PlayClipAtPoint(CurrentSettings.ShootSound, transform.position);
            OnShoot(this);
        }
        else
        {
            AudioSource.PlayClipAtPoint(CurrentSettings.EmptyClipSound, transform.position);
        }
    }

    private void OnProjectileDisappeared(ProjectileBase projectile)
    {
        aliveProjectiles.Remove(projectile);
        if (aliveProjectiles.Count == 0 && ProjectilesLeft == 0)
            OnOutOfProjectiles();
    }

    private WeaponSettings CurrentSettings => settings[currentSettingsIndex];
    private WeaponModel CurrentModel => models[currentSettingsIndex];

    [SerializeField] Transform visualParent;

    private int currentSettingsIndex;
    private List<ProjectileBase> aliveProjectiles;
    private WeaponSettings[] settings;
    private WeaponModel[] models;
}
