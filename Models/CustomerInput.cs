using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models;
public static class CustomerInput
{
    public class Create
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }

    public class CreateValidator : AbstractValidator<Create> {
        public CreateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }


    public class IdOnly
    {
        public int Id { get; set; }
    }
}
