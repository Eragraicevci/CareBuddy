using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ServiceTypeAndHospitalsSpecification : BaseSpecification<Service>
    {
        public ServiceTypeAndHospitalsSpecification(string sort, int? hospitalId, int? typeId)
        : base(x =>
           
            (!hospitalId.HasValue || x.HospitalId == hospitalId) &&
            (!typeId.HasValue || x.ServiceTypeId == typeId)
            )
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
            AddOrderBy(x => x.Name);

             if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ServiceTypeAndHospitalsSpecification(int id) 
        : base(x => x.Id == id)
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
        }
    }
}
