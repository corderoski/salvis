using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Salvis.Entities
{

    public class Catalog
    {
        [Key]
        [Column(Order = 1)]
        public string Category { get; set; }

        [Key]
        [Column(Order = 2)]
        public int SubCategoryId { get; set; }

        public string Value { get; set; }
        public bool Enable { get; set; }
        public string DescriptionES { get; set; }
        public string DescriptionEN { get; set; }



        public const String PARENT_TYPE = "PARNT_TYPE";
        public const String PERIOD_INTERAL = "PERD_INTVL";
        public const String SAVING_TYPE = "SAVE_TYPE";
        public const String DEBT_TYPE = "DEBT_TYPE";
        public const String RECURRENT_TYPE = "RCRNT_TYPE";
        public const String API_PROVIVDER = "API_PROVDR";
        public const String APP_CURRENCY = "APP_CURREN";
    }

    public enum CatalogDescriptionLang
    {
        ES,
        EN
    }

    public enum TimeInterval
    {
        Dialy = 1,
        Weekly = 7,
        BiWeekly = 15,
        Monthly = 30,
        Quarterly = 90,
        Annual = 365
    }

    public enum MessageState
    {
        Unread = 0,
        Read = 1,
        Delete = 9
    }

    public enum GoalEntityType
    {
        Saving = 1,
        Debt = 2,
        Recurrent = 3,
        None = 9
    }

}
