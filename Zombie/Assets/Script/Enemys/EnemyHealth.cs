using UnityEngine;

public class EnemyHealth : Health
{
    [Header("Has to have the EXACT same name as the loot table in the game manager")]
    public string lootTableName;
    public override void OtherDamageLogic()
    {
    }
    public override void HandleDeath()
    {
        CallTable();
        gameObject.SetActive(false);
        currentHealth = maxHealth;
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
