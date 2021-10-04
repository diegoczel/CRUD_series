using System;
using System.Collections.Generic;
using DIO.Series.Interfaces;

namespace DIO.Series.Classes
{
    public class SerieRepository : IRepository<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();

        // BEGIN implements IRepository
        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }

        public void Insere(Serie entidade)
        {
            listaSerie.Add(entidade);
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Atualiza(int id, Serie entidade)
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