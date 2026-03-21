using System;
//este es el bueno 
class Program
{
    static int totalEvaluados = 0;
    static int publicados = 0;
    static int rechazados = 0;
    static int revision = 0;

    static int impactoAlto = 0;
    static int impactoMedio = 0;
    static int impactoBajo = 0;

    static void Main()
    {
        int op;

        do
        {
            Console.WriteLine("\nTreaming");
            Console.WriteLine("1. Evaluar nuevo contenido");
            Console.WriteLine("2. Mostrar reglas del sistema");
            Console.WriteLine("3. Mostrar estadísticas");
            Console.WriteLine("4. Reiniciar estadísticas");
            Console.WriteLine("5. Salir");

            Console.Write("Seleccione opción: ");
            op = Convert.ToInt32(Console.ReadLine());

            switch (op)
            {
                case 1:
                    Evaluar();
                    break;

                case 2:
                    Mostrar();
                    break;

                case 3:
                    MostrarEstadisticas();
                    break;

                case 4:
                    ReiniciarEstadisticas();
                    break;

                case 5:
                    Console.WriteLine("Saliendo del sistema...");
                    MostrarEstadisticas();
                    break;

                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }

        } while (op != 5);
    }

    static void Evaluar()
    {
        string tipo;
        int duracion;
        string clasificacion;
        int hora;
        string produccion;

        Console.Write("Tipo (pelicula/serie/documental/evento): ");
        tipo = Console.ReadLine().ToLower();

        Console.Write("Duración en minutos: ");
        duracion = Convert.ToInt32(Console.ReadLine());

        Console.Write("Clasificación (tp/+13/+18): ");
        clasificacion = Console.ReadLine().ToLower();

        Console.Write("Hora programada (0-23): ");
        hora = Convert.ToInt32(Console.ReadLine());

        Console.Write("Producción (bajo/medio/alto): ");
        produccion = Console.ReadLine().ToLower();

        bool valido = true;
        string razon = "";

        if (tipo == "pelicula")
        {
            if (duracion < 60 || duracion > 180)
            {
                valido = false;
                razon = "Duración inválida para película";
            }
        }
        else if (tipo == "serie")
        {
            if (duracion < 20 || duracion > 90)
            {
                valido = false;
                razon = "Duración inválida para serie";
            }
        }
        else if (tipo == "documental")
        {
            if (duracion < 30 || duracion > 120)
            {
                valido = false;
                razon = "Duración inválida para documental";
            }
        }
        else if (tipo == "evento")
        {
            if (duracion < 30 || duracion > 240)
            {
                valido = false;
                razon = "Duración inválida para evento";
            }
        }
        if (valido)
        {
            if (clasificacion == "+13")
            {
                if (hora < 6 || hora > 22)
                {
                    valido = false;
                    razon = "Horario no permitido para +13";
                }
            }
            else if (clasificacion == "+18")
            {
                if (!(hora >= 22 || hora <= 5))
                {
                    valido = false;
                    razon = "Horario no permitido para +18";
                }
            }
        }

        if (valido)
        {
            if (produccion == "bajo")
            {
                if (clasificacion == "+18")
                {
                    valido = false;
                    razon = "Producción baja no permitida para +18";
                }
            }
        }

        totalEvaluados++;

        if (!valido)
        {
            Console.WriteLine("Desicion: Rechazar");
            Console.WriteLine("Razón: " + razon);
            rechazados++;
            return;
        }

        string impacto = "Bajo";

        if (produccion == "alto" || duracion > 120 || (hora >= 20 && hora <= 23))
        {
            impacto = "Alto";
            impactoAlto++;
        }
        else if (produccion == "medio" || (duracion >= 60 && duracion <= 120))
        {
            impacto = "Medio";
            impactoMedio++;
        }
        else
        {
            impacto = "Bajo";
            impactoBajo++;
        }

        if (impacto == "Alto")
        {
            Console.WriteLine("Desicion: Enviar a resultados");
            revision++;
        }
        else
        {
            Console.WriteLine("Decisión: Publicar");
            publicados++;
        }

        Console.WriteLine("Impacto: " + impacto);
    }

    static void Mostrar()
    {
        Console.WriteLine("\nReglas");

        Console.WriteLine("\nDuración:");
        Console.WriteLine("Película 60-180");
        Console.WriteLine("Serie 20-90");
        Console.WriteLine("Documental 30-120");
        Console.WriteLine("Evento 30-240");

        Console.WriteLine("\nClasificación:");
        Console.WriteLine("Todo público: cualquier hora");
        Console.WriteLine("+13: 6-22");
        Console.WriteLine("+18: 22-5");

        Console.WriteLine("\nProducción baja solo TP o +13");
    }

    static void MostrarEstadisticas()
    {
        Console.WriteLine("\nEstadísticas");

        Console.WriteLine("Total evaluados: " + totalEvaluados);
        Console.WriteLine("Publicados: " + publicados);
        Console.WriteLine("Rechazados: " + rechazados);
        Console.WriteLine("En revisión: " + revision);

        int aprobados = publicados + revision;

        if (totalEvaluados > 0)
        {
            double porcentaje = (aprobados * 100.0) / totalEvaluados;
            Console.WriteLine("Porcentaje de aprobación: " + porcentaje + "%");
        }

        string impactoPredominante = "Bajo";

        if (impactoAlto > impactoMedio && impactoAlto > impactoBajo)
            impactoPredominante = "Alto";
        else if (impactoMedio > impactoBajo)
            impactoPredominante = "Medio";

        Console.WriteLine("Impacto predominante: " + impactoPredominante);
    }

    static void ReiniciarEstadisticas()
    {
        totalEvaluados = 0;
        publicados = 0;
        rechazados = 0;
        revision = 0;

        impactoAlto = 0;
        impactoMedio = 0;
        impactoBajo = 0;

        Console.WriteLine("Estadísticas reiniciadas :)");
    }
}