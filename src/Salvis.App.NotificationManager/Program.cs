using System;
using System.Threading.Tasks;
using Autofac;
using System.Linq;
using Salvis.App.NotificationManager.Dispatchers;
using Salvis.App.NotificationManager.Models;
using Salvis.App.NotificationManager.Utils;
using Salvis.Entities;
using Salvis.Framework.Helpers;
using Salvis.Framework.Services;

namespace Salvis.App.NotificationManager
{

    class Program
    {

        private static readonly ILifetimeScope Scope;

        private static readonly INotificationBoardService NotificacionBoardService;
        private static readonly INotificationService NotificacionService;

        private static readonly IGoalService<Debt> GoalDebtService;
        private static readonly IGoalService<Saving> GoalSavingService;
        private static readonly IGoalService<Recurrent> GoalRecurrentService;

        private static readonly IOperationService OperationService;

        private static readonly IUserService UserService;

        private static readonly Dispatchers.Dispatcher Dispatcher;

        private static readonly NotificationConverter NotificationConverter;

        private static bool _keep;

        private const int MinutesInterval = 5;

        static Program()
        {
            Scope = CompositionRoot.GetBuilder.BeginLifetimeScope();
            Dispatcher = new Dispatcher();
            NotificationConverter = new NotificationConverter();
            //  Services
            NotificacionBoardService = Scope.Resolve<INotificationBoardService>();
            NotificacionService = Scope.Resolve<INotificationService>();

            GoalDebtService = Scope.Resolve<IGoalService<Debt>>();
            GoalSavingService = Scope.Resolve<IGoalService<Saving>>();
            GoalRecurrentService = Scope.Resolve<IGoalService<Recurrent>>();

            UserService = Scope.Resolve<IUserService>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting {0}...", APP_NAME);
            /*
                Algoritmo:  Mediante un proceso automático, buscar Notificaciones para un espacio en tiempo y enviar las mismas.
                Objetivo:   Buscar Notificaciones a ser enviadas a sus destinos.
                1.- Activar proceso Batch (Intervalo de ejecución = Cada 60 minutos)
                2.- Esperar por Ejecución
                3.- Buscar notificaciones que coincidan para ese Tiempo del día
                4.- Buscar parientes de las notificaciones
                5.- Mediante los parientes determinar la operación (nombre, finalidad, monto)
                6.- Insertar notificación en vía / medio de envío (e-mail, SMS, etc.) 
                7.- 
             */
            var worker = new Worker();

            Console.CancelKeyPress += (sender, eventArgs) => Console_CancelKeyPress(worker, null);

            Engine_SendMessage("Loading {0}...", APP_NAME);

            MainOperation();

            //Proceso comentado hasta determinado el flujo seguro y continuo.
            //_keep = false;
            //while (_keep)
            //{
            //    if (worker.IsWorking)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        Engine_SendMessage("- # Panel de Opciones {0} # -", APP_NAME);
            //        Engine_SendMessage("{0}    ***************************************************     {0}", Environment.NewLine);
            //        Engine_SendMessage("-e | Salir", APP_NAME);
            //        Engine_SendMessage("-s | Iniciar / Reanudar Proceso", APP_NAME);
            //        Engine_SendMessage("-d | Detener Proceso", APP_NAME);

            //        var sys = Console.ReadLine();
            //        switch (sys)
            //        {
            //            case "e":
            //                Engine_Exit(worker);
            //                break;
            //            case "s":
            //                Engine_Start(worker);
            //                continue;
            //            case "d":
            //                Engine_Stop(worker);
            //                continue;
            //            default:
            //                Engine_SendMessage("¡Instrucción desconocida!");
            //                Engine_SendMessage("Cargando Panel de Opciones..." + Environment.NewLine + Environment.NewLine);
            //                continue;
            //        }
            //    }

            //    //--Task.Delay((int)TimeSpan.FromMinutes(99).TotalMilliseconds).Wait();
            //}
            Engine_SendMessage("Exiting {0}...", APP_NAME);
            Console.Read();
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            var worker = sender as Worker;
            Engine_Stop(worker);
        }

        private static void Engine_SendMessage(string message, params object[] values)
        {
            Console.WriteLine(message, values);
        }

        private static void Engine_Start(Worker worker)
        {
            worker.Start(MainOperation, null);
            _keep = true;
        }

        private static void Engine_Stop(Worker worker)
        {
            Engine_SendMessage("Deteniendo sistema por instrucción CONS.CanKey_ESC | {0}", DateTimeOffset.Now);
            worker.Stop();
            Engine_SendMessage("El sistema ha sido detenido.");
        }

        private static void Engine_Exit(Worker worker)
        {
            Engine_SendMessage("Finalizando sistema por instrucción CONS.CanKey_ESC | {0}", DateTimeOffset.Now);
            worker.Stop();
            _keep = false;
            Engine_SendMessage("El sistema ha sido cerrado.");
        }

        private static void MainOperation()
        {
            //  init
            var lastExec = NotificacionBoardService.GetLastExecution(); //  ¿for what?

            var exec = NotificacionBoardService.GetNewExecution(null);  //  Creates and starts based on the actual Time
            var notifications = NotificacionService.GetByDate(exec.StartDate);

            // retrieve entities
            var debts = notifications.Where(n => n.ParentTypeId == (int)GoalEntityType.Debt).Select(i => GoalDebtService.Get(i.ParentId));
            var savings = notifications.Where(n => n.ParentTypeId == (int)GoalEntityType.Saving).Select(i => GoalSavingService.Get(i.ParentId));
            var recurrents = notifications.Where(n => n.ParentTypeId == (int)GoalEntityType.Recurrent).Select(i => GoalRecurrentService.Get(i.ParentId));

            var users = UserService.GetUsersDeliveryInformation(notifications.Select(i => i.UserId));
            //  retrieve formatted messages and adapt them...
            var messages = NotificationConverter.Transform(notifications, users);
            var sms = messages.Where(m => m.Types.Contains(MessageType.SMS));
            var push = messages.Where(m => m.Types.Contains(MessageType.Push));
            var email = messages.Where(m => m.Types.Contains(MessageType.Email));

            Dispatcher.SendEMAIL(email);
            Dispatcher.SendMessage(email);
            Dispatcher.SendPush(push);
            Dispatcher.SendSMS(sms);

            //  Closing NotificationBoard
            exec.ItemsCount = notifications.Count();
            exec.Duration = (int)(DateTimeOffset.Now.DateTime - exec.StartDate).TotalSeconds;
            NotificacionBoardService.CloseExecution(exec);
        }

        internal const String APP_NAME = "Salvis.App.NotificationManager vDev";

    }
}
