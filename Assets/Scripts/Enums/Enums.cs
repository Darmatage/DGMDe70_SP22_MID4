namespace Game.Enums
{
    /// <summary>
    /// Scene Enums
    /// </summary>

    public enum SceneName
    {
        Scene_Main,
        Scene_Dialogue,
        Scene_PortalTest
    }

    public enum DestinationIdentifier
    {
        A, B, C, D, E, F
    }
    public enum CutSceneDestinationIdentifier
    {
        Wizard,
        EvilWizard,
        Guard
    }

    public enum GameScenes
    {
        Scene_Main,
        Scene_Dialogue,
        Scene_Credits,
        Scene_Gameover,
        Scene_01,
        Scene_02,
        Scene_03,
        Scene_04,
        Scene_05,
        Scene_06,
        Scene_07,
        Scene_08,
        Scene_09
    }

    public enum GameStages
    {
        Stage_01,
        Stage_02,
        Stage_03,
        Stage_04
    }

    public enum DialogueVariant
    {
        DV_01,
        DV_02,
        DV_03
    }

    public enum CursorType
    {
        None,
        Melee,
        Range,
        UI
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
        ExperienceToLevelUp,
        MovementSpeed
    }

    public enum PlayerTransformState
    {
        Human,
        Monster,
        Either
    }

    public enum CurseTypes
    {
        None,
        Werewolf,
        Llama,
        LizardPerson
    }

    public enum CurseEffectTypes
    {
        None,
        DetailEffect,
        SoulBonus,
        SoulHealBonus,
        DamageHealth,
        EquipmentRestrictMaterial
    }

    public enum CurseEffectConditionType
    {
        None,
        Advantage,
        Disadvantage

    }

    /// <summary>
    /// Enemy Enums<br/>
    /// Info about the enemy.
    /// </summary>
    public enum AIMotiveState
    {
        None,
        Enemy,
        Friendly
    }
    public enum AIBaseStat
    {
        None,
        Health,
        BaseDamage,
        ExperienceReward
    }
    public enum EnemyType
    {
        None,
        Lizard,
        Wolf,
        WerePig
    }
    public enum FriendlyType
    {
        None,
        Pig
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
        Shield,
        Curse
    }

    public enum EquipmentMaterial
    {
        None,
        Cloth,
        wood,
        Leather,
        Iron,
        Steel,
        Silver,
        LlamaFiber
    }

    public enum WeaponAttackSpeed
    {
        None,
        Fast,
        Medium,
        Slow
    }

    public enum SoulType
    {
        None,
        Red,
        Green,
        Blue
    }

}