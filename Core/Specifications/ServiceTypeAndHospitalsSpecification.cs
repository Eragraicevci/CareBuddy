using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ServiceTypeAndHospitalsSpecification : BaseSpecification<Service>
    {
        public ServiceTypeAndHospitalsSpecification(ServiceSpecParams serviceParams)
            : base(
                x =>
                    (
                        string.IsNullOrEmpty(serviceParams.Search)
                        || x.Name.ToLower().Contains(serviceParams.Search)
                    )
                    && (
                        !serviceParams.HospitalId.HasValue
                        || x.HospitalId == serviceParams.HospitalId
                    )
                    && (!serviceParams.TypeId.HasValue || x.ServiceTypeId == serviceParams.TypeId)
            )
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
            AddOrderBy(x => x.Name);
            ApplyPaging(
                serviceParams.PageSize * (serviceParams.PageIndex - 1),
                serviceParams.PageSize
            );

            if (!string.IsNullOrEmpty(serviceParams.Sort))
            {
                switch (serviceParams.Sort)
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

        public ServiceTypeAndHospitalsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ServiceType);
            AddInclude(x => x.Hospital);
        }
    }
}
