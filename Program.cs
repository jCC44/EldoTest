using System;
using System.Collections.Generic;

namespace TestEldo
{
    class Program
    {

        static void Main(string[] args)
        {
            var friendshipList = new List<string>();
            friendshipList.Add("Benjamin est ami avec Paul");
            friendshipList.Add("Sophie est amie avec moi");
            friendshipList.Add("Paul est amie avec Sophie");
            friendshipList.Add("Gertrude est amie avec Toto");
            friendshipList.Add("Je suis ami avec Bernadette");

            var friendManager = new FriendsManager(friendshipList);
        }
    }
}
