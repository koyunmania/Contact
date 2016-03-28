using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Contacts.Models
{
	public class Contact
	{
		public int ID { get; set; }

		//[Required(ErrorMessage = "Please enter contact name!")]
		//[StringLength(50, ErrorMessage = "Name can't be longer than 50 Char!")]
		public string Name { get; set; }

		//[Required(ErrorMessage = "Please enter phone number!")]
		[Display(Name = "Phone")]
		[DataType(DataType.Text)]
		public string PhoneNum { get; set; }

		[EmailAddress(ErrorMessage = "This email adress is not valid!")]
		//[Required(ErrorMessage = "Please enter an email adress!")]
		public string Email { get; set; }
	}
	public class ContactDBContext : DbContext
	{
		public DbSet<Contact> Contacts { get; set; }
	}
}