using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using Salvis.Entities;
using Salvis.Framework.Engine;
using Salvis.Framework.Helpers;
using Salvis.Framework.Security;

namespace Salvis.Framework.Services
{
    public abstract class GoalService<T> : IGoalService<T> where T : class
    {
        
        private IGoalService<T> _service;

        protected GoalService()
        {
            
        }

        protected GoalService(IGoalService<T> service)
        {
            _service = service;
        }

        /// <summary>
        /// Indicates the lenght of an Unique Code for every Goal.
        /// </summary>
        protected internal const Int32 CODE_LENGHT = 35;

        public abstract void Dispose();

        /// <summary>
        /// Adds an entity with all the validations
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract T Add(T item);

        /// <summary>
        /// Adds an entity without pre-validations, creating and storing his operations and notifications.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="timeInterval"></param>
        /// <returns></returns>
        public abstract T Add(T item, TimeInterval timeInterval);

        public abstract T GetByCode(string code);

        public abstract IEnumerable<T> GetByUserId(string userId);

        public abstract void Delete(T item);
        public abstract bool DeleteByCode(string code);

        public abstract void Update(T item);

        public abstract T Get(int id);
        public abstract T Get(long id);
        public abstract IEnumerable<T> Get();

        public dynamic BuilderGraphic(Goal goal, bool realValue = false, int margin = 4, int max = 9)
        {
            if (goal == null) throw new ArgumentNullException("goal");
            if (goal.OperationDetails == null) throw new ArgumentNullException("goal", "Property goal.OperationDetails can't be Null.");

            var operationDetails = goal.OperationDetails;
            var nextOperation = NextExpectedOperation(goal);

            //Se extrae los montos y su fecha de pago ordenado por fecha, por lo que el (index + 1) representa le numero de cuota.
            var value = operationDetails.OrderBy(p => p.InputDate)
                     .Select(p => (realValue)
                                  ? new { p.InputDate, Value = (p.RealValue.HasValue) ? p.RealValue.Value : 0.0 }
                                  : new { p.InputDate, Value = p.ExpValue }).ToList();

            var axies = new List<string[]>(); //Contiene los puntos (x,y) => (cuota, $monto).
            var yaxisSub = new List<string>(); //Contiene los fechas, donde coinside sus indexes con los de la cuota.

            //[Index Ultima operación esperada] = [Index Proxima Operación] - 1
            var lastFee = value.FindIndex(p => p.InputDate == nextOperation.NextDate) - 1;

            var valuePrevious = 0.0;
            //Determinar desde que punto comienza muestra los datos, teniendo el cuenta el marge y el numero maximo.
            var indexToBegin = lastFee <= margin ? 0 : lastFee - margin;

            //En caso que [value] es menor del valor por defecto de [MAX]
            max = value.Count >= max ? max : value.Count - 1;

            for (var i = indexToBegin; i < max; i++)
            {
                var counterFeet = i + 1;

                var amoung = value[i].Value;
                valuePrevious += (i > 0) ? value[i - 1].Value : 0.0;
                var total = valuePrevious + amoung;

                var dateInput = value[i].InputDate;
                var point = new[] { dateInput.ToString(FormatHelper.DATE_FORMAT_STANDARD),
                    total.ToString(FormatHelper.MONEY_CURRENCY_LETTER) };

                axies.Add(point);
                yaxisSub.Add(counterFeet.ToString(FormatHelper.APP_INT_CURRENCY));
            }

            dynamic graphic = new ExpandoObject();
            graphic.Line = axies;
            graphic.YaxisSub = yaxisSub;
            graphic.NextOperation = nextOperation;
            return graphic;
        }
              
        public OperationExpected NextExpectedOperation(Goal goal)
        {
            return GoalEngine.GetOperationExpected(goal);
        }

