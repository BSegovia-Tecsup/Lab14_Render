using LAB4_Bryan_Segovia.Models;
namespace LAB4_Bryan_Segovia.Interfaz
{
    public interface IClienteRepository
    {
        Cliente GetById(int id);
        IEnumerable<Cliente> GetAll();
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
    }
    
}
    




