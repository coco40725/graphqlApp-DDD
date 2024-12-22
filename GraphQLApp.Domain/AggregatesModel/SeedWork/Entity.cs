namespace GraphQLApp.Domain.AggregatesModel.SeedWork;

public abstract class Entity
{
     private int? _requestedHashCode;
     int _id;

     public virtual int Id
     {
          get
          {
               return _id;
          }
          protected set
          {
               _id = value;
          }
     }
     
     // 判斷是否才剛建立 還沒賦予Id
     private bool IsTransient()
     {
          return this.Id == default;
     }
     
     public override bool Equals(object obj)
     {
          if (obj is not Entity)
          {
               return false;
          }
          if (Object.ReferenceEquals(this, obj))
          {
               return true;
          }
          if (this.GetType() != obj.GetType())
          {
               return false;
          }
          var item = (Entity)obj;
          if (item.IsTransient() || this.IsTransient())
          {
               return false;
          }
          else
          {
               return item.Id == this.Id;
          }
     }
     
     public override int GetHashCode()
     {
          if (!IsTransient())
          {
               if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

               return _requestedHashCode.Value;
          }
          else
               return base.GetHashCode();

     }
}