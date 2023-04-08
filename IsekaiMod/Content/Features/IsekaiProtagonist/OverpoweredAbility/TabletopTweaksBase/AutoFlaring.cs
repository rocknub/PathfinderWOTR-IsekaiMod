﻿using IsekaiMod.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using TabletopTweaks.Core.Utilities;
using UnityEngine;
using static IsekaiMod.Main;
using static TabletopTweaks.Core.MechanicsChanges.MetamagicExtention;

namespace IsekaiMod.Content.Features.IsekaiProtagonist.OverpoweredAbility.TabletopTweaksBase {

    internal class AutoFlaring {
        private const string Name = "Overpowered Ability — Auto Flaring";
        private const string Description = "Every time you cast a light, fire, or electricity spell, it causes the dazzling condition, as though using the Flaring Spell feat.";

        private static readonly Sprite Icon_FlaringSpell = BlueprintTools.GetBlueprint<BlueprintFeature>("59e1695070ef481aa958d69cb370592b").m_Icon;

        public static void Add() {
            var AutoFlaringBuff = TTCoreExtensions.CreateBuff("AutoFlaringBuff", bp => {
                bp.SetName(IsekaiContext, Name);
                bp.SetDescription(IsekaiContext, Description);
                bp.m_Icon = Icon_FlaringSpell;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = (Metamagic)CustomMetamagic.Flaring;
                });
                bp.Stacking = StackingType.Replace;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
            });
            var AutoFlaringAbility = TTCoreExtensions.CreateActivatableAbility("AutoFlaringAbility", bp => {
                bp.SetName(IsekaiContext, Name);
                bp.SetDescription(IsekaiContext, Description);
                bp.m_Icon = Icon_FlaringSpell;
                bp.m_Buff = AutoFlaringBuff.ToReference<BlueprintBuffReference>();
            });
            var AutoFlaringFeature = Helpers.CreateBlueprint<BlueprintFeature>(IsekaiContext, "AutoFlaringFeature", bp => {
                bp.SetName(IsekaiContext, Name);
                bp.SetDescription(IsekaiContext, Description);
                bp.m_Icon = Icon_FlaringSpell;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AutoFlaringAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });

            OverpoweredAbilitySelection.AddToAllSelection(AutoFlaringFeature);
        }
    }
}