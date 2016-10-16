using System;

namespace Salvis.Entities
{
    public interface IGoalEntity<TKey>
    {
        TKey Id { get; set; }
        string Code { get; set; }
        int ReasonTypeId { get; set; }
        string UserId { get; set; }

        Goal Goal { get; set; }
    }
}
