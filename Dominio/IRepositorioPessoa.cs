
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public interface IRepositorioPessoa 
    {
        
        void Create(string nome, string sobrenome, DateTime niver);
        IEnumerable<Pessoa> GetPessoas();
        void update(int id, Pessoa pessoa);
        void delete(int id, Pessoa pessoa);
        
            
    }
}
