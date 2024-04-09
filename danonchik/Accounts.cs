using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;


namespace danonchik
{
    internal class Accounts
    {
        private const string _account_key = "Player_Data";
        private int _id;
        private string _name;
        private long _cash;
        public Accounts()
        {
            this._name = "";
            this._cash = 1000;
        }
        public Accounts(string name, long cash = 1000)
        {
            this._name = name;
            this._cash = cash;
        }
        public static bool IsPlayerLoggedIn(Player player)
        {
            if (player != null) return player.HasData(_account_key);
            return false;
        }
    }
}
