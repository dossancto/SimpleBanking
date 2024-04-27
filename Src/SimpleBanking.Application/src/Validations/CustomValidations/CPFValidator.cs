using FluentValidation;

namespace SimpleBanking.Application.Validations.CustomValidations;

/// <summary>
/// CPF Validations
/// </summary>
public static class CPFValidator
{
    /// <summary>
    /// Validates if the CPF in valid
    /// </summary>
    public static IRuleBuilder<T, string> ValidCPF<T>(this IRuleBuilder<T, string> ruleBuilder)
      => ruleBuilder
      .NotEmpty()
      .Must(IsCpf).WithMessage("Invalid CPF");

    private static bool IsCpf(string cpf)
    {
        if (Repetidos(cpf))
        {
            return false;
        }

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;
        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");
        if (cpf.Length != 11)
            return false;
        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cpf.EndsWith(digito);
    }

    public static bool Repetidos(string nums)
    {
        var firsChar = nums[0];

        return nums.All(x => x == firsChar);
    }

}
