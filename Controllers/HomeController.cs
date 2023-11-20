using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Crea variable int para verificar si existe el contador en la session
        int? ifExistNumber = HttpContext.Session.GetInt32("Count");

        // Evalúa si el contador es null lo cree partiendo en 0
        if (ifExistNumber == null)
        {
            int number = 0;
            // Le asigna a la session un valor de 0
            HttpContext.Session.SetInt32("Count", number);
        }

        return View();
    }

    [HttpPost("random")]
    // [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public IActionResult Random(RandomPass password)
    {
        // Genera un string vacío
        string? newPass = "";

        // Declara variable de tipo int que puede ser null, con el valor del contador de la session
        int? currentNumber = HttpContext.Session.GetInt32("Count");

        // Evalúa si del modelo RandomPass el valor es null o distinto de null genere un nuevo password aleatorio
        if (password.Pass == null || password.Pass != null)
        {
            // Genera una instancia de Método Random()
            Random rand = new Random();

            // Se crea un string con valores alfanumericos
            const string pass = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            // se crea bucle for recorre en el largo solicitado de 14 
            for (int i = 0; i < 14; i++)
            {
                // Se crea un número aleatorio en el largo de la cadena pass
                int randomNum = rand.Next(pass.Length);
                
                // se agrega a string vacío newPass por cada iteración una posición con el valor aleatorio generado por el método Random.
                newPass += pass[randomNum];
            };
        }

        // Evalúa si el numero actual es distinto de null le sume 1 unidad al valor, para que registre el contador en la session
        if (currentNumber != null)
        {
            HttpContext.Session.SetInt32("Count", (int)currentNumber + 1);
        }

        // Se crea una session y se pasa el el valor de la cadena generada para la password
        HttpContext.Session.SetString("Pass", newPass);

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
