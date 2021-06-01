﻿using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace DepartmentManagement.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _repository.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Department();

            if (id == null)
            {
                return View(model);
            }

            model = await _repository.GetAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Department model)
        {
            if (model.Id == 0)
            {
                await _repository.AddAsync(model);
            }
            else
            {
                await _repository.UpdateAsync(model.Id, model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _repository.GetAsync(id);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _repository.GetAsync(id);

            if (model != null)
            {
                await _repository.DeleteAsync(model.Id);
            }

            return RedirectToAction("Index");
        }
    } 
}