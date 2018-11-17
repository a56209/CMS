using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSZ.IService;
using ZSZ.AdminWeb.App_Start;
using ZSZ.AdminWeb.Models;
using ZSZ.CommonMVC;
using ZSZ.DTO;

namespace ZSZ.AdminWeb.Controllers
{
    public class HouseController:Controller
    {
        public IAdminUserService userService { get; set; }
        public IHouseService houseService { get; set; }
        public ICityService cityService { get; set; }
        public IRegionService regionService { get; set; }
        public IIdNameService idNameService { get; set; }
        public IAttachmentService attService { get; set; }
        public ICommunityService communityService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId">房源类型（整租、合租）</param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(long typeId, int pageIndex = 1)
        {
            long userId = AdminHelper.GetUserId(HttpContext).Value;
            long? cityId = userService.GetById(userId).CityId;
            if(cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }
            var houses = houseService.GetPagedData(cityId.Value, typeId, 10, (pageIndex - 1) * 10);
            long totalCount = houseService.GetTotalCount(cityId.Value, typeId);
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = totalCount;
            ViewBag.typeId = typeId;
            return View(houses);
        }

        public ActionResult LoadCommunities(long regionId)
        {
            var communities = communityService.GetByRegionId(regionId);
            return Json(new AjaxResult { Status = "ok", Data = communities });
        }

        [CheckPermission("House.Add")]
        [HttpGet]
        public ActionResult Add()
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }
            var regions = regionService.GetAll(cityId.Value);
            var roomTypes = idNameService.GetAll("户型");
            var statuses = idNameService.GetAll("房屋状态");
            var decorateStatuses = idNameService.GetAll("装修状态");
            var attachments = attService.GetAll();
            var types = idNameService.GetAll("房屋类别");

            HouseAddViewModel model = new HouseAddViewModel();
            model.regions = regions;
            model.roomTypes = roomTypes;
            model.statuses = statuses;
            model.decorateStatuses = decorateStatuses;
            model.attachments = attachments;
            model.types = types;
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Add(HouseAddModel model)
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }

            HouseAddNewDTO dto = new HouseAddNewDTO();
            dto.Address = model.address;
            dto.Area = model.area;
            dto.AttachmentIds = model.attachmentIds;
            dto.CheckInDateTime = model.checkInDateTime;
            dto.CommunityId = model.CommunityId;
            dto.DecorateStatusId = model.DecorateStatusId;
            dto.Description = model.description;
            dto.Direction = model.direction;
            dto.FloorIndex = model.floorIndex;
            dto.LookableDateTime = model.lookableDateTime;
            dto.MonthRent = model.monthRent;
            dto.OwnerName = model.ownerName;
            dto.OwnerPhoneNum = model.ownerPhoneNum;
            dto.RoomTypeId = model.RoomTypeId;
            dto.StatusId = model.StatusId;
            dto.TotalFloorCount = model.totalFloor;
            dto.TypeId = model.TypeId;

            houseService.AddNew(dto);
            return Json(new AjaxResult { Status="ok"});
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            long? userId = AdminHelper.GetUserId(HttpContext);
            long? cityId = userService.GetById(userId.Value).CityId;
            if (cityId == null)
            {
                return View("Error", (object)"总部不能进行房源管理");
            }
            var house = houseService.GetById(id);
            HouseEditViewModel model = new HouseEditViewModel();
            model.house = house;

            var regions = regionService.GetAll(cityId.Value);
            var roomTypes = idNameService.GetAll("户型");
            var statuses = idNameService.GetAll("房屋状态");
            var decorateStatuses = idNameService.GetAll("装修状态");
            var attachments = attService.GetAll();
            var types = idNameService.GetAll("房屋类别");

            model.regions = regions;
            model.roomTypes = roomTypes;
            model.statuses = statuses;
            model.decorateStatuses = decorateStatuses;
            model.attachments = attachments;
            model.types = types;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HouseEditModel model)
        {
            HouseDTO dto = new HouseDTO();
            dto.Address = model.address;
            dto.Area = model.area;
            dto.AttachmentIds = model.attachmentIds;
            dto.CheckInDateTime = model.checkInDateTime;
            //有没有感觉强硬用一些不适合的DTO，有一些没用的属性时候的迷茫？
            dto.CommunityId = model.CommunityId;
            dto.DecorateStatusId = model.DecorateStatusId;
            dto.Description = model.description;
            dto.Direction = model.direction;
            dto.FloorIndex = model.floorIndex;
            dto.Id = model.Id;
            dto.LookableDateTime = model.lookableDateTime;
            dto.MonthRent = model.monthRent;
            dto.OwnerName = model.ownerName;
            dto.OwnerPhoneNum = model.ownerPhoneNum;
            dto.RoomTypeId = model.RoomTypeId;
            dto.StatusId = model.StatusId;
            dto.TotalFloorCount = model.totalFloor;
            dto.TypeId = model.TypeId;
            houseService.Update(dto);

            return Json(new AjaxResult { Status = "ok" });
        }
    }
}