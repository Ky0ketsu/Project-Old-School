using UnityEngine;
using System;

public class HealthScript : MonoBehaviour, iDamageable
{
    //GAMEPLAY VARIABLES - ADJUST IN INSPECTOR
    [Header("GAMEPLAY")]
    [Range(1f, 100f)][SerializeField] float hpMax = 1;
    [SerializeField] bool spawnOnStart = true;
    [SerializeField] bool destroyOnDeath = true;
    // DRAG AND DROP IN INSPECTOR
    [Header("OPTIONAL")]
    [SerializeField] BarHandler healthBar;
    [Header("EFFECTS")]
    [SerializeField] GameObject[] onSpawn;
    [SerializeField] GameObject[] onHit;
    [SerializeField] GameObject[] onHeal;
    [SerializeField] GameObject[] onDeath;
    // EVENTS FOR TAKING DAMAGE, HEALING, DEATH AND SPAWN. Please subscribe from outside to be notified
    public event Action OnDeath;
    public void InvokeDeath() { Debug.Log(gameObject.name + " Death 💀"); OnDeath?.Invoke(); }
    public event Action OnSpawn;
    public void InvokeSpawn() { Debug.Log(gameObject.name + " Spawn 🚼"); OnSpawn?.Invoke(); }
    public event Action<float> OnDamage;
    public void InvokeDamage(float amount) { Debug.Log(gameObject.name + " Damaged 🪓"); OnDamage?.Invoke(amount); }
    public event Action<float> OnHeal;
    public void InvokeHeal(float amount) { Debug.Log(gameObject.name + " Heal ⛑️"); OnHeal?.Invoke(amount); }

    // PUBLIC METHODS - Call them via the IDamageable interface
    public void TakeDamage(float amount)
    {
        if (Dead || Invulnerable || GAME.MANAGER.CurrentState != State.gameplay) return;
        if (amount < 0)
        {
            Heal(-amount);
            return;
        }
        amount = -amount;
        ApplyChange(amount, onHit);
        InvokeDamage(amount);
    }

    public void Heal(float amount)
    {
        if (Dead || Invulnerable || GAME.MANAGER.CurrentState != State.gameplay) return;
        if (amount < 0)
        {
            TakeDamage(-amount);
            return;
        }
        ApplyChange(amount, onHeal);
        InvokeHeal(amount);
    }
    public void Spawn()
    {
        dead = false;
        for (int i = 0; i < onSpawn.Length; i++) if (onSpawn[i]) Instantiate(onSpawn[i], transform);
        InvokeSpawn();
    }

    public void SpawnFullHealth()
    {
        FullHealth();
        Spawn();
    }


    public void FullHealth()
    {
        HP = hpMax;
    }

    public void ChangeMaxHP(int quantity, bool adjustCurrentHP)
    {
        if (quantity <= 0)
        {
            hpMax = hp = 0;
            Death();
        }
        else
        {
            if (adjustCurrentHP) { hp = Mathf.CeilToInt(hp * quantity / (float)hpMax); }
            hpMax = quantity;
        }
    }
        public void Death()
    {
        if (Dead) return;
        dead = true;
        for (int i = 0; i < onDeath.Length; i++) if (onDeath[i]) Instantiate(onDeath[i], transform);
        InvokeDeath();
        if (destroyOnDeath) Destroy(gameObject);
    }




    // INTERNAL VARIABLES - DO NOT TOUCH
    float hp;
    public float HP { get { return hp; } set { hp = value; if (hp < 1) Death(); } }
    public float HPMax { get { return hpMax; } }
    bool dead = true;
    public bool Dead { get { return dead; } }
    bool invulnerable = false;
    public bool Invulnerable { get { return invulnerable; } set { invulnerable = value; } }

    // INTERNAL METHODS - DO NOT TOUCH
    void Start()
    {
        if (spawnOnStart) SpawnFullHealth();
    }

    void ApplyChange(float amount, GameObject[] fx)
    {
        HP = Mathf.Clamp(HP + amount, 0, hpMax);
        if (Dead == false) for (int i = 0; i < fx.Length; i++) if (fx[i]) Instantiate(fx[i], transform);
        if (healthBar) healthBar.FillAmount = HP / HPMax;
    }

} // SCRIPT END