using UnityEngine;

[CreateAssetMenu(menuName = "Data/TowerData")]
public class TowerData : ScriptableObject
{
    [SerializeField] private BasicTower basicTower;
    [SerializeField] private TowerProjectile towerProjectile;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Sprite icon;
    [SerializeField] private string nameTower;

    public BasicTower BasicTower => basicTower;
    public TowerProjectile TowerProjectile => towerProjectile;
    public float AttackRange => attackRange;
    public float Damage => damage;
    public float AttackCoolDown => attackCoolDown;
    public float ProjectileSpeed => projectileSpeed;
    public Sprite Icon => icon;
    public string NameTower => nameTower;
}