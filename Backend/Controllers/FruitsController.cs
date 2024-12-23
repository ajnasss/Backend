using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace TarimSulamaProjesiV2.Controllers
{
    public class FruitsController : Controller
    {
        private static List<Fruit> _fruits = new List<Fruit>
        {
            new Fruit { Id = 1, Name = "Elma", Color = "Kırmızı", WaterRequirement = 1.5 },
            new Fruit { Id = 2, Name = "Armut", Color = "Yeşil", WaterRequirement = 1.0 },
            new Fruit { Id = 2, Name = "Portakal", Color = "Turuncu", WaterRequirement = 1.2 },
            new Fruit { Id = 3, Name = "Muz", Color = "Sarı", WaterRequirement = 1.0 },
            new Fruit { Id = 4, Name = "Çilek", Color = "Kırmızı", WaterRequirement = 0.8 },
            new Fruit { Id = 5, Name = "Karpuz", Color = "Yeşil", WaterRequirement = 2.5 }
        };

        public IActionResult Index()
        {
            return View(_fruits);
        }

        [HttpPost("addFruit")]
        public IActionResult AddFruit(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newFruit = new Fruit
                {
                    Id = _fruits.Count + 1,
                    Name = name,
                    Color = "Belirtilmemiş",
                    WaterRequirement = 1.0
                };
                _fruits.Add(newFruit);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Fruit fruit)
        {
            fruit.Id = _fruits.Count + 1;
            _fruits.Add(fruit);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var fruit = _fruits.FirstOrDefault(f => f.Id == id);
            if (fruit == null)
                return NotFound();

            return View(fruit);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var fruit = _fruits.FirstOrDefault(f => f.Id == id);
            if (fruit == null)
                return NotFound();

            return View(fruit);
        }

        [HttpPost]
        public IActionResult Edit(int id, Fruit updatedFruit)
        {
            var fruit = _fruits.FirstOrDefault(f => f.Id == id);
            if (fruit == null)
                return NotFound();

            fruit.Name = updatedFruit.Name;
            fruit.Color = updatedFruit.Color;
            fruit.WaterRequirement = updatedFruit.WaterRequirement;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var fruit = _fruits.FirstOrDefault(f => f.Id == id);
            if (fruit == null)
                return NotFound();

            return View(fruit);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var fruit = _fruits.FirstOrDefault(f => f.Id == id);
            if (fruit != null)
                _fruits.Remove(fruit);

            return RedirectToAction("Index");
        }
    }
}
