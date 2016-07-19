#region

using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK.Events;
using PortAIO.Utility;
using LeagueSharp.Common;
using SharpDX;
using PortAIO.Properties;
using iLucian;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using LeagueSharp.SDK.Core.Utils;
using System.Linq;
// ReSharper disable ObjectCreationAsStatement

#endregion

namespace PortAIO
{
    internal static class Init
    {
        /// <summary>
        ///     Spells that reset the attack timer.
        /// </summary>
        private static readonly string[] AttackResets =
        {
            "dariusnoxiantacticsonh", "fiorae", "garenq",
            "gravesmove", "hecarimrapidslash", "jaxempowertwo", "jaycehypercharge", "leonashieldofdaybreak", "luciane",
            "monkeykingdoubleattack", "mordekaisermaceofspades", "nasusq", "nautiluspiercinggaze", "netherblade",
            "gangplankqwrapper", "powerfist", "renektonpreexecute", "rengarq", "aspectofthecougar",
            "shyvanadoubleattack", "sivirw", "takedown", "talonnoxiandiplomacy", "trundletrollsmash", "vaynetumble",
            "vie", "volibearq", "xenzhaocombotarget", "yorickspectral", "reksaiq", "itemtitanichydracleave", "masochism",
            "illaoiw", "elisespiderw", "fiorae", "meditate", "sejuaninorthernwinds", "asheq"
        };

        public static SCommon.PluginBase.Champion Champion;

        /// <summary>
        ///     Returns true if the spellname resets the attack timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if the specified name is an auto attack reset; otherwise, <c>false</c>.</returns>
        public static bool IsAutoAttackReset(string name)
        {
            return AttackResets.Contains(name.ToLower());
        }

