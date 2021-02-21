using UnityEngine;
using UnityEngine.UI;

public class WeaponPresenter : MonoBehaviour
{
    public void Init(WeaponSettings weaponSettings, System.Action weaponSelectedCallback)
    {
        this.weaponSelectedCallback = weaponSelectedCallback;
        clipSize.text = "Clip Size\n" + weaponSettings.ClipSize;
        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void OnSelectButtonClicked()
    {
        weaponSelectedCallback();
    }

    [SerializeField] Image weaponView;
    [SerializeField] TMPro.TMP_Text clipSize;
    [SerializeField] Button selectButton;

    System.Action weaponSelectedCallback;
}
