using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Navigating to Edit or Update view
        public IActionResult Upsert (int? id)
        {
            #region ([Code logic wothout stored proc])
            //CoverType coverType = new CoverType();

            ////Add new Cover Type
            //if (id == null)
            //{
            //    return View(coverType);
            //}

            ////Edit existing Cover Type
            //coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            //if (coverType == null)
            //{
            //    return NotFound();
            //}
            //return View(coverType);
            #endregion

            CoverType coverType = new CoverType();
            //Add new Cover Type view
            if (id == null)
            {
                return View(coverType);
            }

            //Edit existing Cover Type view
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);

            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        //Saving the Edit or Update data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            #region ((Code logic wothout stored proc))
            //if (ModelState.IsValid)
            //{
            //    if (coverType.Id == 0)
            //    {
            //        _unitOfWork.CoverType.Add(coverType);
            //    }
            //    else
            //    {
            //        _unitOfWork.CoverType.Update(coverType);
            //    }
            //    _unitOfWork.Save();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(coverType);
            #endregion

            //Getting data using stored procedure

            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);

                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                }
                else
                {
                    parameter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region [API Calls]
        [HttpGet]
        public IActionResult GetAll()
        {
            #region (Code logic wothout stored proc)
            //Getting data from Unit of work
            //var allObj = _unitOfWork.CoverType.GetAll();
            //return Json(new { data = allObj });
            #endregion

            //Getting data using stored procedure
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //Using the stored procedure
            var parameter = new DynamicParameters();
            //passing @Id" same as stored procedure
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);

            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error in deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });

            #region [Code logic wothout stored proc]
            //var objFromDb = _unitOfWork.CoverType.Get(id);

            //if (objFromDb == null)
            //{
            //    return Json(new { success = false, message = "Error in deleting" });
            //}
            //_unitOfWork.CoverType.Remove(objFromDb);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Deleted Successfully" });
            #endregion
        }
        #endregion
    }
}