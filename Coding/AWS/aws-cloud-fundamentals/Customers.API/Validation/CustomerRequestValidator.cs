using System.Text.RegularExpressions;
using Customers.Api.Contracts.Requests;
using FluentValidation;

namespace Customers.Api.Validation;

public partial class CustomerRequestValidator : AbstractValidator<CustomerRequest>
{
    public CustomerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .Matches(FullNameRegex());

        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.GitHubUsername)
            .Matches(UsernameRegex());

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now)
            .WithMessage("Your date of birth cannot be in the future");
    }

    private static Regex FullNameRegex()
    {
      return new Regex("^[a-z ,.'-]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    private static Regex UsernameRegex()
    {
      return new Regex("^[a-z\\d](?:[a-z\\d]|-(?=[a-z\\d])){0,38}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class GeneratedRegexAttribute : Attribute
    {
      public GeneratedRegexAttribute(string pattern, RegexOptions options, string culture)
      {
        Pattern = pattern;
        Options = options;
        Culture = culture;
      }

      public string Pattern { get; }
      public RegexOptions Options { get; }
      public string Culture { get; }
    }
}
