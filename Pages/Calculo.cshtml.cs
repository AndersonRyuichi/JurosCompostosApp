using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CalculoModel : PageModel
{
    [BindProperty]
    public decimal ValorInicial { get; set; }

    [BindProperty]
    public decimal ValorMensal { get; set; }

    [BindProperty]
    public decimal TaxaJuros { get; set; }

    [BindProperty]
    public string TipoTaxaJuros { get; set; } = "Mensal";  // Novo campo para o tipo de taxa de juros

    [BindProperty]
    public int Tempo { get; set; }

    [BindProperty]
    public string TipoTempo { get; set; } = "Meses";  // Novo campo para o tipo de tempo

    public decimal ValorFinal { get; private set; }

    public void OnPost()
    {
        // Convertendo os valores de acordo com a escolha do usuário
        int tempoEmMeses = TipoTempo == "Anos" ? Tempo * 12 : Tempo;
        decimal taxaMensal = TipoTaxaJuros == "Anual" ? (TaxaJuros / 100) / 12 : TaxaJuros / 100;

        ValorFinal = CalcularJurosCompostos(ValorInicial, ValorMensal, taxaMensal, tempoEmMeses);
    }

    private decimal CalcularJurosCompostos(decimal valorInicial, decimal valorMensal, decimal taxaMensal, int tempoMeses)
    {
        decimal montante = valorInicial;

        for (int i = 0; i < tempoMeses; i++)
        {
            montante += valorMensal;
            montante += montante * taxaMensal;
        }

        return montante;
    }
}
