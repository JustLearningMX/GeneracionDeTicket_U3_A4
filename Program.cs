using System;
using System.Collections.Generic;

namespace DS_DPRN2_U3_A4_HICL
{
    class Program
    {
        //DECLARACIONES GLOBALES DE DOS LISTAS

        //Creamos un objeto del tipo lista para el PRODUCTO
        public static List<Producto> listaProductos = new List<Producto>();

        //Creamos un objeto del tipo lista para el TICKET
        public static List<Ticket> listaTickets = new List<Ticket>();

        static void Main()//Método principal
        {
            byte? opcionMenuPrincipal = null;

            try//Esta sentencia Try-Catch gestionará las excepciones
            {  //que se produzcan en el programa

                do
                {//Mostraremos el menú mientras no se elija SALIR
                    MenuPrincipal();//Se llama al menu principal
                    opcionMenuPrincipal = ElegirOpcion();//Se elige opción del menú principal
                    RealizarAccion(opcionMenuPrincipal);//Se lleva a cabo la tarea  elegida
                    //Console.ReadKey();//Espera de una tecla
                }
                while (opcionMenuPrincipal != 6);//si se elije SALIR finaliza el ciclo

            }
            catch (Exception e)
            {   //Se invoca método para controlar los errores
                ControlarExcepciones(e);//Se envía por parámetro el error
                Main();//Después de indicar el error, se vuelve al menú principal
            }
        }

        static void RealizarAccion(byte? opcionMenuPrincipal)
        {   //Variable para almacenar los datos del producto instanciado de la clase Producto
            Producto producto = null;

            //Variable para almacenar los datos del ticket instanciado de la clase Ticket
            Ticket ticket = null;

            switch (opcionMenuPrincipal)
            {
                case 1://AGREGAR ELEMENTO AL TICKET

                    //Invocamos método para agregar elementos al ticket y                 
                    producto = AgregarElementosAlTicket();

                    //Agregamos el producto capturado a la lista de productos
                    listaProductos.Add(producto);
                    break;

                case 2://ELIMINAR ELEMENTO DEL TICKET
                    if (listaProductos.Count <= 0)
                    {
                        //se lanza la excepción propia
                        throw new GestionDeErroresPropiosException("    ¡NO HAY PRODUCTOS INGRESADOS!");
                    }
                    else
                    {
                        EliminarElementosDellTicket();
                        Console.ReadKey();
                    }
                    break;

                case 3://FINALIZAR VENTA
                    if (listaProductos.Count <= 0)
                    {
                        //se lanza la excepción propia
                        throw new GestionDeErroresPropiosException("    ¡NO HAY PRODUCTOS INGRESADOS!");
                    }
                    else
                    {
                        ticket = FinalizarVenta();
                        //Agregamos el ticket finalizado a la lista de tickets
                        listaTickets.Add(ticket);
                        Console.ReadKey();
                    }

                    break;

                case 4://CANCELAR VENTA
                    if (listaProductos.Count <= 0)
                    {
                        //se lanza la excepción propia
                        throw new GestionDeErroresPropiosException("    ¡NO HAY PRODUCTOS INGRESADOS!");
                    }
                    else
                    {
                        CancelarVenta();
                    }
                    break;

                case 5://IMPRIMIR TICKET
                    if (listaProductos.Count <= 0)//Si no hay productos en la lista
                    {
                        //se lanza la excepción propia
                        throw new GestionDeErroresPropiosException("    ¡NO SE HA GENERADO NINGÚN TICKET!");
                    }
                    else if (listaTickets.Count <= 0)//Si no se ha finalizado la venta
                    {
                        //se lanza la excepción propia
                        throw new GestionDeErroresPropiosException("    FINALICE PRIMERO LA VENTA");
                    }
                    else//Si ya hay productos en la lista
                    {
                        ImprimirTicket();
                        Console.ReadKey();
                    }
                    break;

                case 6: //Salir del programa
                    break;

                default:
                    Console.WriteLine("No existe esa opción");
                    break;
            }
        }

