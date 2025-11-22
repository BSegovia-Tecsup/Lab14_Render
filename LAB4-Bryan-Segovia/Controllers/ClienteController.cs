using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB4_Bryan_Segovia.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_unitOfWork.Clientes.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id) => Ok(_unitOfWork.Clientes.GetById(id));

    [HttpPost]
    public IActionResult Create([FromBody] Cliente cliente)
    {
        _unitOfWork.Clientes.Add(cliente);
        _unitOfWork.SaveChanges();
        return Ok("Cliente creado con éxito");
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Cliente cliente)
    {
        _unitOfWork.Clientes.Update(cliente);
        _unitOfWork.SaveChanges();
        return Ok("Cliente actualizado con éxito");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Clientes.Delete(id);
        _unitOfWork.SaveChanges();
        return Ok("Cliente eliminado con éxito");
    }
}
    