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
        EvilWizard
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
        ExperienceToLevelUp
    }

    public enum PlayerTransformState
    {
        Human,
        Monster
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
        SoulBonus,
        SoulHealBonus,
        DamageHealth
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
        Blob,
        Bat,
        Wolf
    }
    public enum FriendlyType
    {
        None,
        Pig,
        Sprout
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

    public enum ArmorMaterial
    {
        None,
        Cloth,
        Leather,
        Iron,
        Steel,
        LlamaFiber
    }
    public enum WeaponMaterial
    {
        None,
        Wood,
        Iron,
        Steel,
        Silver
    }

    public enum SoulType
    {
        None,
        Red,
        Green,
        Blue
    }

}