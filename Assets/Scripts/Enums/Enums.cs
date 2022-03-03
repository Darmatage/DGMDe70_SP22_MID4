namespace Game.Enums
{
    /// <summary>
    /// Scene Enums
    /// </summary>

    public enum SceneName
    {
        Scene_Main,
        Scene_Dialogue
    }

    public enum GameScenes
    {
        Scene_Main,
        Scene_Dialogue,
        Scene_Credits,
        Scene_Gameover,
        Scene_01,
        Scene_02,
        Scene_03
    }

    public enum GameStages
    {
        Stage_01,
        Stage_02,
        Stage_03
    }

    /// <summary>
    /// Input Action Enums<br/>
    /// </summary>
    public enum ActionTypes
    {
        Attack,
        Interact
    }
    
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
        BaseDamage,
        BaseDefence,
        ExperienceToLevelUp
    }

    public enum PlayerTransformState
    {
        Human,
        Monster
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
        BaseDamage,
        ExperienceReward
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
        Weapon,
        Shield
    }

    public enum SoulType
    {
        None,
        Red,
        Green,
        Blue
    }

}