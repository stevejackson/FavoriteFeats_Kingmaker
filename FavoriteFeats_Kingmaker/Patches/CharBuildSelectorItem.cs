using Kingmaker;
using Kingmaker.Assets.UI.LevelUp;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Class.LevelUp;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FavoriteFeats_Kingmaker.Patches
{
    [Harmony12.HarmonyPatch(typeof(CharBuildSelectorItem),
        "Init",
        typeof(FeatureSelectionState),
        typeof(IFeatureSelectionItem),
        typeof(CharBSelectorLayer),
        typeof(UnityAction))]
    static class CharBuildSelectorItem_Init_Patch
    {
        static void Postfix(FeatureSelectionState featureSelection,
            IFeatureSelectionItem feature,
            CharBSelectorLayer selector,
            UnityAction action,
            ref GameObject ___m_ItemLevelContainer,
            ref TextMeshProUGUI ___m_ItemLevel)
        {
            string characterName = Game.Instance.UI.CharacterBuildController.Unit.CharacterName;
            bool IsFeatFavorited = StateManager.IsFeatFavorited(characterName, feature.NameForAcronim);

            if (IsFeatFavorited)
            {
                ___m_ItemLevel.text = "";
                ___m_ItemLevelContainer.gameObject.SetActive(true);
                ___m_ItemLevelContainer.gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
            }
        }
    }
}

