using Kingmaker;
using Kingmaker.Assets.UI.LevelUp;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Class.LevelUp;
using static Kingmaker.UnitLogic.Class.LevelUp.FeatureSelectionViewState;

namespace FavoriteFeats_Kingmaker.Patches
{
    [Harmony12.HarmonyPatch(typeof(CharBSelectorLayer), "CompareByPriorityNameAndPrerequisites")]
    static class CharBSelectorLayer_CompareByPriorityNameAndPrerequisites_Patch
    {
        static bool Prefix(IFeatureSelectionItem a, IFeatureSelectionItem b, ref int __result, CharBSelectorLayer __instance)
        {
            bool favoritedA = StateManager.IsFeatFavorited(Game.Instance.UI.CharacterBuildController.Unit.CharacterName, a.NameForAcronim);
            bool favoritedB = StateManager.IsFeatFavorited(Game.Instance.UI.CharacterBuildController.Unit.CharacterName, b.NameForAcronim);
            bool alreadyHasA = __instance.CurrentSelectionState.GetViewState(a).CanSelectState == SelectState.AlreadyHas;
            bool alreadyHasB = __instance.CurrentSelectionState.GetViewState(b).CanSelectState == SelectState.AlreadyHas;
            bool num = __instance.CurrentSelectionState.SelectedItem == a;
            bool flag = __instance.CurrentSelectionState.SelectedItem == b;
            bool flag2 = num || __instance.CurrentSelectionState.GetViewState(a).CanSelect;
            bool flag3 = flag || __instance.CurrentSelectionState.GetViewState(b).CanSelect;
            int num2 = ((a.Param == null) ? __instance.CurrentSelectionState.GetViewState(a).RecomendationPriority : 0);
            int num3 = ((b.Param == null) ? __instance.CurrentSelectionState.GetViewState(b).RecomendationPriority : 0);

            if (favoritedA && !favoritedB && !alreadyHasA)
            {
                __result = -1;
                goto quit_method;
            }
            if (favoritedB && !favoritedA && !alreadyHasB)
            {
                __result = 1;
                goto quit_method;
            }

            if (flag2 && flag3)
            {
                if (num2 != num3)
                {
                    __result = num3.CompareTo(num2);
                    goto quit_method;

                }
            }
            else
            {
                if (flag2 || flag3)
                {
                    if (!flag3)
                    {
                        __result = -1;
                        goto quit_method;

                    }
                    __result = 1;
                    goto quit_method;

                }
                bool flag4 = __instance.CurrentSelectionState.GetViewState(a).CanSelectState == FeatureSelectionViewState.SelectState.AlreadyHas;
                bool flag5 = __instance.CurrentSelectionState.GetViewState(b).CanSelectState == FeatureSelectionViewState.SelectState.AlreadyHas;
                if (flag4 != flag5)
                {
                    __result = flag5.CompareTo(flag4);
                    goto quit_method;

                }
            }
            __result = string.Compare(a.Name, b.Name);
            goto quit_method;

        quit_method:
            return false;
        }
    }
}
