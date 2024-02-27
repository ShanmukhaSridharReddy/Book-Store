using ModelLayer.Model;
using System.Collections.Generic;

namespace RepositoryLayer.InterFace
{
    public interface IAddressRepo
    {
        AddressModel AddAddress(int userId, AddressModel addressModel);
        IEnumerable<AddressModel> GetAddress(int userId);
        string UpdateAddress(UpdateAddressModel updateAddressModel);

    }
}