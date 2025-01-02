using System.Linq;
using System.Collections.Generic;

namespace SonJeremy.SModUtil.OxygenNotIncluded
{
    public static class SGameTags
    {
        public static Tag ToTag(this SimHashes Hash)
        {
            var IsFoundElement = ElementLoader.FindElementByHash(Hash);
            var IsFoundTag = IsFoundElement?.tag ?? TagManager.Create(Hash.ToString());

            return IsFoundTag;
        }
        
        public static List<Tag> GetAllGameTagsByFilterType(Filterable.ElementState FilterType)
        {
            List<Tag> AllTags = new List<Tag>();

            if (FilterType == Filterable.ElementState.Solid)
                return DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(GameTags.Solid).ToList();

            foreach (Element MaterialElement in ElementLoader.elements)
            {
                if (MaterialElement.IsGas && FilterType == Filterable.ElementState.Gas)
                    AllTags.Add(MaterialElement.GetMaterialCategoryTag());

                if (MaterialElement.IsLiquid && FilterType == Filterable.ElementState.Liquid)
                    AllTags.Add(MaterialElement.GetMaterialCategoryTag());
            }

            return AllTags;
        }
    }
}