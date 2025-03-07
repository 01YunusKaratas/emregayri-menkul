using System;
using Microsoft.AspNetCore.Identity;

namespace emregayrımenkul.Models;

public class CustomIdentityErrorDescriber :IdentityErrorDescriber
{

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = $"'{email}' başka bir kullanıcı tarafından kullanılmaktadır."
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "Şifreniz en az bir özel karakter içermelidir (!,@,#,$,% vb.)"
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "Şifreniz en az bir rakam içermelidir (0-9)"
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "Şifreniz en az bir küçük harf içermelidir (a-z)"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "Şifreniz en az bir büyük harf içermelidir (A-Z)"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = $"Şifreniz en az {length} karakter uzunluğunda olmalıdır"
        };
    }
}
