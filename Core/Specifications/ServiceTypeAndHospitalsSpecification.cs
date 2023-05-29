using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ServiceTypeAndHospitalsSpecification : BaseSpecification<Service>
    {
        public ServiceTypeAndHospitalsSpecification()
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
        }

        public ServiceTypeAndHospitalsSpecification(int id) 
        : base(x => x.Id == id)
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
        }
    }
}
