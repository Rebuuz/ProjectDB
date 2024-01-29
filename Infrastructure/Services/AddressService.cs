﻿

using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AddressService
{
    private readonly AddressRepo _addressRepo;

    public AddressService(AddressRepo addressRepo)
    {
        _addressRepo = addressRepo;
    }

    /// <summary>
    /// Create a new addresss, if the address already exists, use that one 
    /// </summary>
    /// <param name="streetName"></param>
    /// <param name="city"></param>
    /// <param name="postalCode"></param>
    /// <returns></returns>
    public AddressEntity CreateAddressEntity(string streetName, string city, string postalCode)
    {
        var addressEntity = _addressRepo.GetOne(x => x.StreetName == streetName && x.City == city && x.PostalCode == postalCode);
        addressEntity ??= _addressRepo.Create(new AddressEntity { StreetName = streetName, City = city, PostalCode = postalCode  });

        return addressEntity;
    }

    /// <summary>
    /// Get Address by streetname
    /// </summary>
    /// <param name="streetName"></param>
    /// <returns></returns>
    public AddressEntity GetAddressByStreetName(string streetName)
    {
        var addressEntity = _addressRepo.GetOne(x => x.StreetName == streetName);
        return addressEntity;
    }

    /// <summary>
    /// Get Address by city
    /// </summary>
    /// <param name="city"></param>
    /// <returns></returns>
    public AddressEntity GetAddressByCity(string city)
    {
        var addressEntity = _addressRepo.GetOne(x => x.City == city);
        return addressEntity;
    }

    /// <summary>
    /// Get Address by Postal code
    /// </summary>
    /// <param name="postalCode"></param>
    /// <returns></returns>
    public AddressEntity GetAddressByPostalCode(string postalCode)
    {
        var addressEntity = _addressRepo.GetOne(x => x.PostalCode == postalCode);
        return addressEntity;
    }

    /// <summary>
    /// Get address by Id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AddressEntity GetAddressById(int id)
    {
        var addressEntity = _addressRepo.GetOne(x => x.Id == id);
        return addressEntity;
    }

    /// <summary>
    /// Get all addresses in a list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<AddressEntity> GetAllAddresses()
    {
        var addresses = _addressRepo.GetAll();
        return addresses;
    }

    /// <summary>
    /// Update an address
    /// </summary>
    /// <param name="addressEntity"></param>
    /// <returns></returns>
    public AddressEntity UpdateAddress(AddressEntity addressEntity)
    {
        var updatedAddressEntity = _addressRepo.Update(x => x.Id == addressEntity.Id, addressEntity);
        return updatedAddressEntity;
    }

    /// <summary>
    /// Delete an address
    /// </summary>
    /// <param name="id"></param>
    public void DeleteRole(int id)
    {
        _addressRepo.Delete(x => x.Id == id);
    }
}
