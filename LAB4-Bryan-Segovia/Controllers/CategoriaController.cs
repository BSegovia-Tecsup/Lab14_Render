using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_unitOfWork.Categorias.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_unitOfWork.Categorias.GetById(id));

    [HttpPost]
    public IActionResult Create([FromBody] Categoria categoria)
    {
        _unitOfWork.Categorias.Add(categoria);
        _unitOfWork.SaveChanges();
        return Ok("Categoría creada con éxito");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Categoria categoria)
    {
        _unitOfWork.Categorias.Update(categoria);
        _unitOfWork.SaveChanges();
        return Ok("Categoría actualizada con éxito");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Categorias.Delete(id);
        _unitOfWork.SaveChanges();
        return Ok("Categoría eliminada con éxito");
    }
}
