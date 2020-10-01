namespace PetClinic.Application.Common.Mapping
{
    using AutoMapper;

    // needs to be registered in the mapping profile config
    public interface IMapTo<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(this.GetType(), typeof(T));
    }
}
