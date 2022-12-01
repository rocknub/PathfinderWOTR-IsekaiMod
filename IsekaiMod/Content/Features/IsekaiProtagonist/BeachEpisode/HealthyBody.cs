﻿using IsekaiMod.Extensions;
using IsekaiMod.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using UnityEngine;

namespace IsekaiMod.Content.Features.IsekaiProtagonist.BeachEpisode
{
    class HealthyBody
    {
        private static readonly Sprite Icon_PurityOfBody = Resources.GetBlueprint<BlueprintFeature>("9b02f77c96d6bba4daf9043eff876c76").m_Icon;
        public static void Add()
        {
            var HealthyBody = Helpers.CreateFeature("HealthyBody", bp => {
                bp.SetName("Healthy Body");
                bp.SetDescription("You gain immunity to bleed, blindness, curses, poison, disease, sickened, and nauseated conditions.");
                bp.m_Icon = Icon_PurityOfBody;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Sickened;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Nauseated;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Blindness;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Sickened
                    | SpellDescriptor.Nauseated
                    | SpellDescriptor.Bleed
                    | SpellDescriptor.Blindness
                    | SpellDescriptor.Curse
                    | SpellDescriptor.Disease
                    | SpellDescriptor.Poison;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Sickened
                    | SpellDescriptor.Nauseated
                    | SpellDescriptor.Bleed
                    | SpellDescriptor.Blindness
                    | SpellDescriptor.Curse
                    | SpellDescriptor.Disease
                    | SpellDescriptor.Poison;
                });
            });

            BeachEpisodeSelection.AddToSelection(HealthyBody);
        }
    }
}
