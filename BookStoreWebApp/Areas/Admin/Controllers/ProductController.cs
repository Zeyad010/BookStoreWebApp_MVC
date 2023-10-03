﻿using BS.DataAccess.Data;
using BS.DataAccess.Repository.IRepository;
using BS.Models.Models;
using BS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWebApp.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
			
			return View(objProductList);
		}

		public IActionResult Upsert(int? id) // Upsert --> Upadte & Insert
		{

			ProductVM productVM = new ProductVM()
			{


				CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()

				}),
				Product = new Product()

			};	

			if(id == null || id == 0)
			{
				//create
				return View(productVM);


			}

			else
			{
				//update
				productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
				return View(productVM);

			}


		}

		[HttpPost]
		public IActionResult Upsert(ProductVM productVM,IFormFile? file)
		{

			


			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(productVM.Product);
				_unitOfWork.Save();
				TempData["success"] = "Product Created Successfully";
				return RedirectToAction("Index");


			}
			else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()

				});

				return View(productVM);
			}
			
		}

		

		

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{

				return NotFound();
			}

			Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);

			if (ProductFromDb == null)
			{
				return NotFound();
			}

			return View(ProductFromDb);
		}


		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Product.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Product Deleted Successfully";

			return RedirectToAction("Index");



		}

	}
}
