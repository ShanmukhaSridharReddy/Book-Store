using BusinessLayer.InterFace;
using ModelLayer.Model;
using RepositoryLayer.InterFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Sessions
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepo addressRepo;
        public AddressBusiness(IAddressRepo addressRepo)
        {
            this.addressRepo = addressRepo;
        }
        public AddressModel AddAddress(int userId, AddressModel addressModel)
        {
            return addressRepo.AddAddress(userId, addressModel);
        }
        public IEnumerable<AddressModel> GetAddress(int userId)
        {
            return addressRepo.GetAddress(userId);
        }
        public string UpdateAddress(UpdateAddressModel updateAddressModel)
        {
            return addressRepo.UpdateAddress(updateAddressModel);
        }


    }
}
