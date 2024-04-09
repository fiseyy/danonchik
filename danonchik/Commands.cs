using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace danonchik
{
    internal class Commands : Script
    {
        [Command("gethere", "USAGE: /gethere [nickname]", Alias = "gh")]
        private static void cmd_gethere(Player player, Player target)
        {
            
        }
        [Command("freeze", "USAGE: /freeze [nickname] [true/false]", Alias = "")]
        private static void cmd_freezeplayer(Player player, Player target, bool freezestatus)
        {
            NAPI.ClientEvent.TriggerClientEvent(target, "PlayerFreeze", freezestatus);
        }
        [Command("setmydim", "USAGE: /setmydim DIM")]
        private static void cmd_setMyDim(Player player, uint dim)
        {
            player.Dimension = dim;
        }
        [Command("spped", "USAGE: /spped ")]
        private static void cmd_spped(Player player, uint pedhash)
        {
            NAPI.Ped.CreatePed(pedhash, player.Position, player.Heading, 0);
        }
        [Command("setskin", "USAGE: /setskin Skin_Hash")]
        private static void cmd_setskin(Player player, uint skinHash)
        {
            //uint skinHash = NAPI.Util.GetHashKey(skinName);
            player.SetSkin(skinHash);
        }
        [Command("login", "USAGE: /login LOGIN PASSWORD", Alias = "log_in")]
        private static void cmd_login(Player player, string login, string password)
        {
            string name = login;
            if (database.IsAccountExists(name)) { 
                if(database.Login(name, password))
                {
                    player.Dimension = 0;
                    player.SendChatMessage("~r~[LOGIN]~w~ Вы ~b~успешно авторизовались ~w~на сервере. Приятной игры.");
                }
                else
                {
                    player.SendChatMessage("~r~[LOGIN] ~w~Неверный пароль. Попробуйте ещё раз.");
                }
            }
            else
            {
                player.SendChatMessage("~r~[LOGIN] ~w~Аккаунт с таким логином не существует. Для регистрации аккаунта воспользуйтесь командой /register.");
            }
        }
        [Command("register", "USAGE: /register PASSWORD", Alias = "reg")]
        private static void cmd_reg(Player player, string login, string password)
        {
            if (!database.IsAccountExists(login))
            {
                if (database.Reg(login, password))
                {
                    player.Dimension = 0;
                    player.SendChatMessage("~r~[REG]~w~ Вы ~b~успешно зарегистрировались ~w~на сервере. Приятной игры.");
                }
                else {
                    player.SendChatMessage("~r~[REG]~w~ Что-то пошло не так. Проверьте правильность введенных данных и повторите попытку.");
                }
            }
            else
            {
                player.SendChatMessage("~r~[REG] ~w~Аккаунт уже существует. Воспользуйтесь командой /login для входа.");
            }
        }
        [Command("veh", "USAGE: /veh MODEL COLOR1 COLOR2", Alias = "vehicle")]
        private static void cmd_veh(Player player, string vehname, int color1 = 0, int color2 = 0)
        {
            uint veh_hash = NAPI.Util.GetHashKey(vehname);
            if (veh_hash <= 0)
            {
                player.SendChatMessage("~r~ERROR in command /veh: ~w~Неверное название т/с.");
            }
            else
            {
                Vehicle spawned_veh = NAPI.Vehicle.CreateVehicle(veh_hash, player.Position, player.Heading, color1, color2, "ADMIN");

                spawned_veh.Locked = false;
                spawned_veh.EngineStatus = true;
                player.SetIntoVehicle(spawned_veh, (int)VehicleSeat.Driver);
            }
        }
        [Command("delveh", "USAGE: /delveh", Alias = "deleteveh")]
        private static void cmd_delveh(Player player)
        {
            if (player.IsInVehicle == true)
            {
                Vehicle veh = NAPI.Player.GetPlayerVehicle(player);
                veh.Delete();
            }
            else
            {
                player.SendChatMessage("Вы не сидите в автомобиле");
            }
        }

        [Command("fix", "USAGE: /fix", Alias = "fixveh")]
        private static void cmd_fix(Player player)
        {
            if (player.IsInVehicle == true)
            {
                Vehicle veh = NAPI.Player.GetPlayerVehicle(player);
                veh.Repair();
            }
            else
            {
                player.SendChatMessage("Вы не сидите в автомобиле");
            }
        }
        [Command("stt", "USAGE: /stt speed acceleration", Alias = "setmaxspeed")]
        private static void cmd_stt(Player player, int maxspeed, int maxacceleration)
        {
            if (player.IsInVehicle == true)
            {
                Vehicle veh = NAPI.Player.GetPlayerVehicle(player);

            }
            else
            {
                player.SendChatMessage("Вы не сидите в автомобиле");
            }
        }
        [Command("rescue", "USAGE: /rescue ID", Alias = "res")]
        private static void cmd_rescue(Player player, uint id)
        {
           
        }
    }
}
