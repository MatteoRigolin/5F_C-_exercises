using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaIngresso
{
    class Bolletta
    {
        protected string nome_riscaldamento;
        protected double spesa_materia;
        protected double spesa_contatore;
        protected double spesa_oneri;
        protected double spesa_QVD;
        protected double spesa_installazione;
        public Bolletta(string nome_riscaldamento, double spesa_materia, double spesa_contatore, double spesa_oneri, double spesa_QVD, double spesa_installazione)
        {
            this.nome_riscaldamento = nome_riscaldamento;
            this.spesa_materia = spesa_materia;
            this.spesa_contatore = spesa_contatore;
            this.spesa_oneri = spesa_oneri;
            this.spesa_QVD = spesa_QVD;
            this.spesa_installazione = spesa_installazione;
        }
        public string Get_nome_riscaldamento()
        {
            return nome_riscaldamento;
        }
        public double Calcolo_totale()
        {
            return (spesa_materia + spesa_contatore + spesa_oneri + spesa_QVD + spesa_installazione);
        }
    }
}
