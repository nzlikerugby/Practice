using CMS.Model;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Infrastructure
{
    public class Services
    {
        private readonly IRepository<Customer> repoCustomer;
        private readonly IRepository<Address> repoAddress;
        private readonly IRepository<Note> repoNote;
        private readonly IRepository<Contact> repoContact;
        public Services(IRepository<Customer> repoCustomer, IRepository<Address> repoAddress, IRepository<Note> repoNote, IRepository<Contact> repoContact)
        {
            this.repoCustomer = repoCustomer;
            this.repoAddress = repoAddress;
            this.repoNote = repoNote;
            this.repoContact = repoContact;
        }

        public List<Customer> Search(string keyword)
        {
            var contacts = repoContact.Filter(keyword).Select(p => p.CustomerId);
            var addresses = repoAddress.Filter(keyword).Select(p => p.CustomerId);
            var customerIds = contacts.Concat(addresses).ToList();
            return repoCustomer.GetAll().Where(p => customerIds.Contains(p.Id)).ToList();
        }
        #region CustomerServices
        public Customer GetCustomer(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException();
            }
            return repoCustomer.Get(Id);
        }

        public Customer GetCustomerDetails(Guid Id)
        {
            var customer = repoCustomer.Get(Id);
            if (customer == null)
            {
                throw new NullReferenceException();
            }
            customer.Address = repoAddress.GetFromFK(Id, "CustomerId");
            customer.Contact = repoContact.GetFromFK(Id, "CustomerId");
            return customer;
        }

        public void EditCustomer(Guid Id,Customer customer,Contact contact,Address address)
        {
            repoCustomer.Update(customer);

            contact.Id = repoContact.GetFromFK(Id, "CustomerId").Id;
            contact.CustomerId = Id;
            repoContact.Update(contact);

            address.Id = repoAddress.GetFromFK(Id, "CustomerId").Id;
            address.CustomerId = Id;
            repoAddress.Update(address);
        }

        public List<Customer> GetCustomers()
        {
            return repoCustomer.GetAll();
        }

        public void UpdateCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException();
            }
            repoCustomer.Update(customer);
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException();
            }
            repoCustomer.Add(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException();
            }
            repoCustomer.Remove(customer);
        }
        #endregion

        #region AddressServices
        public Address GetAddress(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException();
            }
            return repoAddress.Get(Id);
        }

        public List<Address> GetAddresses()
        {
            return repoAddress.GetAll();
        }

        public void UpdateAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException();
            }
            repoAddress.Update(address);
        }

        public void AddAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException();
            }
            repoAddress.Add(address);
        }
        #endregion

        #region NoteServices
        public Note GetNote(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException();
            }
            return repoNote.Get(Id);
        }

        public List<Note> GetNotes()
        {
            return repoNote.GetAll();
        }

        public void UpdateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException();
            }
            repoNote.Update(note);
        }

        public void AddNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException();
            }
            repoNote.Add(note);
        }

        public Note CreateNote(string Id,string content)
        {
            var note = new Note();
            note.Id = Guid.NewGuid();
            note.CustomerId = Guid.Parse(Id);
            note.Content = content;
            if (!string.IsNullOrEmpty(content))
            {
                AddNote(note);
            }
            return note;
        }
        #endregion
    }
}
