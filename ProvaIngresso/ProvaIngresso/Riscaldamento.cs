using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaIngresso
{
	class Riscaldamento
	{
		protected string nome;
		protected double rendimento;
		protected double costo_installazione;
		protected double costo_annuo;
		protected string tipo_consumo;
		public double costo_totale;
		public double consumo;
		public Riscaldamento(string nome, string tipo_consumo, double rendimento, double costo_installazione, double costo_annuo, double costo_totale, double consumo)
		{
			this.nome = nome;
			this.rendimento = rendimento;
			this.costo_installazione = costo_installazione;
			this.costo_annuo = costo_annuo;
			this.tipo_consumo = tipo_consumo;
			this.costo_totale = costo_totale;
			this.consumo = consumo;
		}
		public string Get_nome()
        {
			return nome;
        }
		public string Get_tipo_consumo()
		{
			return tipo_consumo;
		}
		public double Get_rendimento()
		{
			return rendimento;
		}
		public double Get_costo_installazione()
		{
			return costo_installazione;
		}
		public double Get_costo_annuo()
		{
			return costo_annuo;
		}
		public double Get_costo_totale()
		{
			return costo_totale;
		}
		public double Get_consumo()
		{
			return consumo;
		}
	}
}
