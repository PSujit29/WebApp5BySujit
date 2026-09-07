using Microsoft.AspNetCore.Mvc;
using WebApp5BySujit.Data;
using WebApp5BySujit.Models;

namespace WebApp5BySujit.Controllers;

public class ProductController : Controller
{
    private readonly ProductRepository _repo;
    public ProductController(ProductRepository repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll());

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Product p) { _repo.Add(p); return RedirectToAction(nameof(Index)); }

    public IActionResult Edit(int id) => View(_repo.GetById(id));

    [HttpPost]
    public IActionResult Edit(Product p) { _repo.Update(p); return RedirectToAction(nameof(Index)); }

    public IActionResult Delete(int id) => View(_repo.GetById(id));

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id) { _repo.Delete(id); return RedirectToAction(nameof(Index)); }
}