using System;

namespace DS_DPRN2_U3_A4_HICL
{
    class Producto
    {
        //Declaramos sus atributos
        protected string nombre;
        protected int cantidad;
        protected decimal precioUnitario;
        protected decimal precioTotal;

        //Propiedades Getter y Setter
        public string Nombre { get => nombre; set => nombre = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public decimal PrecioUnitario { get => precioUnitario; set => precioUnitario = value; }
        public decimal PrecioTotal { get => precioTotal; set => precioTotal = value; }

        //Constructores de la clase
        public Producto(string _nombre, int _cantidad, decimal _unitario, decimal _total)
        {
            //Guardamos los datos
            this.Nombre = _nombre;
            this.Cantidad = _cantidad;
            this.PrecioUnitario = _unitario;
            this.PrecioTotal = _total;
        }

        public Producto()
        { }
    }
}