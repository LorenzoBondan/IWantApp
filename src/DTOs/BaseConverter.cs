using IWantApp.DTOs.Base;
using IWantApp.Models.Base;

public interface BaseConverter<E, D> where E : BaseEntity where D : BaseDTO
{
    D ToDto(E entity);
    E ToEntity(D dto);
}
