namespace Game.Enums
{
    /// <summary>
    /// Player Class Enums<br/>
    /// What is the players class.
    /// </summary>
    public enum CharacterClasses
    {
        Footman,
        Knight,
        MageApprentice,
        MageBlue,
        MageRed,
        MageWhite,
        Rogue,
        Sorcerer,
        Templar,
        Thief
    }

    /// <summary>
    /// Player Stats Enums
    /// </summary>
    public enum PlayerStats
    {
        None,
        Health,
        Mana,
        ManaRegenRate,
        Strength,
        Dexterity,
        Intelligence,
        BaseDamage,
        Defence
    }

    /// <summary>
    /// Enemy Enums<br/>
    /// Info about the enemy.
    /// </summary>
    public enum EnemyType
    {
        None,
        Blob,
        Bat,
        Wolf
    }
    public enum EnemyBaseStat
    {
        Health,
        BaseDamage
    }

    /// <summary>
    /// Weapon Attack Type Enums<br/>
    /// What type of weapon attack is this.
    /// </summary>
    public enum WeaponAttackType
    {
        None,
        Melee,
        Range
    }

    /// <summary>
    /// Inventory Enums<br/>
    /// Locations on the players body where items can be equipped.
    /// </summary>
    public enum EquipLocation
    {
        None,
        Head,
        Body,
        Gloves,
        Legs,
        Boots,
        Hand_Right,
        Hand_Left
    }

}