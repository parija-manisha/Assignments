using DemoUserManagement.Attributes;
using DemoUserManagement.Business;
using DemoUserManagement.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DemoUserManagement.Controllers
{
    public class UserList_v2Controller : Controller
    {
        // GET: UserList_v2
        [CustomAuthorize_v2]
        public ActionResult UserList_v2(int? page, string sortColumn, string sortDirection)
        {
            int currentPage = page ?? 1;

            sortColumn = string.IsNullOrEmpty(sortColumn) ? "Name" : sortColumn;
            sortDirection = ToggleSortDirection(sortDirection);

            List<UserDetailDTO> userDetailDTO = UserLogic.GetAllUsers(start: (currentPage - 1) * 10, length: 10, sortColumn: sortColumn, sortDirection: sortDirection);

            int totalPages = UserLogic.GetTotalRecords() / 10;
            foreach (var user in userDetailDTO)
            {
                user.CurrentPage = currentPage;
                user.TotalPages = totalPages;
            }

            ViewBag.SortColumn = sortColumn;
            ViewBag.SortDirection = sortDirection;

            return View(userDetailDTO);
        }

        private string ToggleSortDirection(string currentDirection)
        {
            return currentDirection?.ToUpper() == "ASC" ? "DESC" : "ASC";
        }
    }
}
