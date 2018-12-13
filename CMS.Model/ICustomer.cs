using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CMS.Model
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }

    public class Customer : BaseEntity,ICustomer
    {
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public List<Note> Notes { get; set; }
    }


    public interface ICustomer
    {
        Guid Id { get; set; }
        Status Status { get; set; }
        DateTime CreateDate { get; set; }
        Contact Contact { get; set; }
        List<Note> Notes { get; set; }
    }

    public enum Status
    {
        PROSPECTIVE,
        CURRENT,
        NONACTIVE
    }

    public interface IContact
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
    }

    public class Contact:BaseEntity, IContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public interface IAddress
    {
        Guid Id { get; set; }
        string Line1 { get; set; }
        string Line2 { get; set; }
        string City { get; set; }
        string Country { get; set; }
    }

    public class Address:BaseEntity, IAddress
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public interface INote 
    {
        Guid Id { get; set; }
        string Content { get; set; }
    }

    public class Note:BaseEntity, INote
    {
        public string Content { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
