using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series.Classes
{
    public class FilmeRepository : IRepository<Filme>
    {
        private List<Filme> listaSerie = new List<Filme>();

        // BEGIN implements IRepository
        public List<Filme> Lista()
        {
            return listaSerie;
        }

        public Filme RetornaPorId(int id)
        {
            return listaSerie[id];
        }

        public void Insere(Filme entidade)
        {
            listaSerie.Add(entidade);
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Atualiza(int id, Filme entidade)
        {
            listaSerie[id] = entidade;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }
        // END implements IRepository
    }
}