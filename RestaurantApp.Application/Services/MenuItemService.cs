using Mapster;
using RestaurantApp.Application.Dtos;
using RestaurantApp.Application.Interfaces;
using RestaurantApp.Application.Results;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepo _menuItemRepo;
        private readonly IFileService _fileService;

        public MenuItemService(IMenuItemRepo menuItemRepo,IFileService fileService)
        {
           _menuItemRepo = menuItemRepo;
            _fileService = fileService;
        }
        public async Task<Result> CreateAsync(CreateMenuItemDto createMenuItemDto)
        {
            try
            {
                if (await _menuItemRepo.ExistsByNameAsync(createMenuItemDto.Name))
                {
                    return Result.Fail("This Name Already Exists");
                }
                createMenuItemDto.ImageUrl = await _fileService.UploadFile(createMenuItemDto.Image, "MenuItem");
                var MenuItem = createMenuItemDto.Adapt<MenuItem>();
                await _menuItemRepo.CreateAsync(MenuItem);
                await _menuItemRepo.SaveAsync();
                return Result.OK("Added Succseefuly!!");


            }
            catch (Exception ex)
            {
               return Result.Fail(ex.Message);
            }
         
        }

        public async Task Delete(int id)
        {

            var item = await _menuItemRepo.GetByIdAsync(id);
            if (item != null)
            {
                item.IsDeleted = true;
                await _menuItemRepo.SaveAsync();
            }
        }

        public async Task<PagedResult> GetAllAsync(int pageNumber=1,int pageSize=3)
        {
            var menuitems= await _menuItemRepo.GetItemsWithCategoryAsync(pageNumber, pageSize);
            var menuitemsmapped=menuitems.Adapt<IEnumerable<MenuItemDto>>();
            int count =( await _menuItemRepo.GetAllAsync()).Count();
            var pagedResult = new PagedResult()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                menuItems = menuitemsmapped.ToList(),
                PageCount =(int)Math.Ceiling((decimal)count / pageSize)

            };
            return pagedResult;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
        {
            var menuitems = await _menuItemRepo.GetItemsWithCategoryAsync();
            var menuitemsmapped = menuitems.Adapt<IEnumerable<MenuItemDto>>();

            return menuitemsmapped;

        }

        public async Task<MenuItemDto> GetMenuItemAsync(int id)
        {
          var menuItem=await _menuItemRepo.GetByIdAsync(id);
            return menuItem.Adapt<MenuItemDto>(); 
        }

        public async Task Update(EditMenuItemDto menuItemDto)
        {
            if(menuItemDto.Image != null)
            {
                await _fileService.DeleteFile(menuItemDto.ImageUrl, "MenuItem");
                menuItemDto.ImageUrl = await _fileService.UploadFile(menuItemDto.Image, "MenuItem");
            }
            var menuItem= menuItemDto.Adapt<MenuItem>();
            _menuItemRepo.Update(menuItem);
           await  _menuItemRepo.SaveAsync();
        }
    }
}
