using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace  Salvis.App.Web.Models
{
    public class MonthlyReportModel : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }

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

            return this.Amount >= 1 && !String.IsNullOrWhiteSpace(this.Name) && !String.IsNullOrWhiteSpace(this.Description);
        } 
        
    }
}