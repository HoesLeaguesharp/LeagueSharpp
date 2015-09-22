﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace Slutty_Utility.Drawings
{
    internal class AllyRanges
    {
        public static readonly SpellSlot[] Slots =
        {
            SpellSlot.Q,
            SpellSlot.E,
            SpellSlot.W,
            SpellSlot.R
        };

        public static void OnLoad()
        {
            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            foreach (
                var hero in
                    HeroManager.AllHeroes.Where(
                        x => (x.IsAlly || x.IsMe) &&
                            !x.IsDead && x.IsValid && x.Position.Distance(Helper.Player.Position) < 2000 &&
                            x.IsChampion()))
            {
                if (!Helper.GetBool("showdrawingss" + hero.ChampionName, typeof(bool)))
                    return;

                if (Helper.GetBool("showdrawingsaaa" + hero.ChampionName, typeof(bool)))
                {
                    Render.Circle.DrawCircle(hero.Position, hero.AttackRange, Color.DeepPink, 3);
                }

                foreach (var spell in hero.Spellbook.Spells)
                {
                    foreach (var herospell in Slots)
                    {
                        if (spell.Slot == herospell && herospell.IsReady() &&
                            Helper.GetBool(
                                "spellrange.spellrangeenemy.spellrangeallyname" + herospell + hero.ChampionName,
                                typeof (bool)))
                        {
                            Render.Circle.DrawCircle(hero.Position, spell.SData.CastRange, spell.Slot == SpellSlot.Q
                                ? Color.Blue
                                : spell.Slot == SpellSlot.E
                                    ? Color.Red
                                    : spell.Slot == SpellSlot.W
                                        ? Color.Chocolate
                                        : Color.Aqua, 0);
                        }
                    }
                }
            }

        }
    }
}
