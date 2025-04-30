using UnityEngine;

public class EnemyHealth : Health
{
    public EnemyBehavior enemyBehavior;
    [Header("Has to have the EXACT same name as the loot table in the game manager")]
    public string lootTableName;

    public override void OtherHealthLogic()
    {
        if (healthbar != null)
            healthbar.UpdateHealth(currentHealth / maxHealth);
    }

    public override void OtherDamageLogic()
    {
        if (enemyBehavior.canInterrupt)
        {
            enemyBehavior.interrupted = true;
        }
    }
    public override void HandleDeath()
    {
        CallTable();

        GameManager.Instance.UIManager.UpdateScore(enemyBehavior.scoreOnDeath);
        gameObject.SetActive(false);
        currentHealth = maxHealth;
        healthbar.UpdateHealth(maxHealth);
    }

    public void CallTable()
    {
        GameObject loot = GameManager.Instance.lootTable.FindLootTable(lootTableName);
        if (loot == null)
            return;
        GameObject lootObj = Instantiate(loot);
        lootObj.transform.position = transform.position;
    }
}
