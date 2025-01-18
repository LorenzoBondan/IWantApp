namespace IWantApp.DTOs;

public interface BaseConverter<E, D>
{
    D ToDto(E entity);
    E ToEntity(D dto);
}
