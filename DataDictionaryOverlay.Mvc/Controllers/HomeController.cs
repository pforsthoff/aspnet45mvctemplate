using DataDictionary.ServiceModel.Entities;
using DataDictionaryOverlay.Controls.Datatables;
using DataDictionaryOverlay.Models;
using ServiceModel.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace DataDictionaryOverlay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMetadataManager _metadataManager;
        private readonly IMappingEngine _mappings;
        public HomeController(IMetadataManager metadataManager, IMappingEngine mappings)
        {
            _metadataManager = metadataManager;
            _mappings = mappings;
        }
        public ActionResult Index()
        {
            //ViewBag.Message = _metadataManager.GetSingleString();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public JsonResult AjaxMetadata([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestParameters)
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

            // Skip number of Rows count  
            var start = Request.Form["start"].FirstOrDefault();

            // Paging Length 10,20  
            var length = Request.Form["length"].FirstOrDefault();

            // Sort Column Name  
            var sortColumn = Request.Form["order[0][column]"].FirstOrDefault().ToString();

            // Sort Column Direction (asc, desc)  
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault().ToString();

            //Paging Size (10, 20, 50,100)  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int skip = start != null ? Convert.ToInt32(start) : 0;

            int recordsTotal = 0;
            IEnumerable<Metadata> metadataItems = _metadataManager.GetAllMetadataItems();

            //Search    
            if (! string.IsNullOrEmpty(requestParameters.Search.Value))
            {
                var searchParam = requestParameters.Search.Value;
                metadataItems =
                    metadataItems.Where(
                        b =>
                            b.Id.ToString().Contains(searchParam) ||
                            b.TableName.ToLower().Contains(searchParam.ToLower()) ||
                            b.Column.ToString().ToLower().Contains(searchParam.ToLower()) ||
                            b.Entity.ToString().ToLower().Contains(searchParam.ToLower()) ||
                            b.RecordingRate.Contains(searchParam.ToLower()));
            }
            //Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && sortColumn != "0" && sortColumnDirection != "a" && string.IsNullOrEmpty(sortColumnDirection)))
            {
                metadataItems = SortByColumnWithOrder(sortColumn, sortColumnDirection, metadataItems);
            }
            //total number of rows counts   
            recordsTotal = metadataItems.Count();
            //Paging   
            var mappedResults = _mappings.Map<IEnumerable<Metadata>, ICollection<MetadataViewModel>>(metadataItems);

            //var data = lootItems.Skip(skip).Take(pageSize).ToList();
            //var data = mappedResults;

            //Returning Json Data  

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = mappedResults });
        }
        private IEnumerable<Metadata> SortByColumnWithOrder(string sortColumn, string sortColumnDir, IEnumerable<Metadata> data)
        {
            // Sorting   
            //switch (sortColumn)
            //{
            //    case "0":
            //        data = sortColumnDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Order).ToList() : data.OrderBy(p => p.Order).ToList();
            //        break;
            //    case "1":
            //        data = sortColumnDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList() : data.OrderBy(p => p.Name).ToList();
            //        break;
            //    case "2":
            //        data = sortColumnDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Quantity.ToString()).ToList() : data.OrderBy(p => p.Quantity.ToString()).ToList();
            //        break;
            //    default:
            //        data = sortColumnDir.Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Order).ToList() : data.OrderBy(p => p.Order).ToList();
            //        break;
            //}
            return data.AsEnumerable();
        }
    }

}