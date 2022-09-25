using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaIngresso
{
    class Stufa_elettrica : Riscaldamento
    {
		public Stufa_elettrica(string nome, string tipo_consumo, double rendimento, double costo_installazione, double costo_annuo, double costo_totale, double consumo) : base(nome, tipo_consumo, rendimento, costo_installazione, costo_annuo, costo_totale, consumo)
		{
			this.nome = nome;
			this.rendimento = rendimento;
			this.costo_installazione = costo_installazione;
			this.costo_annuo = costo_annuo;
			this.tipo_consumo = tipo_consumo;
			this.costo_totale = costo_totale;
			this.consumo = consumo;
		}
	}
}
