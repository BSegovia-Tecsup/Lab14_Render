using LAB4_Bryan_Segovia.Models;
using LAB4_Bryan_Segovia.Interfaz;

namespace LAB4_Bryan_Segovia.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContextTienda _context;

        public IGenericRepository<Cliente> Clientes { get; }
        public IGenericRepository<Producto> Productos { get; }
        public IGenericRepository<Categoria> Categorias { get; }
        public IGenericRepository<Ordene> Ordenes { get; }
        public IGenericRepository<Detallesorden> DetallesOrden { get; }
        public IGenericRepository<Pago> Pagos { get; }

        public UnitOfWork(DbContextTienda context)
        {
            _context = context;
            Clientes = new GenericRepository<Cliente>(_context);
            Productos = new GenericRepository<Producto>(_context);
            Categorias = new GenericRepository<Categoria>(_context);
            Ordenes = new GenericRepository<Ordene>(_context);
            DetallesOrden = new GenericRepository<Detallesorden>(_context);
            Pagos = new GenericRepository<Pago>(_context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}