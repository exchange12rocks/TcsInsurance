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
            }).CreateMapper();
        }
        public T2 Map<T1, T2>(T1 source)
        {
            return this.mapper.Map<T1, T2>(source);
        }
    }
}