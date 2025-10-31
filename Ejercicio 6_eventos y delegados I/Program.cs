using System;
using EjercicioOne;
using EjercicioTwo;
using EjercicioThree;
using EjercicioFour;
using EjercicioFive;

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

namespace EjercicioOne
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
namespace EjercicioTwo
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
            if (TemperaturaAlta != null)
            {
                TemperaturaAlta(this, e);
            }
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

/*Ejercicio 3: Sistema de Backup y Notificación de Archivos
Implementa un sistema que gestione la creación de copias de seguridad. La clase
GestorBackups debe emitir el evento BackupCompletado cuando un archivo se haya
respaldado. BackupEventArgs incluye el nombre del archivo y la fecha. Crea
ServicioNotificacion para enviar una notificación y ServicioLog para registrar la
operación de respaldo*/
namespace EjercicioThree
{
    public class BackupEventArgs : EventArgs
    {
        public string NombreArchivo { get; set; }
        public DateTime FechaBackup { get; set; }
    }

    public class GestorBackups
    {
        public event EventHandler<BackupEventArgs> BackupCompletado;

        protected virtual void OnBackupCompletado(BackupEventArgs e)
        {
            if (BackupCompletado != null)
            {
                BackupCompletado(this, e);
            }
        }

        public void CrearBackup(string nombreArchivo)
        {
            Console.WriteLine("[GestorBackups] Iniciando backup del archivo: " + nombreArchivo);
            OnBackupCompletado(new BackupEventArgs { NombreArchivo = nombreArchivo, FechaBackup = DateTime.Now });
        }
    }

    public class ServicioNotificacion
    {
        public void EnviarNotificacion(object sender, BackupEventArgs e)
        {
            Console.WriteLine("[ServicioNotificacion] Notificación: Backup completado para el archivo " + e.NombreArchivo + " a día " + e.FechaBackup);
        }
    }

    public class ServicioLog
    {
        public void RegistrarBackup(object sender, BackupEventArgs e)
        {
            Console.WriteLine("[ServicioLog] Registro: Archivo respaldado " + e.NombreArchivo + " a las " + e.FechaBackup);
        }
    }

    public static class EjercicioTres
    {
        public static void Ejecutar()
        {
            var gestor = new GestorBackups();
            var notificacion = new ServicioNotificacion();
            var log = new ServicioLog();
            gestor.BackupCompletado += notificacion.EnviarNotificacion;
            gestor.BackupCompletado += log.RegistrarBackup;
            gestor.CrearBackup("betisWeno.txt");
        }
    }
}

/*Ejercicio 4: Sistema de Monitoreo de Sensores de Puertas y Ventanas
Crea un sistema de monitoreo para una casa inteligente que controle el estado de
puertas y ventanas. Diseña una clase SensorMonitoreo que emita el evento
AlertaIntruso cuando se detecta una puerta o ventana abierta fuera del horario
permitido. Usa IntrusoEventArgs para incluir detalles del sensor (nombre de la
puerta/ventana y la hora de detección). Crea dos servicios que respondan a este evento:
• ServicioAlarma activa una alarma.
• ServicioRegistroIncidencias guarda un registro en la base de datos.*/
namespace EjercicioFour
{
    public class IntrusoEventArgs : EventArgs
    {
        public string NombreSensor { get; set; }
        public DateTime HoraDeteccion { get; set; }
    }

    public class SensorMonitoreo
    {
        public event EventHandler<IntrusoEventArgs> AlertaIntruso;

        protected virtual void OnAlertaIntruso(IntrusoEventArgs e)
        {
            if (AlertaIntruso != null)
            {
                AlertaIntruso(this, e);
            }
        }

        public void DetectarIntruso(string nombreSensor)
        {
            Console.WriteLine("[SensorMonitoreo] Detección de intruso en: " + nombreSensor);
            OnAlertaIntruso(new IntrusoEventArgs { NombreSensor = nombreSensor, HoraDeteccion = DateTime.Now });
        }
    }

    public class ServicioAlarma
    {
        public void ActivarAlarma(object sender, IntrusoEventArgs e)
        {
            Console.WriteLine("[ServicioAlarma] ¡ALARMA activada! Intruso detectado en: " + e.NombreSensor + " a las " + e.HoraDeteccion);
        }
    }

