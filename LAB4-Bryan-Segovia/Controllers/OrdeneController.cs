using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB4_Bryan_Segovia.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrdeneController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdeneController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_unitOfWork.Ordenes.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_unitOfWork.Ordenes.GetById(id));

    [HttpPost]
    public IActionResult Create([FromBody] Ordene orden)
    {
        _unitOfWork.Ordenes.Add(orden);
        _unitOfWork.SaveChanges();
        return Ok("Orden creada con éxito");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Ordene orden)
    {
        _unitOfWork.Ordenes.Update(orden);
        _unitOfWork.SaveChanges();
        return Ok("Orden actualizada con éxito");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Ordenes.Delete(id);
        _unitOfWork.SaveChanges();
        return Ok("Orden eliminada con éxito");
    }
}
