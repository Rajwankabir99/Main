using Main.Common.Enums;
using Main.Common.Helper;
using Main.Common.HelperServices;
using Main.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FineArtsWebApp
{
    public class ViewComponenetDropDownDataList
    {
        public ViewComponenetDropDownDataList()
        {
        }

        private static IEnumerable<SelectListItem> GetAValueSelectList(
            List<ParentChildVriableModel> listAValue, 
            string selectText)
        {
            List<SelectListItem> objList = 
                new List<SelectListItem>() 
            { 
                new SelectListItem() 
                { 
                    Text = selectText, 
                    Value = null, 
                    Selected = true 
                } 
            };

            listAValue.ForEach(a =>
            {
                SelectListItem objItem = new SelectListItem
                {
                    Text = a.Text.Trim(),
                    Value = a.ValueID.ToString().Trim()
                };

                objList.Add(objItem);
            });

            return objList.AsEnumerable();
        }

        [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
        public static IEnumerable<SelectListItem> GetCategoryList(EnumCategoryFor categoryFor)
        {
            if(categoryFor == EnumCategoryFor.LifeStyles)
            {
                return GetAValueSelectList(
                    BusinessSeedLifeStyle.GetListByVariable(EnumAllowedVariable.Category, EnumCategoryFor.LifeStyles),"").ToList().AsEnumerable();
            }
            else if (categoryFor == EnumCategoryFor.FineArts)
            {
                return GetAValueSelectList (
                    BusinessSeedFineArts.GetListByVariable( EnumAllowedVariable.Category,EnumCategoryFor.LifeStyles ),"" ).ToList ( ).AsEnumerable ( );
            }

            return Enumerable.Empty<SelectListItem>();
        }

        [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
        public static IEnumerable<SelectListItem> GetSubCategoryList(EnumCategoryFor categoryFor)
        {
            if(categoryFor == EnumCategoryFor.LifeStyles)
            {
                return GetAValueSelectList (
                   BusinessSeedLifeStyle.GetListByVariable ( EnumAllowedVariable.SubCategory,
                   EnumCategoryFor.LifeStyles ),"" ).ToList ( ).AsEnumerable ( );
            }
            else if(categoryFor == EnumCategoryFor.FineArts)
            {
                return GetAValueSelectList (
                   BusinessSeedLifeStyle.GetListByVariable ( EnumAllowedVariable.SubCategory,
                   EnumCategoryFor.FineArts ),"" ).ToList ( ).AsEnumerable ( );
            }
            return Enumerable.Empty<SelectListItem>();
        }

        [ResponseCache(CacheProfileName = "Cache1dayServerNBrowser")]
        public static List<SelectListItem> GetAllStateList()
        {
            var listStates = ListEnum.GetCountryStates(EnumCountry.Bangladesh, false);
            List<SelectListItem> objList = new List<SelectListItem> { new SelectListItem() { Text = "", Value = "" } };
            listStates.ForEach(a =>
            {
                SelectListItem objItem = new SelectListItem();
                objItem.Text = a.Text.Trim();
                objItem.Value = a.ValueID.ToString().Trim();
                objList.Add(objItem);
            });
            return objList;
        }
    }
}
