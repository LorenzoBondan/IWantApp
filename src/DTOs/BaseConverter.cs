using IWantApp.DTOs.Base;
using IWantApp.Models.Base;

public interface BaseConverter<E, D> where E : BaseEntity where D : BaseDTO
{
    E ToEntity(D dto);
    D ToDto(E entity);
}
