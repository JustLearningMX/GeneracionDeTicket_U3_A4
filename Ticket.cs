using System;

namespace DS_DPRN2_U3_A4_HICL
{
    class Ticket
    {
        //Declaramos sus atributos
        protected decimal totalAPagar;
        protected decimal efectivo;
        protected decimal cambio;

        //Propiedades Getter y Setter
        public decimal TotalAPagar { get => totalAPagar; set => totalAPagar = value; }
        public decimal Efectivo { get => efectivo; set => efectivo = value; }
        public decimal Cambio { get => cambio; set => cambio = value; }

        //Constructor de la clase
        public Ticket(decimal _total, decimal _efectivo, decimal _cambio)
        {
            //Guardamos los datos
            this.TotalAPagar = _total;
            this.Efectivo = _efectivo;
            this.Cambio = _cambio;
        }
        public Ticket()
        {}
    }
}