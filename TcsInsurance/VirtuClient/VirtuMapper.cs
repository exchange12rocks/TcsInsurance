using AutoMapper;
using VirtuClient.Models;
namespace VirtuClient
{
    public class VirtuMapper
    {
        private static VirtuMapper instance = null;
        public static VirtuMapper Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new VirtuMapper();
                }
                return instance;
            }
        }
        private IMapper mapper;
        public VirtuMapper()
        {
            this.mapper = new MapperConfiguration((config) =>
            {
                config.CreateMap<GetProductOutput, GetProductResult>();
                config.CreateMap<GetClassifierOutput, GetClassifierResult>();
                config.CreateMap<GetTariffOutput, GetTariffResult>()
                    .ForMember(o => o.InsPeriod, b => b.MapFrom(z => z.InsPeriod.value))
                    .ForMember(o => o.InsSum, b => b.MapFrom(z => z.InsSum.value))
                    .ForMember(o => o.Year, b => b.MapFrom(z => z.Year.value));
            }).CreateMapper();
        }
        public T2 Map<T1, T2>(T1 source)
        {
            return this.mapper.Map<T1, T2>(source);
        }
    }
}