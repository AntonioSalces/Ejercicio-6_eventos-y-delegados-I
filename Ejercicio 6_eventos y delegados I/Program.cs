using System;
using EjercicioUno;
using EjercicioDos;

/*Ejercicio 1: Sistema de Registro y Notificación de Ventas
Implementa un sistema que notifique y registre cada venta realizada. Crea una clase
RegistroVentas que actúe como emisor del evento VentaRealizada, junto con el método
ProcesarVenta para simular el proceso de una venta. Esta clase emitirá el evento usando
una instancia de VentaEventArgs que contiene información sobre la venta (producto y
precio). Crea dos clases receptoras: ServicioRegistro y ServicioNotificacion:
    • ServicioRegistro tiene un método RegistrarVenta que guarda los detalles de la
    venta.
    • ServicioNotificacion tiene un método EnviarNotificacionVenta que envía una
    notificación al usuario.*/

namespace EjercicioUno
{
    public class VentaEventArgs : EventArgs
    {
        public string Producto { get; set; }
        public decimal Precio { get; set; }
    }

    public class RegistroVentas
    {
        public event EventHandler<VentaEventArgs> VentaRealizada;

        protected virtual void OnVentaRealizada(VentaEventArgs e)
        {
            if (VentaRealizada != null)
            {
                VentaRealizada(this, e);
            }
        }

        public void ProcesarVenta(string producto, decimal precio)
        {
            Console.WriteLine("[Servicio de procesamiento] Procesando venta de " + producto + " por  " + precio);
            OnVentaRealizada(new VentaEventArgs { Producto = producto, Precio = precio });
        }
    }

    public class ServicioRegistro
    {
        public void RegistrarVenta(object sender, VentaEventArgs e)
        {
            Console.WriteLine($"[Servicio de registro] Producto: " + e.Producto + " Precio: " + e.Precio);
        }
    }

    public class ServicioNotificacion
    {
        public void EnviarNotificacionVenta(object sender, VentaEventArgs e)
        {
            Console.WriteLine("[Servicio de notificación] Se realizó una venta de " + e.Producto + " por " + e.Precio);
        }
    }

    public static class EjercicioUno
    {
        public static void Ejecutar()
        {
            var registroVentas = new RegistroVentas();
            var servicioRegistro = new ServicioRegistro();
            var servicioNotificacion = new ServicioNotificacion();

            registroVentas.VentaRealizada += servicioRegistro.RegistrarVenta;
            registroVentas.VentaRealizada += servicioNotificacion.EnviarNotificacionVenta;

            registroVentas.ProcesarVenta("Bandera Betis", 15.50m);
            Console.WriteLine();
            registroVentas.ProcesarVenta("Camiseta Betis Centenario", 90);
        }
    }
}

/*Ejercicio 2: Sistema de Control de Temperatura en un Invernadero
Diseña una clase ControlTemperatura que supervise la temperatura del invernadero y
emita el evento TemperaturaAlta cuando la temperatura exceda un umbral. Usa
TemperaturaEventArgs para transmitir la temperatura actual y el umbral. Crea las clases
ServicioAlerta y ServicioRegistroTemperatura para manejar este evento:
    • ServicioAlerta enviará una alerta en consola.
    • ServicioRegistroTemperatura registrará la temperatura en consola.*/
namespace EjercicioDos
{
    public class TemperaturaEventArgs : EventArgs
    {
        public double TemperaturaActual { get; set; }
        public double Umbral { get; set; }
    }

    public class ControlTemperatura
    {
        public event EventHandler<TemperaturaEventArgs> TemperaturaAlta;

        private double umbral;

        public ControlTemperatura(double umbral)
        {
            this.umbral = umbral;
        }

        protected virtual void OnTemperaturaAlta(TemperaturaEventArgs e)
        {
            TemperaturaAlta?.Invoke(this, e);
        }

        public void ActualizarTemperatura(double temperaturaActual)
        {
            Console.WriteLine("[ControlTemperatura] Temperatura actual: " + temperaturaActual);
            if (temperaturaActual > umbral)
            {
                OnTemperaturaAlta(new TemperaturaEventArgs { TemperaturaActual = temperaturaActual, Umbral = umbral });
            }
        }
    }

    public class ServicioAlerta
    {
        public void EnviarAlerta(object sender, TemperaturaEventArgs e)
        {
            Console.WriteLine("[ServicioAlerta] ALERTA: Temperatura alta! Actual: " + e.TemperaturaActual + " °C, Umbral: " + e.Umbral + " Cº");
        }
    }

    public class ServicioRegistroTemperatura
    {
        public void RegistrarTemperatura(object sender, TemperaturaEventArgs e)
        {
            Console.WriteLine("[ServicioRegistroTemperatura] Registro: Temperatura actual = " + e.TemperaturaActual + "°C, Umbral = " + e.Umbral + "Cº");
        }
    }

    public static class EjercicioDos
    {
        public static void Ejecutar()
        {
            var control = new ControlTemperatura(30.0);
            var alerta = new ServicioAlerta();
            var registro = new ServicioRegistroTemperatura();

            control.TemperaturaAlta += alerta.EnviarAlerta;
            control.TemperaturaAlta += registro.RegistrarTemperatura;

            control.ActualizarTemperatura(28.5);
            control.ActualizarTemperatura(31.2);
        }
    }
}


namespace ProgramaPrincipal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ejercicio 1");
            EjercicioUno.EjercicioUno.Ejecutar();
            Console.WriteLine("");

            Console.WriteLine("Ejercicio 2" +
                "");
            EjercicioDos.EjercicioDos.Ejecutar();
            Console.WriteLine("");
        }
    }
}
