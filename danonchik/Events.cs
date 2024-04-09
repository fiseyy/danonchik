using System;
using GTANetworkAPI;

namespace danonchik
{
    internal class Events : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            database.InitConnection();
        }
        [ServerEvent(Event.PlayerConnected)]
        private static void OnPlayerConnected(Player player)
        {
            //player.SendChatMessage("Добро пожаловать на сервер...");
            player.Dimension = 1;
            /*if (database.IsAccountExists(player.Name))
            {
                player.SendChatMessage("~r~[LOGIN]~w~Ваш аккаунт уже ~g~зарегистрирован~w~ на сервере. ~b~Используйте /login~w~ для авторизации.");
            }
            else
            {
                player.SendChatMessage("~r~[LOGIN]~w~Ваш аккаунт ещё ~g~не зарегистрирован~w~ на сервере. ~b~Используйте /register~w~ для регистрации.");
            }*/
            player.SendChatMessage("~r~[!]~w~ Добро пожаловать на сервер. Для того, чтобы начать играть, авторизуйтесь или зарегистрируйтесь на сервере, введя команду /login или /register");
        }
        [ServerEvent(Event.PlayerSpawn)]
        private static void OnPlayerSpawn(Player player)
        {
            player.Health = 100;
            player.Armor = 0;
        }
    }
}
