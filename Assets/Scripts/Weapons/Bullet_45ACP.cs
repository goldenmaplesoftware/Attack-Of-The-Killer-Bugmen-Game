using UnityEngine;

public class Bullet_45ACP : MonoBehaviour
{
    public Weapon_Pistol kimberCustom;
    [SerializeField] public int bulletInventoryToAdd;

    public void AddAmmo(int bulletInventoryToAdd)
    {
        kimberCustom.addAmmo();
    }

}
