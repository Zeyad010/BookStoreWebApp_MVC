using BS.DataAccess.Data;
using BS.DataAccess.Repository.IRepository;
using BS.Models.Models;
using Microsoft.AspNetCore.Mvc;

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

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product obj)
		{

			


			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product Created Successfully";
				return RedirectToAction("Index");

			}
			return View();
		}

		public IActionResult Edit(int? id) // the var name in the function should be the same -> in (asp-route-the same var name) 
		{
			if (id == null || id == 0)
			{

				return NotFound();
			}

			Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id); // Find only work on PK
																				  //Product? ProductFromDb = _ProductRepo.Categories.FirstOrDefault(c => c.Id == id);
																				  //Product? ProductFromDb = _ProductRepo.Categories.Where(u=>u.Id == id).FirstOrDefault();	
			if (ProductFromDb == null)
			{
				return NotFound();
			}

			return View(ProductFromDb);
		}


		[HttpPost]
		public IActionResult Edit(Product obj)
		{



			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product Updated Successfully";

				return RedirectToAction("Index");

			}
			return View();
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
