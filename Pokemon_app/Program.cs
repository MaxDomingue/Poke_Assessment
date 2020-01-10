using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Pokemon_app
{
    class Program
    {
        static void Main(string[] args)
        {
            API_client.InitPokeClient();

            string pokeName;
            Console.WriteLine("Enter the name of the Pokemon you are searching for: ");
            pokeName = Console.ReadLine();

            Pokemon pokemon = GetPokemon(pokeName).Result;
            TypeData[] pokeTypes = GetTypeData(pokemon).Result;

            foreach (TypeData types in pokeTypes)
            {
                Console.WriteLine($"Type: {types.name}");
                foreach (NameData weakToTypes in types.damage_relations.double_damage_from)
                {
                    Console.WriteLine($"Weak To {weakToTypes.name}");
                }

                foreach (NameData strongAgainstTypes in types.damage_relations.double_damage_to)
                {
                    Console.WriteLine($"Strong Against {strongAgainstTypes.name}");
                }
            }

            Console.ReadLine();
        }

        public static class API_client
        {
            public static HttpClient poke_client { get; set; }

            public static void InitPokeClient()
            {
                poke_client = new HttpClient();
                poke_client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

                poke_client.DefaultRequestHeaders.Accept.Clear();
                poke_client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public static async Task<Pokemon> GetPokemon(string pokeName)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokeName}/";
            return await GetApiData<Pokemon>(url);
        }

        public static async Task<TypeData[]> GetTypeData(Pokemon pokemon)
        {
            List<TypeData> typeDataList = new List<TypeData>();
            foreach (PokemonType pokeType in pokemon.types)
            {
                typeDataList.Add(await GetApiData<TypeData>(pokeType.type.url));
            }

            return typeDataList.ToArray();
        }

        public static async Task<T> GetApiData<T>(string url)
        {
            using (HttpResponseMessage response = await API_client.poke_client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public class TypeData
        {
            public DamageRelationships damage_relations { get; set; }
            public string name { get; set; }
        }


        public class DamageRelationships
        {
            public NameData[] double_damage_from { get; set; }
            public NameData[] double_damage_to { get; set; }
            public NameData[] half_damage_from { get; set; }
            public NameData[] half_damage_to { get; set; }
            public NameData[] no_damage_from { get; set; }
            public NameData[] no_damage_to { get; set; }
        }

        public class Pokemon
        {
            public List<PokemonType> types { get; set; }
        }

        public class PokemonType
        {
            public NameData type { get; set; }
        }

        public class NameData
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}
