using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB4_Bryan_Segovia.Controller;

[ApiController]
[Route("api/[controller]")]
public class PagoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_unitOfWork.Pagos.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_unitOfWork.Pagos.GetById(id));

    [HttpPost]
    public IActionResult Create([FromBody] Pago pago)
    {
        _unitOfWork.Pagos.Add(pago);
        _unitOfWork.SaveChanges();
        return Ok("Pago registrado con éxito");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Pago pago)
    {
        _unitOfWork.Pagos.Update(pago);
        _unitOfWork.SaveChanges();
        return Ok("Pago actualizado con éxito");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Pagos.Delete(id);
        _unitOfWork.SaveChanges();
        return Ok("Pago eliminado con éxito");
    }
}