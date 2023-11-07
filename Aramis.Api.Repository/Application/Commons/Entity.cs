using Aramis.Api.Repository.Interfaces.Commons;
using System.ComponentModel.DataAnnotations;

namespace Aramis.Api.Repository.Application.Commons
{
    public class Entity : IEntity
    {
        private Guid _id;

        [Key]
        public Guid Id
        {
            get
            {

                if (_id == default || _id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
            }


            set
            {
                _id = value;
            }
        }
    }
}


