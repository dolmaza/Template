using Core.Entities;
using DevExpress.Web.Mvc;
using Service.IServices;
using Service.Properties;
using Service.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using Template.Admin.Models;
using Template.Admin.Reusable;

namespace Template.Areas.Admin.Controllers
{
    public class DictionariesController : BaseController
    {
        private static IDictionaryService _dictionaryService;

        public DictionariesController()
        {
            _dictionaryService = new DictionaryService();
        }

        [Route("dictionaries", Name = "Dictionaries")]
        public ActionResult Index()
        {
            var model = new DictionariesViewModel
            {
                TreeViewModel = GetTreeViewModel()
            };
            return View(model);
        }

        [Route("dictionaries/tree", Name = "DictioanriesTree")]
        public ActionResult DictionaryTree()
        {
            return PartialView("_DictionariesTree", GetTreeViewModel());
        }

        [Route("dictionaries/add", Name = "DictionariesAdd")]
        public ActionResult DictioanriesAdd([ModelBinder(typeof(DevExpressEditorsBinder))] DictionariesViewModel.DictionariesTreeViewModel.DictionaryTreeItem model)
        {
            _dictionaryService.Add(new Dictionary
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                CaptionEng = model.CaptionEng,
                StringCode = model.StringCode,
                IntCode = model.IntCode,
                DecimalValue = model.DecimalValue,
                Code = model.Code,
                SortIndex = model.SortIndex
            });

            if (_dictionaryService.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionariesTree", GetTreeViewModel());
        }

        [Route("dictionaries/update", Name = "DictionariesUpdate")]
        public ActionResult DictioanriesUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] DictionariesViewModel.DictionariesTreeViewModel.DictionaryTreeItem model)
        {
            var dictionary = _dictionaryService.GetByID(model.ID);

            if (dictionary == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                dictionary.ParentID = model.ParentID;
                dictionary.Caption = model.Caption;
                dictionary.CaptionEng = model.CaptionEng;
                dictionary.StringCode = model.StringCode;
                dictionary.IntCode = model.IntCode;
                dictionary.DecimalValue = model.DecimalValue;
                dictionary.Code = model.Code;
                dictionary.SortIndex = model.SortIndex;

                _dictionaryService.Update(dictionary);

                if (_dictionaryService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }
            }

            return PartialView("_DictionariesTree", GetTreeViewModel());
        }

        [Route("dictionaries/delete", Name = "DictionariesDelete")]
        public ActionResult DictionarisDelete(int? ID)
        {
            var dictionary = _dictionaryService.GetByID(ID);

            if (dictionary == null)
            {
                throw new Exception(Resources.Abort);
            }
            else
            {
                _dictionaryService.Remove(dictionary);

                if (_dictionaryService.IsError)
                {
                    throw new Exception(Resources.Abort);
                }

            }

            return PartialView("_DictionariesTree", GetTreeViewModel());
        }

        private DictionariesViewModel.DictionariesTreeViewModel GetTreeViewModel()
        {
            return new DictionariesViewModel.DictionariesTreeViewModel
            {
                ListUrl = Url.RouteUrl("DictioanriesTree"),
                AddNewUrl = Url.RouteUrl("DictionariesAdd"),
                UpdateUrl = Url.RouteUrl("DictionariesUpdate"),
                DeleteUrl = Url.RouteUrl("DictionariesDelete"),
                TreeItems = _dictionaryService.GetAllTreeItems().Select(d => new DictionariesViewModel.DictionariesTreeViewModel.DictionaryTreeItem
                {
                    ID = d.ID,
                    ParentID = d.ParentID,
                    Caption = d.Caption,
                    CaptionEng = d.CaptionEng,
                    StringCode = d.StringCode,
                    IntCode = d.IntCode,
                    DecimalValue = d.DecimalValue,
                    Code = d.Code,
                    SortIndex = d.SortIndex
                }).ToList()
            };
        }
    }
}