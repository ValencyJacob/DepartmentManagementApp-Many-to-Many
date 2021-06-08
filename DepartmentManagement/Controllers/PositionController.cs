using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace DepartmentManagement.Controllers
{
    [Authorize(Roles = Common.Common.AdminRole)]
    public class PositionController : Controller
    {
        private readonly IPositionRepository _repository;

        public PositionController(IPositionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _repository.GetAllAsync();

            if (models != null)
            {
                return View(models);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Position();

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
        public async Task<IActionResult> Upsert(Position model)
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

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
