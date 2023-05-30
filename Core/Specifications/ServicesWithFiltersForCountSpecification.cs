using Core.Entities;

namespace Core.Specifications
{
    public class ServicesWithFiltersForCountSpecification : BaseSpecification<Service>
    {
        public ServicesWithFiltersForCountSpecification(ServiceSpecParams serviceParams)
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
            ) { }
    }
}