        static void CancelarVenta()
        {
            DibujaTitulo();
            decimal total = 0;

            Console.SetCursorPosition(38, 9);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("¿DESEA CANCELAR LA VENTA? [ ]    [S]i - [N]o");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 12);
            Console.WriteLine("#  Producto   Cant.     Precio      Total   ");
            Console.SetCursorPosition(38, 13);
            Console.WriteLine("--------------------------------------------");

            //Se recorre la lista de los productos para imprimir la info
            for (int i = 1; i <= listaProductos.Count; i++)
            {
                Console.SetCursorPosition(38, 13 + (i));//Nombre
                Console.WriteLine(i + ".");

                Console.SetCursorPosition(42, 13 + (i));//Nombre
                Console.WriteLine(listaProductos[i - 1].Nombre);

                Console.SetCursorPosition(53, 13 + (i));//Cantidad
                Console.WriteLine(listaProductos[i - 1].Cantidad);

                Console.SetCursorPosition(62, 13 + (i));//Precio unitario
                Console.WriteLine("$" + listaProductos[i - 1].PrecioUnitario);

                Console.SetCursorPosition(74, 13 + (i));//Precio total
                Console.WriteLine("$" + listaProductos[i - 1].PrecioTotal);

                total = total + listaProductos[i - 1].PrecioTotal;
            }

            Console.SetCursorPosition(38, 14 + listaProductos.Count);
            Console.WriteLine("--------------------------------------------");
            Capturar();

            void Capturar()
            {
                string opcion = null;
                try
                {
                    Console.SetCursorPosition(65, 10);
                    Console.WriteLine(" ");

                    Console.SetCursorPosition(65, 10);
                    opcion = Console.ReadLine();

                    if (opcion.ToLower().Equals("s"))
                    {
                        listaProductos.Clear();
                        listaTickets.Clear();
                    }
                    else if (opcion.ToLower().Equals("n"))
                    {

                    }
                    else
                    {
                        //se lanza una excepción propia
                        throw new GestionDeErroresPropiosException("          Ingrese [S] ó [N]");
                    }
                }
                catch (Exception e)
                {   //Se invoca método para controlar los errores
                    ControlarExcepciones(e);//Se envía por parámetro el error
                    //Console.ReadKey();//Espera de una tecla
                    Capturar();//Se pide nuevamente el dato
                }
            }
        }

        static void EliminarElementosDellTicket()
        {
            DibujaTitulo();
            decimal total = 0;

            Console.SetCursorPosition(38, 9);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("  ELIJA EL PRODUCTO QUE DESEA ELIMINAR [ ]  ");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 12);
            Console.WriteLine("#  Producto   Cant.     Precio      Total   ");
            Console.SetCursorPosition(38, 13);
            Console.WriteLine("--------------------------------------------");

            //Se recorre la lista de los productos para imprimir la info
            for (int i = 1; i <= listaProductos.Count; i++)
            {
                Console.SetCursorPosition(38, 13 + (i));//Nombre
                Console.WriteLine(i + ".");

                Console.SetCursorPosition(42, 13 + (i));//Nombre
                Console.WriteLine(listaProductos[i - 1].Nombre);

                Console.SetCursorPosition(53, 13 + (i));//Cantidad
                Console.WriteLine(listaProductos[i - 1].Cantidad);

                Console.SetCursorPosition(62, 13 + (i));//Precio unitario
                Console.WriteLine("$" + listaProductos[i - 1].PrecioUnitario);

                Console.SetCursorPosition(74, 13 + (i));//Precio total
                Console.WriteLine("$" + listaProductos[i - 1].PrecioTotal);

                total = total + listaProductos[i - 1].PrecioTotal;
            }

            Console.SetCursorPosition(38, 14 + listaProductos.Count);
            Console.WriteLine("--------------------------------------------");
            Capturar();

