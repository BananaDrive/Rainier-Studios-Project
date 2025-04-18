using UnityEngine;

public class EnemyHealth : Health
{
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
        GameObject loot = GameManager.Instance.lootTable.FindLootTable(GetComponent<EnemyBehavior>().lootTableName);
        if (loot == null)
            return;
        GameObject lootObj = Instantiate(loot);
        lootObj.transform.position = transform.position;
    }
}
