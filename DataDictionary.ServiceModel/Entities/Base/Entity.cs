using System.Runtime.Serialization;

namespace DataDictionary.ServiceModel.Entities.Base
{
    [DataContract]
    public abstract class Entity<TId> : EntityBase<TId>
        where TId : struct
    {
    }
}