        public virtual IServiceResult Validate(DateTime? startDate, DateTime? endDate,
                                        double? partAmount,
                                        double? fullAmount,
                                        TimeInterval timeInterval = TimeInterval.Monthly)
        {
            if (!startDate.HasValue)
                startDate = DateTime.Now;
            return Validate(startDate.Value, endDate, partAmount, fullAmount, timeInterval);
        }

        public virtual IServiceResult Validate(DateTime startDate, DateTime? endDate,
                                        double? partAmount,
                                        double? fullAmount,
                                        TimeInterval timeInterval)
        {
            var result = FieldValidations(startDate, endDate, partAmount, ref fullAmount, timeInterval);

            if (result.Errors.Any())
                return new ServiceResult(null, result.Errors);

            var entity = result.Result as Goal;
            entity.OperationDetails = GoalEngine.Create(entity, timeInterval);

            return new ServiceResult(entity, result.Errors);
        }

        internal String CreateCode(Goal item)
        {
            //  creates public and unique ID
            return KeyCreator.RequestKey(String.Format("", item.Name, item.StartDate),
                                         CODE_LENGHT, excludeInvalidChars: true,
                                         date: DateTimeOffset.Now.DateTime);
        }

        internal protected IServiceResult FieldValidations(DateTime startDate,
                                                                DateTime? endDate,
                                                                double? partAmount,
                                                                ref double? fullAmount,
                                                                TimeInterval timeInterval)
        {
            var errors = new Collection<String>();

            /*  
             * Escenarios:
             * 
             * 1.- Si no contiene fecha final, calcula plazos y busca fecha final.
             * 2.- Si contiene fecha final y cuotas, buscará plazos y monto final.
             * 3.- Si contiene fecha final y monto final, buscará plazos y cuotas.
             * 4.- Si no contiene Fecha inicial, se inicia a partir de la actual
             */

            if (DateTime.MinValue > startDate)
            {
                errors.Add(String.Format("startDate can't be lower than {0:d}.", DateTime.MinValue));
            }


            if (!partAmount.HasValue && !fullAmount.HasValue)
            {
                errors.Add("partAmount and fullAmount can't be both empty.");
            }
            else
            {
                if ((partAmount.HasValue && partAmount.Value <= 0) && (fullAmount.HasValue && fullAmount.Value <= 0))
                {
                    errors.Add("partAmount and fullAmount can't be both empty.");
                }
            }

            if (!endDate.HasValue && (!partAmount.HasValue || partAmount.Value <= 0f))
            {
                errors.Add("endDate and partAmount can't be both invalid.");
            }

            if (errors.Any())
            {
                return new ServiceResult(null, errors);
            }

            if (!Enum.IsDefined(typeof(TimeInterval), timeInterval))
            {
                errors.Add("timeInterval doesn't exists.");
            }

            if (timeInterval < TimeInterval.Dialy)
            {
                timeInterval = TimeInterval.Monthly;
            }


            var shares = 0;

            if (endDate.HasValue)
            {
                if (!(endDate > startDate))
                {
                    errors.Add("startDate can't be greater than endDate.");
                }
            }
            else
            {
                if (fullAmount.HasValue && partAmount.HasValue && partAmount.Value > 0)
                {
                    var r = fullAmount.Value / partAmount.Value;
                    endDate = startDate.AddDays(r * (int)timeInterval);

                    if (shares == 0)    // well, if we're here...
                        shares = Convert.ToInt32(r);
                }
            }

            if (shares == 0)
                shares = DateHelper.GetIntervalsInTime(startDate, endDate.Value, (int)timeInterval);

            if (shares <= 0)
            {
                errors.Add("shares/fees can't be lower than zero.");
            }

            if (!fullAmount.HasValue)
                fullAmount = partAmount * shares;

            var result = new Goal
            {
                StartDate = startDate,
                EndDate = endDate.Value,
                Amount = fullAmount.Value
            };

            return new ServiceResult(result, errors);
        }

    }
}
