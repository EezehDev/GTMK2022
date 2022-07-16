public class MeleeWeapon : Weapon
{
    private void Awake()
    {
        _isMelee = true;
    }
    public override void DoAttack()
    {
        // nothing
    }
}