            void Capturar()
            {
                byte? opcion = null;
                try
                {
                    Console.SetCursorPosition(77, 10);
                    Console.WriteLine("[ ]   ");

                    Console.SetCursorPosition(78, 10);
                    opcion = Convert.ToByte(Console.ReadLine());

                    if (opcion <= 0 || opcion > listaProductos.Count)
                    {
                        //se lanza una excepción propia
                        throw new GestionDeErroresPropiosException("  ¡NO EXISTE ESE PRODUCTO EN LA LISTA!");
                    }
                    else
                    {
                        listaProductos.Remove(listaProductos[(Int16)opcion - 1]);
                    }
                }
                catch (Exception e)
                {   //Se invoca método para controlar los errores
                    ControlarExcepciones(e);//Se envía por parámetro el error
                    //Console.ReadKey();//Espera de una tecla
                    Capturar();//Se pide nuevamente el dato
                }
            }
        }

        static Ticket FinalizarVenta()
        {
            DibujaTitulo();
            decimal total = 0;
            decimal efectivo = 0;

            //Instanciamos un objeto de la clase Ticket
            Ticket ticket = new Ticket();

            Console.SetCursorPosition(38, 9);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("            FINALIZANDO LA VENTA            ");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 12);
            Console.WriteLine("PRODUCTO      CANT.     PRECIO      TOTAL   ");
            Console.SetCursorPosition(38, 13);
            Console.WriteLine("--------------------------------------------");

            //Se recorre la lista de los productos para imprimir la info
            for (int i = 1; i <= listaProductos.Count; i++)
            {
                Console.SetCursorPosition(39, 13 + (i));//Nombre
                Console.WriteLine(listaProductos[i - 1].Nombre);

                Console.SetCursorPosition(53, 13 + (i));//Cantidad
                Console.WriteLine(listaProductos[i - 1].Cantidad);

                Console.SetCursorPosition(62, 13 + (i));//Precio unitario
                Console.WriteLine("$" + listaProductos[i - 1].PrecioUnitario);

                Console.SetCursorPosition(74, 13 + (i));//Precio total
                Console.WriteLine("$" + listaProductos[i - 1].PrecioTotal);

                total = total + listaProductos[i - 1].PrecioTotal;
            }

            Console.SetCursorPosition(38, 15 + listaProductos.Count);
            Console.WriteLine("--------------------------------------------");
            Console.SetCursorPosition(38, 16 + listaProductos.Count);
            Console.WriteLine("                        TOTAL:              ");
            Console.SetCursorPosition(38, 17 + listaProductos.Count);
            Console.WriteLine("                     EFECTIVO: $            ");
            Console.SetCursorPosition(38, 18 + listaProductos.Count);
            Console.WriteLine("                       CAMBIO: $            ");
            Console.SetCursorPosition(38, 19 + listaProductos.Count);
            Console.WriteLine("--------------------------------------------");

            Console.SetCursorPosition(69, 16 + listaProductos.Count);
            Console.WriteLine("$" + total);
            CapturaEfectivo();

            void CapturaEfectivo()
            {
                try
                {
                    Console.SetCursorPosition(70, 17 + listaProductos.Count);
                    Console.WriteLine("           ");
                    Console.SetCursorPosition(70, 17 + listaProductos.Count);
                    efectivo = Convert.ToDecimal(Console.ReadLine());

                    if (efectivo < total)
                    {
                        //se lanza una excepción propia
                        throw new GestionDeErroresPropiosException("               ¡NO ALCANZA!");
                    }
                }
                catch (Exception e)
                {   //Se invoca método para controlar los errores
                    ControlarExcepciones(e);//Se envía por parámetro el error
                    //Console.ReadKey();//Espera de una tecla
                    CapturaEfectivo();
                }
            }

            //Cambio del cliente
            Console.SetCursorPosition(70, 18 + listaProductos.Count);
            Console.WriteLine(efectivo - total);

            ticket.TotalAPagar = total;
            ticket.Efectivo = efectivo;
            ticket.Cambio = efectivo - total;

            return ticket;
        }

        static void ImprimirTicket()
        {
            DibujaTitulo();

            //Colocamos algunas etiquetas
            Console.SetCursorPosition(38, 9);
            Console.WriteLine("           TIENDITA DE LA ESQUINA           ");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("              Su tienda amiga               ");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("********************************************");
            Console.SetCursorPosition(38, 12);
            Console.WriteLine("              TICKET DE COMPRA              ");
            Console.SetCursorPosition(38, 13);
            Console.WriteLine("********************************************");
            Console.SetCursorPosition(38, 14);
            Console.WriteLine("PRODUCTO      CANT.     PRECIO      TOTAL   ");
            Console.SetCursorPosition(38, 15);
            Console.WriteLine("____________________________________________");

            //Se recorre la lista de los productos para imprimir la info
            for (int i = 1; i <= listaProductos.Count; i++)
            {
                Console.SetCursorPosition(39, 15 + (i));//Nombre
                Console.WriteLine(listaProductos[i - 1].Nombre);

                Console.SetCursorPosition(53, 15 + (i));//Cantidad
                Console.WriteLine(listaProductos[i - 1].Cantidad);

                Console.SetCursorPosition(62, 15 + (i));//Precio unitario
                Console.WriteLine("$" + listaProductos[i - 1].PrecioUnitario);

                Console.SetCursorPosition(74, 15 + (i));//Precio total
                Console.WriteLine("$" + listaProductos[i - 1].PrecioTotal);
            }

            Console.SetCursorPosition(38, 17 + listaProductos.Count);
            Console.WriteLine("********************************************");
            Console.SetCursorPosition(38, 18 + listaProductos.Count);
            Console.WriteLine("                        TOTAL: $            ");
            Console.SetCursorPosition(38, 19 + listaProductos.Count);
            Console.WriteLine("                     EFECTIVO: $            ");
            Console.SetCursorPosition(38, 20 + listaProductos.Count);
            Console.WriteLine("                       CAMBIO: $            ");
            Console.SetCursorPosition(38, 21 + listaProductos.Count);
            Console.WriteLine("********************************************");

            Console.SetCursorPosition(70, 18 + listaProductos.Count);//TOTAL del ticket
            Console.WriteLine(listaTickets[0].TotalAPagar);

            Console.SetCursorPosition(70, 19 + listaProductos.Count);//Efectivo
            Console.WriteLine(listaTickets[0].Efectivo);

            Console.SetCursorPosition(70, 20 + listaProductos.Count);//Efectivo
            Console.WriteLine(listaTickets[0].Cambio);
        }

        static Producto AgregarElementosAlTicket()
        {
            //Instanciamos un objeto de la clase Producto
            Producto producto = new Producto();
            Capturar();

            void Capturar()
            {
                DibujaTitulo();

                //Colocamos algunas etiquetas
                Console.SetCursorPosition(38, 9);
                Console.WriteLine("|------------------------------------------|");
                Console.SetCursorPosition(38, 10);
                Console.WriteLine("|      INGRESE LOS DATOS DEL PRODUCTO      |");
                Console.SetCursorPosition(38, 11);
                Console.WriteLine("|------------------------------------------|");
                Console.SetCursorPosition(38, 12);
                Console.WriteLine("|          NOMBRE:                         |");
                Console.SetCursorPosition(38, 13);
                Console.WriteLine("|        CANTIDAD:                         |");
                Console.SetCursorPosition(38, 14);
                Console.WriteLine("| PRECIO UNITARIO:                         |");
                Console.SetCursorPosition(38, 15);
                Console.WriteLine("|    PRECIO TOTAL:                         |");
                Console.SetCursorPosition(38, 16);
                Console.WriteLine("|------------------------------------------|");

                try
                {
                    //solicitamos los datos
                    Console.SetCursorPosition(57, 12);
                    producto.Nombre = Console.ReadLine();//Nombre del producto
                    Console.SetCursorPosition(57, 13);
                    producto.Cantidad = Convert.ToInt16(Console.ReadLine());//Cantidad
                    Console.SetCursorPosition(57, 14);
                    producto.PrecioUnitario = Convert.ToDecimal(Console.ReadLine());//Precio unitario
                    Console.SetCursorPosition(57, 15);
                    Console.WriteLine(producto.Cantidad * producto.PrecioUnitario);

                    producto.PrecioTotal = producto.Cantidad * producto.PrecioUnitario;//Precio total

                    Console.ReadKey();
                }
                catch (Exception e)
                {   //Se invoca método para controlar los errores
                    ControlarExcepciones(e);//Se envía por parámetro el error
                    Capturar();
                }
            }

            //Retornamos el objeto Producto creado
            return producto;
        }
        static byte? ElegirOpcion()
        {//Método para elegir opción del menú principal
            byte? opcionMenuPrincipal = null;//Variable que guarda la opción elegida

            Console.SetCursorPosition(58, 19);//Se captura la opcion deseada
            opcionMenuPrincipal = Convert.ToByte(Console.ReadLine());

            if (opcionMenuPrincipal < 1 || opcionMenuPrincipal > 6)
            {
                throw new GestionDeErroresPropiosException("            ¡OPCIÓN NO VÁLIDA!");
            }
            else
            {
                return opcionMenuPrincipal;//Se retorna la opción elegida
            }
        }

        static void MenuPrincipal()//Método que implementa al menú principal
        {

            DibujaTitulo();
            //Menú de opciones
            Console.SetCursorPosition(38, 9);
            Console.WriteLine("|------------------------------------------|");
            Console.SetCursorPosition(38, 10);
            Console.WriteLine("|      GENERACIÓN DE TICKET DE VENTA       |");
            Console.SetCursorPosition(38, 11);
            Console.WriteLine("|------------------------------------------|");
            Console.SetCursorPosition(38, 12);
            Console.WriteLine("| 1. Agregar producto.                     |");
            Console.SetCursorPosition(38, 13);
            Console.WriteLine("| 2. Eliminar producto.                    |");
            Console.SetCursorPosition(38, 14);
            Console.WriteLine("| 3. Finalizar venta.                      |");
            Console.SetCursorPosition(38, 15);
            Console.WriteLine("| 4. Cancelar venta.                       |");
            Console.SetCursorPosition(38, 16);
            Console.WriteLine("| 5. Imprimir ticket.                      |");
            Console.SetCursorPosition(38, 17);
            Console.WriteLine("| 6. Salir.                                |");
            Console.SetCursorPosition(38, 18);
            Console.WriteLine("|------------------------------------------|");
            Console.SetCursorPosition(38, 19);
            Console.WriteLine("| ELIJA UNA OPCIÓN [ ]                     |");
            Console.SetCursorPosition(38, 20);
            Console.WriteLine("|------------------------------------------|");
        }

        static void ControlarExcepciones(Exception e)//Método para controlar las 
        //excepciones. Según la excepción muestra un mensaje
        {
            //Variable para almacenar tipo de error
            string error = null;
            error = Convert.ToString(e.GetType());

            //Si es un error por que se ingresó letras
            if (error.Equals("System.FormatException"))
            {
                Console.SetCursorPosition(40, 24);//Mensaje para el usuario
                Console.WriteLine("¡INGRESE NÚMEROS SIN LETRAS!");

            }//Si es un error por que se ingresó un número muy grande o muy pequeño
            else
            {
                if (error.Equals("System.OverflowException"))
                {
                    Console.SetCursorPosition(40, 24);//Mensaje para el usuario
                    Console.WriteLine("** ¡NÚMERO MUY GRANDE! **");
                }//Si es un error por fuera de rango de un array
                else
                {
                    if (error.Equals("System.IndexOutOfRangeException"))
                    {
                        Console.SetCursorPosition(40, 24);//Mensaje para el usuario
                        Console.WriteLine("SUPERÓ EL RANGO DE LA MATRIZ");
                    }
                    else
                    {
                        Console.SetCursorPosition(40, 24);
                        Console.WriteLine(e.Message);//Mensaje para el usuario
                    }
                }
            }

            Console.ReadKey();//Espera de una tecla
            Console.SetCursorPosition(40, 24);
            Console.WriteLine("                                                 ");//Se limpia el mensaje
        }

        static void DibujaTitulo()//Método que permite agregar título a la ventana principal
        {
            //Mensajes de bienvenida al programa
            Console.Clear();
            Console.SetCursorPosition(40, 1);
            Console.WriteLine("****************************************");
            Console.SetCursorPosition(40, 2);
            Console.WriteLine("*        Programación .NET II          *");
            Console.SetCursorPosition(40, 3);
            Console.WriteLine("*         Unidad 3 Actividad 4         *");
            Console.SetCursorPosition(40, 4);
            Console.WriteLine("*               UNADM                  *");
            Console.SetCursorPosition(40, 5);
            Console.WriteLine("*  Manejo seguro de colecciones en C#  *");
            Console.SetCursorPosition(40, 6);
            Console.WriteLine("*      Alumno: Hiram Chávez López      *");
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("****************************************");
        }
    }
}
