using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB4_Bryan_Segovia.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_unitOfWork.Productos.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_unitOfWork.Productos.GetById(id));

    [HttpPost]
    public IActionResult Create([FromBody] Producto producto)
    {
        _unitOfWork.Productos.Add(producto);
        _unitOfWork.SaveChanges();
        return Ok("Producto creado con éxito");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Producto producto)
    {
        _unitOfWork.Productos.Update(producto);
        _unitOfWork.SaveChanges();
        return Ok("Producto actualizado con éxito");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Productos.Delete(id);
        _unitOfWork.SaveChanges();
        return Ok("Producto eliminado con éxito");
    }
}
