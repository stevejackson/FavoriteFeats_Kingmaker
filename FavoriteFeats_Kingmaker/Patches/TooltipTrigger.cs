using Kingmaker;
using Kingmaker.Assets.UI.LevelUp;
using Kingmaker.UI;
using Kingmaker.UI.Tooltip;
using Kingmaker.Utility;
using UnityEngine.EventSystems;

namespace FavoriteFeats_Kingmaker.Patches
{
    [Harmony12.HarmonyPatch(typeof(TooltipTrigger), "OnPointerClick", typeof(PointerEventData))]
    static class TooltipTrigger_OnPointerClick_Patch
    {
        static void Postfix(PointerEventData eventData, object ___m_Obj)
        {
            if (eventData.button == PointerEventData.InputButton.Middle)
            {
                string characterName = Game.Instance.UI.CharacterBuildController.Unit.CharacterName;
                bool tooltipIsForAFeat = ___m_Obj is FeatureSelectionItem;

                if (tooltipIsForAFeat && !characterName.Empty())
                {
                    FeatureSelectionItem objAsFeature = ___m_Obj as FeatureSelectionItem;
                    StateManager.ToggleFavoriteStatus(characterName, objAsFeature.Feature.NameForAcronim);
                    Game.Instance.UI.Common.UISound.Play(UISoundType.CampCheckSucceeded);
                    // SettingsSwitchToggle
                }
            }
        }
    }

}
