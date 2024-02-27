using ModelLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.InterFace
{
    public interface IAddressBusiness
    {
        AddressModel AddAddress(int userId, AddressModel addressModel);
        IEnumerable<AddressModel> GetAddress(int userId);
        string UpdateAddress(UpdateAddressModel updateAddressModel);

    }
}