        private static void Main()
        {
            Loading.OnLoadingComplete += Initialize;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Orbwalker.ForcedTarget != null)
            {
                if (!Orbwalker.ForcedTarget.IsVisible || Orbwalker.ForcedTarget.IsDead || !Orbwalker.ForcedTarget.VisibleOnScreen || ObjectManager.Player.IsDead || ObjectManager.Player.LSIsRecalling())
                {
                    Orbwalker.ForcedTarget = null;
                }
            }
        }

        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (IsAutoAttackReset(args.SData.Name))
            {
                Orbwalker.ResetAutoAttack();
            }
        }

        private static void OnProcessSpell(Obj_AI_Base unit, GameObjectProcessSpellCastEventArgs Spell)
        {
            var spellName = Spell.SData.Name;
            if (unit.IsMe && IsAutoAttackReset(spellName))
            {
                if (spellName.ToLower().Contains("vaynetumble"))
                {
                    LeagueSharp.Common.Utility.DelayAction.Add(100, Orbwalker.ResetAutoAttack);
                }
                else
                {
                    Orbwalker.ResetAutoAttack();
                }
            }
        }

        private static void Initialize(EventArgs args)
        {
            LeagueSharp.SDK.Bootstrap.Init();

            Loader.Menu();

            LoadUtility();
            LoadChampion();

            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
        }

        public static void LoadChampion()
        {
            if (!Loader.utilOnly)
            {
                switch (ObjectManager.Player.ChampionName.ToLower())
                {
                    case "aatrox": // BrianSharp's Aatrox
                        switch (Loader.aatrox)
                        {
                            case 0:
                                PortAIO.Champion.Aatrox.Program.Main();
                                break;
                            case 1:
                                new KappaSeries.Aatrox();
                                break;
                            default:
                                PortAIO.Champion.Aatrox.Program.Main();
                                break;
                        }
                        break;
                    case "akali": // Akali by xQx
                        switch (Loader.akali)
                        {
                            case 0:
                                PortAIO.Champion.Akali.Program.Main();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                PortAIO.Champion.Akali.Program.Main();
                                break;
                        }
                        break;
                    case "alistar": // El Alistar
                        PortAIO.Champion.Alistar.Program.OnGameLoad();
                        break;
                    case "amumu": // Shine#
                        switch (Loader.amumu)
                        {
                            case 0:
                                PortAIO.Champion.Amumu.Program.OnLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                PortAIO.Champion.Amumu.Program.OnLoad();
                                break;
                        }
                        break;
                    case "annie":
                        switch (Loader.annie)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                OAnnie.Annie.Load();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "caitlyn":
                        switch (Loader.cait)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 2:
                                Challenger_Series.Program.Main();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "twitch":
                        switch (Loader.twitch)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Infected_Twitch.Program.Load();
                                break;
                            case 2:
                                iTwitch.Twitch.OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 4:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "ashe":
                        switch (Loader.ashe)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Challenger_Series.Program.Main();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "jayce":
                        switch (Loader.jayce)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Jayce.Jayce.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "xerath":
                        switch (Loader.xerath)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ElXerath.Xerath.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "ezreal":
                        switch (Loader.ezreal)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                iDZEzreal.EzrealBootstrap.OnGameLoad();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "ekko": // OKTW & ElEkko
                        switch (Loader.ekko)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ElEkko.ElEkko.OnLoad();
                                break;
                            case 2:
                                EkkoGod.Program.GameOnOnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "graves": // OKTW Graves & D-Graves
                        switch (Loader.graves)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                D_Graves.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "ahri":
                        switch (Loader.ahri)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                AhriSharp.Ahri.Ahri_Load();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;

                    case "anivia":
                        switch (Loader.anivia)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 2:
                                Firestorm_AIO.Program.Main();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "sivir":
                        switch (Loader.sivir)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 2:
                                iSivir.Sivir.OnLoad();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "braum":
                        switch (Loader.braum)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                new FreshBooster.Champion.Braum();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "jinx":
                        switch (Loader.jinx)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                Jinx_Genesis.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "syndra":
                        switch (Loader.syndra)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Syndra.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "urgot":
                        switch (Loader.urgot)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "malzahar":
                        switch (Loader.malzahar)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                SurvivorMalzahar.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "orianna":
                    case "velkoz":
                    case "swain":
                        SebbyLib.Program.GameOnOnGameLoad();
                        break;
                    case "azir": // HeavenStrike
                        switch (Loader.azir)
                        {
                            case 0:
                                HeavenStrikeAzir.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                Azir_Creator_of_Elo.Program.Init();
                                break;
                            default:
                                HeavenStrikeAzir.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "bard": // Dreamless Wanderer & FreshBooster
                        switch (Loader.bard)
                        {
                            case 0:
                                PortAIO.Champion.Bard.Program.OnLoad();
                                break;
                            case 1:
                                new FreshBooster.Champion.Bard();
                                break;
                            default:
                                PortAIO.Champion.Bard.Program.OnLoad();
                                break;
                        }
                        break;
                    case "blitzcrank": // Fresh Booster & OKTW
                        switch (Loader.blitzcrank)
                        {
                            case 0:
                                PortAIO.Champion.Blitzcrank.Program.OnLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                KurisuBlitzcrank.Program.Game_OnGameLoad();
                                break;
                            default:
                                PortAIO.Champion.Blitzcrank.Program.OnLoad();
                                break;
                        }
                        break;
                    case "brand": // TheBrand (or OKTWBrand)
                        switch (Loader.brand)
                        {
                            case 0:
                                PortAIO.Champion.Brand.Program.Load();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                yol0Brand.Program.Game_OnGameLoad();
                                break;
                            default:
                                PortAIO.Champion.Brand.Program.Load();
                                break;
                        }
                        break;
                    case "cassiopeia": // Synx Auto Carry & Seph Cassio
                        switch (Loader.cassiopeia)
                        {
                            case 0:
                                Champion = new SAutoCarry.Champions.Cassiopeia();
                                break;
                            case 1:
                                SephCassiopeia.Cassiopeia.CassMain();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Champion = new SAutoCarry.Champions.Cassiopeia();
                                break;
                        }
                        break;
                    case "chogath": // Underrated Cho'Gath
                        UnderratedAIO.Program.Init();
                        break;
                    case "corki": // ElCorki & OKTW
                        switch (Loader.corki)
                        {
                            case 0:
                                ElCorki.Corki.Game_OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                D_Corki.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 4:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                ElCorki.Corki.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "darius": // Exory & OKTW
                        switch (Loader.darius)
                        {
                            case 0:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                new KurisuDarius.KurisuDarius();
                                break;
                            default:
                                ExorAIO.AIO.OnLoad();
                                break;
                        }
                        break;
                    case "olaf":
                        switch (Loader.olaf)
                        {
                            case 0:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 1:
                                OlafxQx.Program.Init();
                                break;
                            case 2:
                                UnderratedAIO.Program.Init();
                                break;
                            default:
                                ExorAIO.AIO.OnLoad();
                                break;
                        }
                        break;
                    case "nautilus":
                    case "nunu":
                    case "pantheon":
                        ExorAIO.AIO.OnLoad();
                        break;
                    case "tryndamere":
                        ExorAIO.AIO.OnLoad();
                        break;
                    case "renekton":
                        switch (Loader.renekton)
                        {
                            case 0:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 1:
                                UnderratedAIO.Program.Init();
                                break;
                            default:
                                ExorAIO.AIO.OnLoad();
                                break;
                        }
                        break;
                    case "ryze":
                        switch (Loader.ryze)
                        {
                            case 0:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 1:
                                ElEasy.Plugins.Ryze f = new ElEasy.Plugins.Ryze();
                                f.Load();
                                break;
                            case 2:
                                Slutty_ryze.Program.OnLoad();
                                break;
                            case 3:
                                Arcane_Ryze.Program.Load();
                                break;
                            case 4:
                                RyzeAssembly.Program.Init();
                                break;
                            case 5:
                                HeavenStrikeRyze.Program.Game_OnGameLoad();
                                break;
                            default:
                                ExorAIO.AIO.OnLoad();
                                break;
                        }
                        break;
                    case "diana":
                        switch (Loader.diana)
                        {
                            case 0:
                                ElDiana.Diana.OnLoad();
                                break;
                            case 1:
                                Nechrito_Diana.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                ElDiana.Diana.OnLoad();
                                break;
                        }
                        break;
                    case "drmundo": // Hestia's Mundo
                        switch (Loader.mundo)
                        {
                            case 0:
                                Mundo.Mundo.OnLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Mundo.Mundo.OnLoad();
                                break;
                        }
                        break;
                    case "draven": // UltimaDraven
                        switch (Loader.draven)
                        {
                            case 0:
                                RevampedDraven.Program.OnLoad();
                                break;
                            case 1:
                                Tyler1.Program.Load();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 4:
                                new MoonDraven.MoonDraven().Load();
                                break;
                            default:
                                RevampedDraven.Program.OnLoad();
                                break;
                        }
                        break;
                    case "elise":
                        switch (Loader.elise)
                        {
                            case 0:
                                GFUELElise.Elise.OnGameLoad();
                                break;
                            case 1:
                                D_Elise.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                EliseGod.Program.OnGameLoad();
                                break;
                            default:
                                GFUELElise.Elise.OnGameLoad();
                                break;
                        }
                        break;
                    case "evelynn": // Evelynn#
                        switch (Loader.evelynn)
                        {
                            case 0:
                                Evelynn.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Evelynn.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "fiddlesticks": // Feedlesticks
                        Feedlesticks.Program.Game_OnGameLoad();
                        break;
                    case "fiora": // Project Fiora
                        FioraProject.Program.Game_OnGameLoad();
                        break;
                    case "fizz": // Math Fizz
                        MathFizz.Program.Game_OnGameLoad();
                        break;
                    case "galio": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "gangplank": // Underrated AIO
                        switch (Loader.gangplank)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    case "garen": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "gnar": // Slutty Gnar
                        switch (Loader.gnar)
                        {
                            case 0:
                                Slutty_Gnar_Reworked.Gnar.OnLoad();
                                break;
                            case 1:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                Slutty_Gnar_Reworked.Gnar.OnLoad();
                                break;
                        }
                        break;
                    case "gragas": // Gragas - Drunk Carry
                        switch (Loader.gragas)
                        {
                            case 0:
                                GragasTheDrunkCarry.Gragas.Game_OnGameLoad();
                                break;
                            case 1:
                                Nechrito_Gragas.Program.OnGameLoad();
                                break;
                            default:
                                GragasTheDrunkCarry.Gragas.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "hecarim": // JustyHecarim && UnderratedAIO
                        switch (Loader.hecarim)
                        {
                            case 0:
                                JustHecarim.Program.OnLoad();
                                break;
                            case 1:
                                UnderratedAIO.Program.Init();
                                break;
                            default:
                                JustHecarim.Program.OnLoad();
                                break;
                        }
                        break;
                    case "heimerdinger": // 2 Girls 1 Dong
                        Two_Girls_One_Donger.Program.Game_OnGameLoad();
                        break;
                    case "illaoi": // Tentacle Kitty
                        switch (Loader.illaoi)
                        {
                            case 0:
                                Illaoi___Tentacle_Kitty.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                Flowers__Illaoi.Program.Load();
                                break;
                            default:
                                Illaoi___Tentacle_Kitty.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "irelia": // Challenger Series Irelia & IreliaGod
                        switch (Loader.irelia)
                        {
                            case 0:
                                Challenger_Series.Irelia.OnLoad();
                                break;
                            case 1:
                                IreliaGod.Program.OnGameLoad();
                                break;
                            case 2:
                                Irelia.Irelia.Init();
                                break;
                            case 3:
                                Irelia_Reloaded.Program.GameOnOnGameLoad();
                                break;
                            default:
                                Challenger_Series.Irelia.OnLoad();
                                break;
                        }
                        break;
                    case "janna": // LCS Janna & FreshBooster
                        switch (Loader.janna)
                        {
                            case 0:
                                LCS_Janna.Program.OnGameLoad();
                                break;
                            case 1:
                                new FreshBooster.Champion.Janna();
                                break;
                            default:
                                LCS_Janna.Program.OnGameLoad();
                                break;
                        }
                        break;
                    case "jarvaniv": // BrianSharp & D_Jarvan
                        switch (Loader.jarvan)
                        {
                            case 0:
                                BrianSharp.Plugin.JarvanIV.OnLoad();
                                break;
                            case 1:
                                D_Jarvan.Program.Game_OnGameLoad();
                                break;
                            default:
                                BrianSharp.Plugin.JarvanIV.OnLoad();
                                break;
                        }
                        break;
                    case "jax": // xqx
                        switch (Loader.jax)
                        {
                            case 0:
                                JaxQx.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                NoobJaxReloaded.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                JaxQx.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "jhin": // Jhin The Virtuoso & OKTW
                        switch (Loader.jhin)
                        {
                            case 0:
                                Jhin___The_Virtuoso.Jhin.JhinOnLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                hJhin.Program.Load();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 4:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Jhin___The_Virtuoso.Jhin.JhinOnLoad();
                                break;
                        }
                        break;
                    case "kalista": // iKalista
                        switch (Loader.kalista)
                        {
                            case 0:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                new iKalistaReborn.Kalista();
                                break;
                            case 2:
                                Challenger_Series.Program.Main();
                                break;
                            case 3:
                                HastaKalistaBaby.Program.OnGameLoad();
                                break;
                            case 4:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "karma": // Karma by Eskor && Spirit Karma
                        switch (Loader.karma)
                        {
                            case 0:
                                Spirit_Karma.Program.Load();
                                break;
                            case 1:
                                Karma.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Spirit_Karma.Program.Load();
                                break;
                        }
                        break;
                    case "karthus": // OKTW & KarthusSharp
                        switch (Loader.karthus)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                KarthusSharp.Program.Game_OnGameLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "kassadin": // Kassawin & Preserved Kassadin
                        switch (Loader.kassadin)
                        {
                            case 0:
                                Kassawin.Kassadin.OnLoad();
                                break;
                            case 1:
                                Preserved_Kassadin.Program.Load();
                                break;
                            default:
                                Kassawin.Kassadin.OnLoad();
                                break;
                        }
                        break;
                    case "katarina": // Staberina
                        switch (Loader.katarina)
                        {
                            case 0:
                                new Staberina.Katarina();
                                break;
                            case 1:
                                e.Motion_Katarina.Program.Game_OnGameLoad();
                                break;
                            default:
                                new Staberina.Katarina();
                                break;
                        }
                        break;
                    case "kayle": // SephKayle
                        switch (Loader.kayle)
                        {
                            case 0:
                                SephKayle.Program.OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                D_Kayle.Program.Game_OnGameLoad();
                                break;
                            default:
                                SephKayle.Program.OnGameLoad();
                                break;
                        }
                        break;
                    case "aurelionsol": // El Aurelion Sol
                        ElAurelion_Sol.AurelionSol.OnGameLoad();
                        break;
                    case "kennen": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "khazix": // SephKhaZix
                        new SephKhazix.Khazix();
                        break;
                    case "kindred": // Yin Yang Kindred & OKTW
                        switch (Loader.kindred)
                        {
                            case 0:
                                Kindred___YinYang.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                Kindred___YinYang.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "kogmaw":
                        switch (Loader.kogmaw)
                        {
                            case 0:
                                KogMaw.Program.OnLoad();
                                break;
                            case 1:
                                Challenger_Series.Program.Main();
                                break;
                            case 2:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 4:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Challenger_Series.Program.Main();
                                break;
                        }
                        break;
                    case "leblanc": // PopBlanc
                        switch (Loader.leblanc)
                        {
                            case 0:
                                PopBlanc.Program.OnLoad();
                                break;
                            case 1:
                                LeblancOLD.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                new FreshBooster.Champion.Leblanc();
                                break;
                            case 3:
                                Leblanc.Program.Init();
                                break;
                            case 4:
                                BLeblanc.Program.Game_OnGameLoad();
                                break;
                            default:
                                PopBlanc.Program.OnLoad();
                                break;
                        }
                        break;
                    case "leesin": // El Lee Sin
                        switch (Loader.leesin)
                        {
                            case 0:
                                Valvrave_Sharp.Program.MainA();
                                break;
                            case 1:
                                ElLeeSin.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                new FreshBooster.Champion.LeeSin();
                                break;
                            default:
                                Valvrave_Sharp.Program.MainA();
                                break;
                        }
                        break;
                    case "leona": // El Easy
                        new ElEasy.Plugins.Leona();
                        break;
                    case "lissandra": // SephLissandra
                        SephLissandra.Lissandra.OnLoad();
                        break;
                    case "lucian": // LCS Lucian
                        switch (Loader.lucian)
                        {
                            case 0:
                                LCS_Lucian.Program.OnLoad();
                                break;
                            case 1:
                                Challenger_Series.Program.Main();
                                break;
                            case 2:
                                var lucian = new Lucian();
                                lucian.OnLoad();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 4:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break; ;
                            case 5:
                                HoolaLucian.Program.OnGameLoad();
                                break;
                            case 6:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                LCS_Lucian.Program.OnLoad();
                                break;
                        }
                        break;
                    case "lulu": // LuluLicious
                        new LuluLicious.Lulu();
                        break;
                    case "lux": // MoonLux
                        switch (Loader.lux)
                        {
                            case 0:
                                MoonLux.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                ElLux.Program.Init();
                                break;
                            case 3:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                MoonLux.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "malphite": // eleasy
                        new ElEasy.Plugins.Malphite();
                        break;
                    case "missfortune":
                        switch (Loader.missfortune)
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "vayne":
                        switch (Loader.vayne)
                        {
                            case 0:
                                Vayne1.Program.OnLoad();
                                break;
                            case 1:
                                VayneHunter_Reborn.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                hi_im_gosu.Vayne.Game_OnGameLoad();
                                break;
                            case 3:
                                hVayne.Program.Load();
                                break;
                            case 4:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 5:
                                Challenger_Series.Program.Main();
                                break;
                            case 6:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                Vayne1.Program.OnLoad();
                                break;
                        }
                        break;
                    case "quinn": // GFuel Quinn & OKTW
                        switch (Loader.quinn)
                        {
                            case 0:
                                GFUELQuinn.Quinn.OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 2:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 3:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                GFUELQuinn.Quinn.OnGameLoad();
                                break;
                        }
                        break;
                    case "tristana": // ElTristana
                        switch (Loader.tristana)
                        {
                            case 0:
                                ElTristana.Tristana.OnLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                ElTristana.Tristana.OnLoad();
                                break;
                        }
                        break;
                    case "taliyah": // taliyah && tophsharp
                        switch (Loader.taliyah)
                        {
                            case 0:
                                Taliyah.Program.OnLoad();
                                break;
                            case 1:
                                TophSharp.Taliyah.OnLoad();
                                break;
                            default:
                                Taliyah.Program.OnLoad();
                                break;
                        }
                        break;
                    case "thresh":
                        switch (Loader.thresh) // OKTW && Thresh the Ruler
                        {
                            case 0:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            case 1:
                                ThreshTherulerofthesoul.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                Thresh___The_Chain_Warden.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                Slutty_Thresh.SluttyThresh.OnLoad();
                                break;
                            case 4:
                                yol0Thresh.Program.OnLoad();
                                break;
                            case 5:
                                Dark_Star_Thresh.Program.OnLoad();
                                break;
                            case 6:
                                ThreshWarden.ThreshWarden.OnLoad();
                                break;
                            default:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                        }
                        break;
                    case "riven": // Nechrito Riven & Badao Riven
                        switch (Loader.riven)
                        {
                            case 0:
                                NechritoRiven.Program.Init();
                                break;
                            case 1:
                                HeavenStrikeRiven.Program.OnStart();
                                break;
                            case 2:
                                KurisuRiven.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                HoolaRiven.Program.OnGameLoad();
                                break;
                            default:
                                NechritoRiven.Program.Init();
                                break;
                        }
                        break;
                    case "talon": // GFuel Talon
                        GFUELTalon.Talon.OnGameLoad();
                        break;
                    case "zed": // iZed
                        switch (Loader.zed)
                        {
                            case 0:
                                Valvrave_Sharp.Program.MainA();
                                break;
                            case 1:
                                Zed.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                iDZed.Zed.OnLoad();
                                break;
                            default:
                                Valvrave_Sharp.Program.MainA();
                                break;
                        }
                        break;
                    case "udyr": // D_Udyr
                        switch (Loader.udyr)
                        {
                            case 0:
                                D_Udyr.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                D_Udyr.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "maokai": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "masteryi": // MasterSharp
                        MasterSharp.MasterSharp.OnLoad();
                        break;
                    case "mordekaiser": // How to Train your dragon
                        Mordekaiser.Program.Game_OnGameLoad();
                        break;
                    case "morgana": // Kurisu Morg & OKTW
                        switch (Loader.morgana)
                        {
                            case 0:
                                new KurisuMorgana.KurisuMorgana();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            default:
                                new KurisuMorgana.KurisuMorgana();
                                break;
                        }
                        break;
                    case "nami": // vSupport Series
                        new vSupport_Series.Champions.Nami();
                        break;
                    case "nasus": // ElEasy
                        ElEasy.Plugins.Nasus.Load();
                        break;
                    case "nidalee":
                        switch (Loader.nidalee)
                        {
                            case 0:
                                KurisuNidalee.KurisuNidalee.Game_OnGameLoad();
                                break;
                            case 1:
                                Nechrito_Nidalee.Program.OnLoad();
                                break;
                            case 2:
                                D_Nidalee.Program.Game_OnGameLoad();
                                break;
                            default:
                                KurisuNidalee.KurisuNidalee.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "yasuo": // YasuPro
                        switch (Loader.yasuo)
                        {
                            case 0:
                                Valvrave_Sharp.Program.MainA();
                                break;
                            case 1:
                                YasuoPro.Initalization.Main();
                                break;
                            case 2:
                                GosuMechanicsYasuo.Program.Game_OnGameLoad();
                                break;
                            case 3:
                                YasuoSharpV2.Program.Init();
                                break;
                            case 4:
                                Firestorm_AIO.Program.Main();
                                break;
                            default:
                                Valvrave_Sharp.Program.MainA();
                                break;
                        }
                        break;
                    case "nocturne": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "poppy": // Underrated AIO
                        switch (Loader.poppy)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                BadaoKingdom.BadaoChampion.BadaoPoppy.BadaoPoppy.BadaoActivate();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    case "rammus": // BrianSharp
                        new BrianSharp.Plugin.Rammus();
                        break;
                    case "rengar": // ElRengar && D-Rengar
                        switch (Loader.rengar)
                        {
                            case 0:
                                ElRengarRevamped.Rengar.OnLoad();
                                break;
                            case 1:
                                D_Rengar.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                PrideStalker_Rengar.Program.Load();
                                break;
                            default:
                                ElRengarRevamped.Rengar.OnLoad();
                                break;
                        }
                        break;
                    case "soraka": // Sophie's Soraka
                        switch (Loader.soraka)
                        {
                            case 0:
                                Sophies_Soraka.SophiesSoraka.OnGameLoad();
                                break;
                            case 1:
                                Challenger_Series.Program.Main();
                                break;
                            default:
                                Challenger_Series.Program.Main();
                                break;
                        }
                        break;
                    case "twistedfate": // Twisted Fate by Kortatu & OKTW
                        switch (Loader.twistedfate)
                        {
                            case 0:
                                TwistedFate.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            default:
                                TwistedFate.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "varus": // ElVarus
                        switch (Loader.varus)
                        {
                            case 0:
                                Elvarus.Varus.Game_OnGameLoad();
                                break;
                            case 1:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            case 2:
                                SebbyLib.Program.GameOnOnGameLoad();
                                break;
                            default:
                                Elvarus.Varus.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "veigar": // FreshBooster
                        switch (Loader.veigar)
                        {
                            case 0:
                                new FreshBooster.Champion.Veigar();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                new FreshBooster.Champion.Veigar();
                                break;
                        }
                        break;
                    case "reksai": // D-Reksai && HeavenStrikeReksaj
                        switch (Loader.reksai)
                        {
                            case 0:
                                D_RekSai.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                HeavenStrikeReksaj.Program.Game_OnGameLoad();
                                break;
                            default:
                                D_RekSai.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "rumble": // Underrated AIO & ElRumble
                        switch (Loader.rumble)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                ElRumble.Rumble.OnLoad();
                                break;
                            default:
                                ElRumble.Rumble.OnLoad();
                                break;
                        }
                        break;
                    case "sejuani": // ElSejuani
                        ElSejuani.Sejuani.OnLoad();
                        break;
                    case "shaco": // Underrated AIO & ChewyMoon's Shaco
                        switch (Loader.shaco)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                ChewyMoonsShaco.ChewyMoonShaco.OnGameLoad();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    case "shen": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "skarner": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "sona": // vSeries Support & ElEasy Sona
                        switch (Loader.sona)
                        {
                            case 0:
                                new vSupport_Series.Champions.Sona();
                                break;
                            case 1:
                                ElEasy.Plugins.Sona f = new ElEasy.Plugins.Sona();
                                f.Load();
                                break;
                            default:
                                new vSupport_Series.Champions.Sona();
                                break;
                        }
                        break;
                    case "teemo": // Sharpshooter & Swiftly_Teemo
                        switch (Loader.teemo)
                        {
                            case 0:
                                new SharpShooter.Plugins.Teemo();
                                break;
                            case 1:
                                Swiftly_Teemo.Program.Load();
                                break;
                            case 2:
                                Marksman.Program.Game_OnGameLoad();
                                break;
                            default:
                                new SharpShooter.Plugins.Teemo();
                                break;
                        }
                        break;
                    case "viktor": // Trus In my Viktor
                        Viktor.Program.Game_OnGameLoad();
                        break;
                    case "vladimir": // ElVlad
                        ElVladimirReborn.Vladimir.OnLoad();
                        break;
                    case "warwick": // Warwick - Mirin
                        switch (Loader.warwick)
                        {
                            case 0:
                                Warwick.Program.Game_OnGameLoad();
                                break;
                            case 1:
                                ExorAIO.AIO.OnLoad();
                                break;
                            default:
                                Warwick.Program.Game_OnGameLoad();
                                break;
                        }
                        break;
                    case "monkeyking": // Wukong - xQx
                        Wukong.Program.Game_OnGameLoad();
                        break;
                    case "xinzhao": // Xin xQx
                        XinZhao.Program.Game_OnGameLoad();
                        break;
                    case "ziggs": // Ziggs#
                        Ziggs.Program.Game_OnGameLoad();
                        break;
                    case "yorick": // UnderratedAIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "zyra": // D-Zyra
                        D_Zyra.Program.Game_OnGameLoad();
                        break;
                    case "zilean": // ElZilean
                        ElZilean.Zilean.OnGameLoad();
                        break;
                    case "shyvana": // D-Shyvana
                        D_Shyvana.Program.Game_OnGameLoad();
                        break;
                    case "singed": // ElSinged
                        ElSinged.Singed.Game_OnGameLoad();
                        break;
                    case "tahmkench": // Underrated AIO
                        UnderratedAIO.Program.Init();
                        break;
                    case "sion": // Underrated AIO
                        switch (Loader.sion)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                Sion.Program.Game_OnGameLoad();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    case "vi": //ElVi
                        ElVi.Vi.OnLoad();
                        break;
                    case "volibear": // Underrated AIO && VoliPower
                        switch (Loader.volibear)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                VoliPower.Program.Game_OnLoad();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    case "trundle": // ElTrundle
                        switch (Loader.trundle)
                        {
                            case 0:
                                ElTrundle.Trundle.OnLoad();
                                break;
                            case 1:
                                FastTrundle.Trundle.Game_OnGameLoad();
                                break;
                            case 2:
                                UnderratedAIO.Program.Init();
                                break;
                            default:
                                ElTrundle.Trundle.OnLoad();
                                break;
                        }
                        break;
                    case "taric": // SkyLv_Taric
                        new SkyLv_Taric.SkyLv_Taric();
                        break;
                    case "zac": // Underrated Zac & Zac_The_Secret_Flubber
                        switch (Loader.zac)
                        {
                            case 0:
                                UnderratedAIO.Program.Init();
                                break;
                            case 1:
                                Zac_The_Secret_Flubber.Program.Game_OnGameLoad();
                                break;
                            default:
                                UnderratedAIO.Program.Init();
                                break;
                        }
                        break;
                    default:
                        Chat.Print("This champion is not supported yet but the utilities will still load! - Berb");
                        break;
                }
            }
        }

        private static void LoadUtility()
        {

            if (!Loader.champOnly)
            {
                if (Loader.useActivator)
                {
                    switch (Loader.activatorCB)
                    {
                        case 0:
                            ElUtilitySuite.Entry.OnLoad();
                            break;
                        case 1:
                            NabbActivator.Index.OnLoad();
                            break;
                        case 2:
                            Activators.Activator.Game_OnGameLoad();
                            break;
                        default:
                            ElUtilitySuite.Entry.OnLoad();
                            break;
                    }
                }

                if (Loader.useRecall)
                {
                    UniversalRecallTracker.Program.Main();
                }

                if (Loader.useSkin)
                {
                    SDK_SkinChanger.Program.Load();
                }

                if (Loader.useTracker)
                {
                    switch (Loader.trackerCB)
                    {
                        case 0:
                            NabbTracker.Program.Game_OnGameLoad();
                            break;
                        case 1:
                            Tracker.Program.Game_OnGameLoad();
                            break;
                        default:
                            NabbTracker.Program.Game_OnGameLoad();
                            break;
                    }
                }

                if (Loader.dzaware)
                {
                    DZAwarenessAIO.Program.Game_OnGameLoad();
                }

                if (Loader.godTracker)
                {
                    GodJungleTracker.Program.OnGameLoad();
                    Chat.Print("Berb : Depending on whether packets are updated or not will this work.");
                }

                if (Loader.ping)
                {
                    new UniversalPings.Program();
                }

                if (Loader.human)
                {
                    Humanizer.Program.Game_OnGameLoad();
                }

                if (Loader.gank)
                {
                    UniversalGankAlerter.Program.Main();
                }

                if (Loader.evade)
                {
                    switch (Loader.evadeCB)
                    {
                        case 0:
                            new ezEvade.Evade();
                            break;
                        case 1:
                            EvadeSharp.Program.Game_OnGameStart();
                            break;
                        default:
                            new ezEvade.Evade();
                            break;
                    }
                }

                if (Loader.predictioner)
                {
                    switch (Loader.predictionerCB)
                    {
                        case 0:
                            EBPredictioner.Program.Init();
                            break;
                        case 1:
                            SDKPredictioner.Program.Init();
                            break;
                        case 2:
                            OKTWPredictioner.Program.Init();
                            break;
                        case 3:
                            SPredictioner.Program.Init();
                            break;
                        case 4:
                            SharpPredictioner.Program.Init();
                            break;
                        default:
                            EBPredictioner.Program.Init();
                            break;
                    }
                }

                if (Loader.cheat)
                {
                    new TheCheater.TheCheater().Load();
                }

                if (Loader.banwards)
                {
                    Sebby_Ban_War.Program.Game_OnGameLoad();
                }

                if (Loader.antialistar)
                {
                    AntiAlistar.AntiAlistar.OnLoad();
                }

                if (Loader.traptrack)
                {
                    AntiTrap.Program.Game_OnGameLoad();
                }

                if (Loader.limitedShat)
                {
                    LimitedShat.Program.Game_OnGameLoad();
                }

                if (Loader.autoLevel)
                {
                    AutoLevelup.Program.Game_OnGameLoad();
                }

                if (Loader.chatLogger)
                {
                    Chat_Logger.Program.Init();
                }

                if (Loader.autoFF)
                {
                    AutoFF.Program.Game_OnGameLoad();
                }

                if (Loader.urfSpell)
                {
                    URF_Spell_Spammer.Program.Game_OnGameLoad();
                }

                if (Loader.pastingSharp)
                {
                    PastingSharp.Program.Game_OnGameLoad();
                }

                if (Loader.emoteSpammer)
                {
                    EmoteSpammer.Program.Game_OnGameLoad();
                }

                if (Loader.antiStealth)
                {
                    new AntiStealth.AntiStealth();
                }

                if (Loader.reform)
                {
                    Toxic_Player_Reform_Program.Program.Main();
                }

                if (Loader.feed)
                {
                    BlackFeeder.Program.Init();
                }

                if (Loader.mes)
                {
                    Mastery_Badge_Spammer.Program.Init();
                }

                if (Loader.dev)
                {
                    DeveloperSharp.Program.Init();
                }

                if (Loader.cursor)
                {
                    VCursor.Program.Game_OnGameLoad();
                }

                if (Loader.condemn)
                {
                    AsunaCondemn.Program.Main();
                }

                if (Loader.randomult)
                {
                    RandomUlt.Program.Main();
                }

                //if (Loader.orbwalker)
                //{
                //PuppyStandaloneOrbwalker.Program.Game_OnGameLoad();
                //}

                /*
                if (Loader.stream)
                {
                    StreamBuddy.Program.Main();
                }
                if (RandomUltChampsList.Contains(ObjectManager.Player.ChampionName))
                {
                    if (Loader.randomUlt)
                    {
                        RandomUlt.Program.Game_OnGameLoad();
                    }
                }
                if (BaseUltList.Contains(ObjectManager.Player.ChampionName))
                {
                    if (Loader.baseUlt)
                    {
                        new BaseUlt3.BaseUlt();
                    }
                }
                */
            }
        }
    }
}
