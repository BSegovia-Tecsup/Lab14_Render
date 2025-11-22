using LAB4_Bryan_Segovia.Models;

namespace LAB4_Bryan_Segovia.Interfaz
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Cliente> Clientes { get; }
        IGenericRepository<Producto> Productos { get; }
        IGenericRepository<Categoria> Categorias { get; }
        IGenericRepository<Ordene> Ordenes { get; }
        IGenericRepository<Detallesorden> DetallesOrden { get; }
        IGenericRepository<Pago> Pagos { get; }
        int SaveChanges();
    }
}