using UnityEngine;

[CreateAssetMenu(fileName ="WeaponSettings", menuName ="Game/Weapon Settings")]
public class WeaponSettings : ScriptableObject
{
    public int ClipSize;
    public WeaponModel ModelPrefab;
    public ProjectileBase ProjectilePrefab;
    public AudioClip ShootSound;
    public AudioClip EmptyClipSound;
}
