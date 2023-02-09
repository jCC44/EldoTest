using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestEldo
{
    internal class FriendsManager
    {
        /// <summary>
        /// Comporte le mapping des amitiés 
        /// </summary>
        Dictionary<string, List<string>> friendshipMap;

        /// <summary>
        /// Comporte les prénoms déjà vérifiés (pour éviter des boucles infinies)
        /// </summary>
        private List<string> firstnameAlreadyChecked;

        /// <summary>
        /// Constructeur et initialisation
        /// </summary>
        /// <param name="friendshipList">Liste textuelle des affinités</param>
        public FriendsManager(List<string> friendshipList)
        {
            this.friendshipMap = new Dictionary<string, List<string>>();
            this.firstnameAlreadyChecked = new List<string>();

            this.sortFriendship(friendshipList);

            var userInput = "";
            Console.WriteLine("Poser votre question:  ");
            while (true)
            {
                userInput = Console.ReadLine();
               
                    if (userInput == "stop")
                    {
                        break;
                    }

                    this.firstnameAlreadyChecked = new List<string>();
                    this.questionTreatment(userInput);
               
            }
        }

        /// <summary>
        /// Extrait les prénoms identifiés par des majuscules. Extrait également le mot moi qui est concidéré comme un prénom
        /// </summary>
        /// <param name="input">Phrase analyser</param>
        /// <returns>Retourne une liste exploitable des prénoms</returns>
        private List<string> searchFirstnames(string input)
        {
            var list = new List<string>();
            MatchCollection firstnamesMatch = Regex.Matches(input, @"(\b[A-Z][a-z]+\b)|(\bmoi\b)");
            list = firstnamesMatch.Select(a => a.Value.ToString()).ToList();

            return list;
        }

        /// <summary>
        /// Créer le mapping des amitiés
        /// </summary>
        /// <param name="friendshipList">Liste des affinités en textuelle</param>
        private void sortFriendship(List<string> friendshipList)
        {
            foreach (var friendship in friendshipList)
            {
                //on ajoute la majuscule systématiquement au début de chaque phrase 
                var friendshipToUpperFirstLetter = friendship.First().ToString().ToUpper() + friendship.Substring(1);

                var firstnames = searchFirstnames(friendship);

                if (firstnames.Count == 2)
                {
                    //Si la syntaxe commence par je de "je suis l'ami de..."
                    if (firstnames[0].ToLower() == "je")
                    {
                        this.AddFriendship(firstnames[0].ToLower(), firstnames[1]);
                    }
                    else
                    {
                        this.AddFriendship(firstnames[0], firstnames[1]);
                    }
                }
                else
                {
                    Console.WriteLine("Merci d'utiliser la bonne syntaxe pour les liens. Les prénoms doivent commencer par une majuscule. (" + friendship + ")");
                }

            }
        }

        /// <summary>
        /// Ajout une amitié dans le mapping
        /// </summary>
        /// <param name="ownerOfFriendship">Prénom du "propriétaire" de l'amitié</param>
        /// <param name="friend">Prénom de l'ami à ajouter</param>
        private void AddFriendship(string ownerOfFriendship, string friend)
        {
            if (ownerOfFriendship == "je")
            {
                ownerOfFriendship = "moi";
            }

            if (!friendshipMap.ContainsKey(ownerOfFriendship))
            {
                friendshipMap.Add(ownerOfFriendship, new List<string>());
            }


            friendshipMap[ownerOfFriendship].Add(friend);
        }

        /// <summary>
        /// Permet de traiter les questions utilisateurs
        /// </summary>
        /// <param name="userInput">question posée par l'utilisateur</param>
        private void questionTreatment(string userInput)
        {
            userInput = userInput.First().ToString().ToLower() + userInput.Substring(1);

            var firstNames = searchFirstnames(userInput);

            if (firstNames.Count == 1)
            {
                if (checkIfIsMyFriend(firstNames[0], "moi"))
                {
                    Console.WriteLine("oui");
                }
                else
                {
                    Console.WriteLine("non");
                }
            }
            else
            {
                Console.WriteLine("Pour poser une question, merci d'utiliser la syntaxe: Est-ce que [Prénom] est mon ami(e) ? Un seul prénom à la fois.");
            }
        }

        /// <summary>
        /// Fonction récursive permettant de faire une recherche "parcours en largeur" des prénoms
        /// </summary>
        /// <param name="firstnameAsked">prénom recherché 1</param>
        /// <param name="friendAsked">prénom recherché 2</param>
        /// <returns></returns>
        private bool checkIfIsMyFriend(string firstnameAsked, string friendAsked)
        {
            this.firstnameAlreadyChecked.Add(firstnameAsked);
            if (friendshipMap.ContainsKey(firstnameAsked))
            {
                if (friendshipMap[firstnameAsked].Contains(friendAsked))
                {
                    return true;
                }
                else
                {
                    foreach (var pair in friendshipMap[firstnameAsked])
                    {
                        return checkIfIsMyFriend(pair, friendAsked);
                    }
                }
            }
            return false;
        }
    }
}
