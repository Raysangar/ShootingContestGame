using UnityEngine;

public class Weapon : MonoBehaviour
{
    public System.Action<Weapon> OnShoot;

    public int ProjectilesLeft { get; private set; }

    public void Init(WeaponSettings[] settings)
    {
        this.settings = settings;
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
            Instantiate(CurrentSettings.ProjectilePrefab, CurrentModel.ProjectileInitialPosition.position,
                CurrentModel.ProjectileInitialPosition.rotation);
            AudioSource.PlayClipAtPoint(CurrentSettings.ShootSound, transform.position);
            OnShoot(this);
        }
        else
        {
            AudioSource.PlayClipAtPoint(CurrentSettings.EmptyClipSound, transform.position);
        }
    }

    private WeaponSettings CurrentSettings => settings[currentSettingsIndex];
    private WeaponModel CurrentModel => models[currentSettingsIndex];

    [SerializeField] Transform visualParent;

    private int currentSettingsIndex;
    private WeaponSettings[] settings;
    private WeaponModel[] models;
}
