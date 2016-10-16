using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Salvis.Entities;

namespace Salvis.App.Web.Models
{

    public class GoalModel : IModel
    {

        public GoalModel()
        {
            StartDate = DateTime.Now;
            TimeType = TimeInterval.Monthly;
        }

        public string Id { get; set; }
        public GoalEntityType Type { get; set; }
        public TimeInterval TimeType { get; set; }
        public int ReasonId { get; set; }
        public String ReasonText { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        [DefaultValue("")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Amount { get; set; }
        [DefaultValue(0)]
        public double Fee { get; set; }

        public IEnumerable<OperationNotificationModel> Notifications { get; set; }

        //public DateTime GetStartingDate()
        //{
        //    return DateTime.Parse(StartDate);
        //}

        //public DateTime GetEndingDate()
        //{
        //    if (string.IsNullOrEmpty(EndingDate))
        //        EndingDate = DateTime.MinValue.ToShortDateString();
        //    return DateTime.Parse(EndingDate);
        //}

        /// <summary>
        /// Checks if it's a valid model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true if valid, otherwise, false.</returns>
        public bool IsValid()
        {
            /*
             * Las comprobaciones deben ser de tal manera, que se unan todos los TRUE
             */
            var dates = false;
            if (System.Data.SqlTypes.SqlDateTime.MinValue.Value < this.EndDate)
                dates = this.StartDate < this.EndDate;
            return this.Amount >= 1 && dates;
        }

        /// <summary>
        /// Checks if it's a valid model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true if valid, otherwise, false.</returns>
        public static bool IsValidRecurrent(GoalModel model)
        {
            /*
             * Las comprobaciones deben ser de tal manera, que se unan todos los TRUE
             */
            var dates = System.Data.SqlTypes.SqlDateTime.MinValue.Value < model.StartDate;
            return model.Amount >= 1 && dates && Enum.IsDefined(typeof(TimeInterval), model.TimeType);
        }

    }

}