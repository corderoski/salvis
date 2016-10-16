using System;

namespace Salvis.Entities
{
    public class OperationExpected
    {
        private double _expAmount;

        public OperationExpected()
        {
            NextDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
        }

        /// <summary>
        /// Indicates when the operation must be done. Can be Null if it's a last operation or the Goal is expired.
        /// </summary>
        public DateTime NextDate { get; set; }

        /// <summary>
        /// Expected amount. Could be different from the RealAmount for balancing.
        /// Pending amount + operation_amount
        /// </summary>
        public Double ExpAmount
        {
            //  non-payed plus next expected
            get { return _expAmount + RealAmount; }
            set { _expAmount = value; }
        }

        /// <summary>
        /// Real amount proyected / expected.
        /// </summary>
        public Double RealAmount { get; set; }
    }
}
