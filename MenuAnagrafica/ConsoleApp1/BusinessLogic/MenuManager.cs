using MenuAnagrrafica.Models;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
namespace MenuAnagrafica.BusinessLogic
{
    static class MenuManager
    {
        static List<Anagrafica> anagrafiche = new List<Anagrafica>();
       public static void MenuCreate()
        {
            ImpostaColoreConsole();
            StampaLineaAsterischi();
            Console.WriteLine();
            StampaCentrato(" ************* MENU ANAGRAFICA ************* ");
            Console.WriteLine();
            StampaLineaAsterischi();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                StampaCentrato(" ************* DIGITA UNA LETTERA ************* ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                StampaCentrato(" ************* A) Inserimento Anagrafica ************* ");
                Console.WriteLine();
                StampaCentrato(" ************* B) Visualizzazione anagrafiche ************* ");
                Console.WriteLine();
                StampaCentrato(" ************* C) Eliminazione Anagrafiche ************* ");
                Console.WriteLine();
                StampaCentrato(" ************* D) Serializzazione Lista Anagrafiche in Json su documento di testo *************");
                Console.WriteLine();
                StampaCentrato(" ************* E) Carica anagrafiche esistenti da file *************");
                Console.WriteLine();
                StampaCentrato(" ************* F) Chiusura & Uscita Programma ************* ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Seleziona un'opzione: ");
                var choise = Console.ReadLine();
                try
                {
                    switch (choise?.ToUpper())
                    {
                        case "A":
                            InserimentoAnagrafica();
                            break;
                        case "B":
                            VisualizzazioneAnagrafiche();
                            break;
                        case "C":
                            EliminazioneAnagrafica();
                            break;
                        case "D":
                            SerializzaAnagrafiche();
                            break;
                        case "E":
                            DeserializzaAnagrafiche();
                            break;
                        case "F":
                            return;
                        default:
                            Console.WriteLine("Opzione non valida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
                finally 
                { 
                    Console.WriteLine("Il comando è stato eseguito."); 
                }
            }
        }
        static void ImpostaColoreConsole()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                for (int x = 0; x < Console.WindowWidth; x++)
                {
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(0, 0);
        }
        static void StampaLineaAsterischi()
        {
            Console.WriteLine(new string('*', Console.WindowWidth));
        }
        static void StampaCentrato(string testo)
        {
            int spazi = (Console.WindowWidth - testo.Length) / 2;
            Console.WriteLine($"{new string(' ', spazi)}{testo}");
        }
        static void InserimentoAnagrafica()
        {
            var anagrafica = new Anagrafica();
            Console.Write("Inserisci Nome: ");
            anagrafica.Nome = Console.ReadLine();
            Console.Write("Inserisci Cognome: ");
            anagrafica.Cognome = Console.ReadLine();
            int età;
            do
            {
                Console.Write("Inserisci Età (deve essere maggiore di 18): ");
                if (!int.TryParse(Console.ReadLine(), out età) || età <= 18)
                {
                    Console.WriteLine("Età non valida. Devi inserire un numero maggiore di 18.");
                }
            } while (età <= 18);
            anagrafica.Età = età;
            Console.Write("Inserisci Indirizzo: ");
            anagrafica.Indirizzo = Console.ReadLine();
            Console.Write("Inserisci Città: ");
            anagrafica.Città = Console.ReadLine();
            Console.Write("Inserisci Provincia: ");
            anagrafica.Provincia = Console.ReadLine();
            string? capString;
            int cap;
            do
            {
                Console.Write("Inserisci CAP (deve avere 5 numeri): ");
                capString = Console.ReadLine();
                if (capString?.Length == 5 && int.TryParse(capString, out cap))
                {
                    anagrafica.CAP = cap;
                    break;
                }
                else
                {
                    Console.WriteLine("CAP non valido. Deve avere 5 numeri.");
                    cap = 0;
                }
            } while (true);
            anagrafiche.Add(anagrafica);
            Console.WriteLine("Anagrafica inserita con successo!");
        }
        static void VisualizzazioneAnagrafiche()
        {
            foreach (var anagrafica in anagrafiche)
            {
                Console.WriteLine($"Nome: {anagrafica.Nome}, Cognome: {anagrafica.Cognome}, Età: {anagrafica.Età}, Indirizzo: {anagrafica.Indirizzo}, Città: {anagrafica.Città}, Provincia: {anagrafica.Provincia}, CAP: {anagrafica.CAP}");
            }
        }
        static void EliminazioneAnagrafica()
        {
            Console.Write("Inserisci il Nome dell'anagrafica da eliminare: ");
            var nome = Console.ReadLine();
            Console.Write("Inserisci il Cognome dell'anagrafica da eliminare: ");
            var cognome = Console.ReadLine();
            var anagraficaDaEliminare = anagrafiche.FirstOrDefault(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && a.Cognome.Equals(cognome, StringComparison.OrdinalIgnoreCase));
            if (anagraficaDaEliminare != null)
            {
                anagrafiche.Remove(anagraficaDaEliminare);
                Console.WriteLine("Anagrafica eliminata con successo!");
            }
            else
            {
                Console.WriteLine("Anagrafica non trovata.");
            }
        }
        static void SerializzaAnagrafiche()
        {
            var json = JsonConvert.SerializeObject(anagrafiche, Formatting.Indented);
            File.WriteAllText("anagrafiche.json", json);
            Console.WriteLine("Anagrafiche serializzate e salvate in 'anagrafiche.json'");
        }
        static void DeserializzaAnagrafiche()
        {
            try
            {
                var json = File.ReadAllText("anagrafiche.json");
                var deserializzate = JsonConvert.DeserializeObject<List<Anagrafica>>(json);
                if (deserializzate != null && deserializzate.Count > 0)
                {
                    anagrafiche.AddRange(deserializzate);
                    Console.WriteLine("Anagrafiche caricate con successo dal file.");
                }
                else
                {
                    Console.WriteLine("Nessuna anagrafica trovata nel file.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File 'anagrafiche.json' non trovato.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Si è verificato un errore durante la deserializzazione: {ex.Message}");
            }
        }
    }
}