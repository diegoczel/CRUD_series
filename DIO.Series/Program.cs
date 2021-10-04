using System;
using DIO.Series.Classes;
using DIO.Series.Enumerador;
namespace DIO.Series
{
    class Program
    {
        private static SerieRepository repositorio = new SerieRepository();
        static void Main(string[] args)
        {
            string opcao = "";
            CreateDados();
            
            do
            {
                opcao = ShowMenu();
                switch (opcao)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    // Console.Clear() em git bash não funciona
                    case "C":
                        Console.Clear();
                        break;
                    case "X":
                        Console.WriteLine();
                        Console.WriteLine("Finalizando aplicação!!!");
                        Console.WriteLine("Até logo!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } while(opcao != "X");
        }

        private static void ListarSeries()
        {

            if (VerificarLen() == false) 
            { 
                Console.WriteLine("Por isso não se pode listar as séries!");
                return; 
            }

            Console.WriteLine("Listar séries!");

            var lista = repositorio.Lista();

            foreach (var serie in lista)      
            {
                Console.WriteLine($"#ID {serie.GetId()}: - {serie.GetTitulo()}{(serie.GetExcluido() ? " - ***Excluido***" : "")}");
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série.");

            /*ShowEnum();

            Console.Write("Digite o gênero entre as opções acima: ");
            int genero = int.Parse(Console.ReadLine());*/

            Genero genero;
            string titulo;
            int ano;
            string descricao;

            PedirDadosSerie(out genero, out titulo, out descricao, out ano);

            Serie serie = new Serie(
                repositorio.ProximoId(),
                genero,
                titulo,
                descricao,
                ano
            );

            repositorio.Insere(serie);
        }

        public static void AtualizarSerie()
        {
            if (VerificarLen() == false) 
            { 
                Console.WriteLine("Por isso não se pode Atualizar série!");
                return; 
            }

            int id = ValidarInputId();

            /*ShowEnum();

            Console.Write("Digite o gênero entre as opções acima: ");
            int genero = int.Parse(Console.ReadLine());*/
            Genero genero;
            string titulo;
            int ano;
            string descricao;

            PedirDadosSerie(out genero, out titulo, out descricao, out ano);

            Serie serie = new Serie(
                id,
                genero,
                titulo,
                descricao,
                ano
            );

            repositorio.Atualiza(id, serie);
        }

        private static void ExcluirSerie()
        {
            if (VerificarLen() == false) 
            { 
                Console.WriteLine("Por isso não se pode excluir série!");
                return; 
            }

            int id = ValidarInputId();

            Console.WriteLine("Digite S para realmente excluir esta série");
            string opcaoExcluir = Console.ReadLine().ToUpper();

            if (opcaoExcluir == "S")
            {
                repositorio.Exclui(id);
                Console.WriteLine("Série excluida!");
            }
            else
            {
                Console.WriteLine("Série não excluida");
            }
            
        }

        private static void VisualizarSerie()
        {
            if (VerificarLen() == false) 
            { 
                Console.WriteLine("Por isso não pode vizualizar série!");
                return; 
            }

            int id = ValidarInputId();

            var serie = repositorio.RetornaPorId(id);

            Console.WriteLine("Vizualizar série!");
            Console.WriteLine(serie);
        }

        // verificar se SerieRepository.listaSerie esta vazia
        // se vazia, não pode efetuar operações como:
        // editar, vizualizar...
        private static bool VerificarLen()
        {
            // false não pode fazer operação
            // true pode
            if (repositorio.ProximoId() == 0) 
            { 
                Console.WriteLine("Não existe(m) série(s) cadastrada(s).");
                return false; 
            }
            return true;
        }

        private static bool VerificarId(int id)
        {
            /*
                repositorio.ProximoId() ==  0
                    não existe elemento na lista
                id < 0
                    indice negativo não pode
                id > repositorio.ProximoId()
                    indice maior que o ultimo da lista
            */
            if (
                repositorio.ProximoId() ==  0 ||
                id < 0 ||
                id >= repositorio.ProximoId()
            )
            {
                return false;
            }
            return true;
        }

        private static int ValidarInputId()
        {
            Console.WriteLine("Digite o id da série.");
            string idInput = Console.ReadLine();
            int id;
            
            while (true)
            {
                // verificar se é um int
                if (int.TryParse(idInput, out id))
                {
                    // id fora do range permitido
                    if (VerificarId(id) == false)
                    {
                        Console.WriteLine($"ID {id}, fora do range permitido");
                    }
                    else
                    {
                        return id;
                    }
                }
                else
                {
                    Console.WriteLine($"ID {id}, é inválido, pois não é um numero");
                }
                Console.WriteLine("Digite um id da série.");
                idInput = Console.ReadLine();
            }
        }

        private static string ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries.");
            Console.WriteLine("2- Inserir nova série.");
            Console.WriteLine("3- Atualizar série.");
            Console.WriteLine("4- Excluir série.");
            Console.WriteLine("5- Vizualizar série.");
            Console.WriteLine("C- Limpar tela.");
            Console.WriteLine("X- Sair.");
            Console.WriteLine();

            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }
    
        private static void CreateDados()
        {
            Serie s1 = new Serie(
                repositorio.ProximoId(),
                Genero.Fantasia,
                "The magicians",
                "Mistura de mundos",
                2015
            );
            repositorio.Insere(s1);

            Serie s2 = new Serie(
                repositorio.ProximoId(),
                Genero.Fantasia,
                "Game of Thrones",
                "putaria e morte",
                2015
            );
            repositorio.Insere(s2);

            Serie s3 = new Serie(
                repositorio.ProximoId(),
                Genero.Suspense,
                "Dexter",
                "Serial killer",
                2015
            );
            repositorio.Insere(s3);
        }
    
        private static void ShowEnum()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
            }
        }

        private static Genero ValidarGenero()
        {
            /*var arrayGenero = Enum.GetValues<Genero>();
            var v = Enum.GetValues(typeof(Genero));
            Console.WriteLine(Enum.IsDefined<Genero>((Genero)50));
            Console.WriteLine(Enum.IsDefined<Genero>((Genero)1));
            Console.WriteLine(arrayGenero[0]);*/
            
            ShowEnum();
            Console.Write("Digite o gênero entre as opções acima: ");
            string generoInput = Console.ReadLine();
            int genero;

            while (true)
            {
                if (int.TryParse(generoInput, out genero))
                {
                    if (!Enum.IsDefined<Genero>((Genero)genero))
                    {
                        Console.WriteLine("Gênero informado fora do range.");
                    }
                    else
                    {
                        return (Genero)genero;
                    }
                }
                else
                {
                    Console.WriteLine("Gênero informado invalido, pois não é um inteiro");
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                generoInput = Console.ReadLine();
            }

        }

        private static void PedirDadosSerie(out Genero genero, out string titulo, out string descricao, out int ano)
        {
            genero = ValidarGenero();
            
            Console.WriteLine("Digite o titulo da série: ");
            titulo = Console.ReadLine();

            Console.Write("Digite o ano de inicio da série: ");
            ano = int.Parse(Console.ReadLine());

            Console.Write("Digite a descrição da série: ");
            descricao = Console.ReadLine();
        }
        
    }
}