    public class ServicioRegistroIncidencias
    {
        public void RegistrarIncidencia(object sender, IntrusoEventArgs e)
        {
            Console.WriteLine("[ServicioRegistroIncidencias] Registro de incidencia: " + e.NombreSensor + ", hora: " + e.HoraDeteccion);
        }
    }

    public static class EjercicioCuatro
    {
        public static void Ejecutar()
        {
            var sensor = new SensorMonitoreo();
            var alarma = new ServicioAlarma();
            var registro = new ServicioRegistroIncidencias();
            sensor.AlertaIntruso += alarma.ActivarAlarma;
            sensor.AlertaIntruso += registro.RegistrarIncidencia;
            sensor.DetectarIntruso("Al final de la palmera");
            sensor.DetectarIntruso("Vitrina copa del rey");
        }
    }
}

/*Ejercicio 5: Sistema de Supervisión de Consumo de Energía
Diseña una clase MonitorEnergia que registre el consumo de energía y emita el evento
ConsumoExcesivoDetectado cuando el consumo supere un umbral establecido. Usa
EnergiaEventArgs para proporcionar el consumo actual y el umbral. Implementa las
clases ServicioNotificacion y ServicioAjusteAutomatizado:
• ServicioNotificacion envía una advertencia al usuario.
• ServicioAjusteAutomatizado ajusta automáticamente los dispositivos para
reducir el consumo.*/

namespace EjercicioFive
{
    public class EnergiaEventArgs : EventArgs
    {
        public double ConsumoActual { get; set; }
        public double Umbral { get; set; }
    }

    public class MonitorEnergia
    {
        public event EventHandler<EnergiaEventArgs> ConsumoExcesivoDetectado;
        private double umbral;

        public MonitorEnergia(double umbral)
        {
            this.umbral = umbral;
        }

        protected virtual void OnConsumoExcesivoDetectado(EnergiaEventArgs e)
        {
            if (ConsumoExcesivoDetectado != null)
            {
                ConsumoExcesivoDetectado(this, e);
            }
        }

        public void RegistrarConsumo(double consumoActual)
        {
            Console.WriteLine("[MonitorEnergia] Consumo actual: " + consumoActual + " kW");
            if (consumoActual > umbral)
            {
                OnConsumoExcesivoDetectado(new EnergiaEventArgs { ConsumoActual = consumoActual, Umbral = umbral });
            }
        }
    }

    public class ServicioNotificacion
    {
        public void EnviarAdvertencia(object sender, EnergiaEventArgs e)
        {
            Console.WriteLine("[ServicioNotificacion] ADVERTENCIA: Consumo excesivo detectado! Consumo: " + e.ConsumoActual + " kW, Umbral: " + e.Umbral + " kW");
        }
    }

    public class ServicioAjusteAutomatizado
    {
        public void AjustarDispositivos(object sender, EnergiaEventArgs e)
        {
            Console.WriteLine("[ServicioAjusteAutomatizado] Ajustando dispositivos para reducir consumo porque excede el umbral de " + e.Umbral + " kW");
        }
    }

    public static class EjercicioCinco
    {
        public static void Ejecutar()
        {
            var monitor = new MonitorEnergia(100.0);
            var notificacion = new ServicioNotificacion();
            var ajuste = new ServicioAjusteAutomatizado();
            monitor.ConsumoExcesivoDetectado += notificacion.EnviarAdvertencia;
            monitor.ConsumoExcesivoDetectado += ajuste.AjustarDispositivos;
            monitor.RegistrarConsumo(85.5);
            monitor.RegistrarConsumo(120.3);
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
            EjercicioUno.Ejecutar();
            Console.WriteLine("");

            Console.WriteLine("Ejercicio 2");
            EjercicioDos.Ejecutar();
            Console.WriteLine("");

            Console.WriteLine("Ejercicio 3");
            EjercicioTres.Ejecutar();
            Console.WriteLine("");

            Console.WriteLine("Ejercicio 4");
            EjercicioCuatro.Ejecutar();
            Console.WriteLine("");

            Console.WriteLine("Ejercicio 5");
            EjercicioCinco.Ejecutar();
            Console.WriteLine("");
        }
    }
}
