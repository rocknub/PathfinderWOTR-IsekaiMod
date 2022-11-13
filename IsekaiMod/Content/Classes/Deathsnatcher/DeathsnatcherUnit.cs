﻿using IsekaiMod.Content.Features.IsekaiProtagonist;
using IsekaiMod.Extensions;
using IsekaiMod.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using UnityEngine;

namespace IsekaiMod.Content.Classes.Deathsnatcher
{
    internal class DeathsnatcherUnit
    {
        // Deathsnatcher Unit Details
        private static readonly BlueprintFeature AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
        private static readonly BlueprintFaction Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
        private static readonly BlueprintFeature MonstrousHumanoidType = Resources.GetBlueprint<BlueprintFeature>("57614b50e8d86b24395931fffc5e409b");
        private static readonly BlueprintFeature RightAndDoubleHandLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("b7b0360f2e384e55a6c4505242c843b6");
        private static readonly BlueprintBrain DeathsnatcherBrain = Resources.GetBlueprint<BlueprintBrain>("39efaf3b8a52dd14f972c6d706249ccf");
        private static readonly BlueprintUnitAsksList Deathsnatcher_Barks = Resources.GetBlueprint<BlueprintUnitAsksList>("ec6a8faba9332024599becceb1da8a54");

        // Deathsnatcher Weapons
        private static readonly BlueprintItemWeapon Bite2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("2abc1dc6172759c42971bd04b8c115cb");
        private static readonly BlueprintItemWeapon Claw1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("65eb73689b94d894080d33a768cdf645");
        private static readonly BlueprintItemWeapon Sting1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("df44800dbe7b4ba43ac6e0e435041ed8");

        // Unit Facts
        private static readonly BlueprintFeature NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");

        public static void Add()
        {
            var DeathsnatcherSlotFeature = Helpers.CreateBlueprint<BlueprintFeature>("DeathsnatcherSlotFeature", bp => {
                bp.SetName("Feature not available");
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.MainHand;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.OffHand;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Boots;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon1;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon2;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon3;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon4;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon5;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon6;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon7;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Weapon8;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Ring2;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Glasses;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Shirt;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Gloves;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Armor;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Headgear;
                });
                bp.AddComponent<LockEquipmentSlot>(c => {
                    c.m_SlotType = LockEquipmentSlot.SlotType.Cloak;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
            });
            var DeathsnatcherPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("DeathsnatcherPortrait", bp => {
                bp.Data = new PortraitData()
                {
                    PortraitCategory = PortraitCategory.None,
                    IsDefault = false,
                    InitiativePortrait = false
                };

            });
            var DeathsnatcherFact = Helpers.CreateBlueprint<BlueprintFeature>("DeathsnatcherFact", bp => {
                bp.SetName("DeathsnatcherFact");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DeathsnatcherUnit = Helpers.CreateBlueprint<BlueprintUnit>("DeathsnatcherUnit", bp => {
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = DeathsnatcherClass.GetReference();
                    c.RaceStat = StatType.Strength;
                    c.LevelsStat = StatType.Strength;
                    c.Skills = new StatType[0];
                    c.DoNotApplyAutomatically = true;
                    c.m_MemorizeSpells = new BlueprintAbilityReference[0];
                    c.m_SelectSpells = new BlueprintAbilityReference[0];
                });
                bp.AddComponent<AddFacts>(c => { c.m_Facts = new BlueprintUnitFactReference[] { RightAndDoubleHandLocatorFeature.ToReference<BlueprintUnitFactReference>() }; });
                bp.AddComponent<AllowDyingCondition>();
                bp.AddComponent<AddResurrectOnRest>();
                bp.Gender = Gender.Female;
                bp.Size = Size.Medium;
                bp.Color = new Color(0.15f, 0.15f, 0.15f, 1.0f);
                bp.Alignment = Alignment.ChaoticEvil;
                bp.m_Portrait = DeathsnatcherPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink() { AssetId = "261c55913d512ad4aac907f43915183c" };
                bp.Visual = new UnitVisualParams()
                {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = Deathsnatcher_Barks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null,
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Neutrals.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = new FactionOverrides() { m_AttackFactionsToAdd = new BlueprintFactionReference[0], m_AttackFactionsToRemove = new BlueprintFactionReference[0] };
                bp.m_Brain = DeathsnatcherBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody()
                {
                    DisableHands = true,
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[]
                    {
                        Bite2d6.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d6.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d6.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d6.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d6.ToReference<BlueprintItemWeaponReference>(),
                        Sting1d4.ToReference<BlueprintItemWeaponReference>(),
                    }
                };
                //Stats set for Tiny size
                bp.Strength = 27;
                bp.Dexterity = 25;
                bp.Constitution = 36;
                bp.Wisdom = 20;
                bp.Intelligence = 25;
                bp.Charisma = 28;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills()
                {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AdditionalTemplates = new BlueprintUnitTemplateReference[0];
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    DeathsnatcherSlotFeature.ToReference<BlueprintUnitFactReference>(),
                    MonstrousHumanoidType.ToReference<BlueprintUnitFactReference>(),
                    DeathsnatcherFact.ToReference<BlueprintUnitFactReference>(),
                    NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()
                };
                bp.IsCheater = false;
                bp.IsFake = false;
            });

            FullPortraitInjector.Replacements[DeathsnatcherUnit.PortraitSafe.Data] = AssetLoader.LoadInternal("Portraits", "DeathsnatcherFull.png", new Vector2Int(692, 1024), TextureFormat.RGBA32);
            HalfPortraitInjector.Replacements[DeathsnatcherUnit.PortraitSafe.Data] = AssetLoader.LoadInternal("Portraits", "DeathsnatcherMedium.png", new Vector2Int(330, 432), TextureFormat.RGBA32);
            SmallPortraitInjector.Replacements[DeathsnatcherUnit.PortraitSafe.Data] = AssetLoader.LoadInternal("Portraits", "DeathsnatcherSmall.png", new Vector2Int(185, 242), TextureFormat.RGBA32);
            EyePortraitInjector.Replacements[DeathsnatcherUnit.PortraitSafe.Data] = AssetLoader.LoadInternal("Portraits", "DeathsnatcherPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var DeathsnatcherFeature = Helpers.CreateBlueprint<BlueprintFeature>("DeathsnatcherFeature", bp => {
                bp.SetName("Deathsnatcher");
                bp.SetDescription("Deathsnatchers dwell amid the ruins of fallen civilizations, where they play at being godlings worshiped by undead slaves. "
                    + "Though self-aggrandizing, deathsnatchers are known to give homage to (and claim descent from) the various demon lords of darkness, the desert, and undeath.");
                bp.m_Icon = null;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = DeathsnatcherUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => {
                    c.NoCompanion = true;
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DeathsnatcherClassProgression.GetCompanionProgression().ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });

            // Add Deathsnatcher to Pet Selection
            IsekaiPetSelection.AddToSelection(DeathsnatcherFeature);
        }
    }
}
